using FlameRestaurant.Application.AppCode.Extensions;
using FlameRestaurant.Application.AppCode.Services;
using FlameRestaurant.Domain.Models.Entities.Membership;
using FlameRestaurant.Domain.Models.FormData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FlameRestaurant.WebUI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<FlameRestaurantUser> signInManager;
        private readonly UserManager<FlameRestaurantUser> userManager;
        private readonly EmailService emailService;

        public AccountController(SignInManager<FlameRestaurantUser> signInManager, UserManager<FlameRestaurantUser> userManager, EmailService emailService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.emailService = emailService;
        }

        [HttpGet]
        public IActionResult Signin()
        {
            return View();
        }
        [Route("signin.html")]
        [HttpPost]
        public async Task<IActionResult> Signin(UserModel user)
        {
            if (ModelState.IsValid)
            {
                FlameRestaurantUser foundedUser = null;

                if (user.Username.IsEmail())
                {
                    foundedUser = await userManager.FindByEmailAsync(user.Username);
                }
                else
                {
                    foundedUser = await userManager.FindByNameAsync(user.Username);
                }

                if (foundedUser == null)
                {
                    ModelState.AddModelError("Username", "Istifadeci adiniz yaxud shifreniz yanlishdir!");
                    goto end;
                }



                var signinResult = await signInManager.PasswordSignInAsync(foundedUser, user.Password, true, true);

                if (!signinResult.Succeeded)
                {
                    ModelState.AddModelError("Username", "Istifadeci adiniz yaxud shifreniz yanlishdir!");
                    goto end;
                }

                var callbackurl = Request.Query["ReturnUrl"];
                if (!string.IsNullOrWhiteSpace(callbackurl))
                {
                    return Redirect(callbackurl);
                }
                return RedirectToAction("Index", "Home");

            }
        end:
            return View(user);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [Route("register.html")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new FlameRestaurantUser();
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.UserName = model.Email;
                user.Email = model.Email;


                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    string path = $"{Request.Scheme}://{Request.Host}/registration-confirm.html?email={user.Email}&token={token}";

                    var emailResponse = await emailService.SendMailAsync(user.Email, "Registration for LoginApp.com", $"Abuneliyinizi <a href='{path}'>link</a> vasitesile tesdiq edin");

                    if (emailResponse)
                    {
                        ViewBag.Message = "Qeydiyyat uğurla tamamlandı";
                    }
                    else
                    {
                        ViewBag.Message = " E-mail' göndərərkən xəta baş verdi, zəhmət olmasa yenidən cəhd edin";
                    }
                    //email confirmsizdir
                    ViewBag.Message = "Tebrikler siz qeydiyyat oldunuz";
                    return RedirectToAction(nameof(Signin));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }
            return View(model);
        }
        [Route("registration-confirm.html")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterConfirm(string email, string token)
        {
            var foundedUser = await userManager.FindByEmailAsync(email);
            if (foundedUser == null)
            {
                ViewBag.Message = "Xetalı token";
                goto end;
            }
            token = token.Replace(" ", "+");
            var result = await userManager.ConfirmEmailAsync(foundedUser, token);

            if (!result.Succeeded)
            {
                ViewBag.Message = "Xetalı token";
                goto end;
            }

            ViewBag.Message = "Hesabiniz Tesdiqlendi";
        end:
            return RedirectToAction(nameof(Signin));
        }
        [Route("/logout.html")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "home");
        }
    }
}
