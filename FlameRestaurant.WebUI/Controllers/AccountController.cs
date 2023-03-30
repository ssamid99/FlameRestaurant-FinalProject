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
                    ModelState.AddModelError("Email", "Your email or password is incorrect!");
                    goto end;
                }

                var userm = await userManager.IsEmailConfirmedAsync(foundedUser);

                if (userm == false)
                {
                    ModelState.AddModelError("EmailConfirmed", "Confirm Email!");
                    goto end;
                }


                var signinResult = await signInManager.PasswordSignInAsync(foundedUser, user.Password, true, true);

                if (!signinResult.Succeeded)
                {
                    ModelState.AddModelError("Email", "Your email or password is incorrect!");
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

                    var emailResponse = await emailService.SendMailAsync(user.Email, "Registration for FlameRestaurant", $"Confirm your subscription with this <a href='{path}'>link</a>.");

                    if (emailResponse)
                    {
                        ViewBag.Message = "Registration is successfully completed";
                    }
                    else
                    {
                        ViewBag.Message = "An error has occurred while sending email!";
                    }
                    //email confirmsizdir
                    ViewBag.Message = "Congratulations, you have successfully registered";
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
                ViewBag.Message = "Invalid token";
                goto end;
            }
            token = token.Replace(" ", "+");
            var result = await userManager.ConfirmEmailAsync(foundedUser, token);

            if (!result.Succeeded)
            {
                ViewBag.Message = "Invalid token";
                goto end;
            }

            ViewBag.Message = "Your account is approved!";
        end:
            return RedirectToAction(nameof(Signin));
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(FlameRestaurantForgotPassword model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var foundedUser = await userManager.FindByEmailAsync(model.Email);

            if (foundedUser != null && await userManager.IsEmailConfirmedAsync(foundedUser))
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(foundedUser);
                token = token.Replace(" ", "+");
                string path = $"{Request.Scheme}://{Request.Host}/reset-password.html?email={foundedUser.Email}&token={token}";

                var emailResponse = await emailService.SendMailAsync(foundedUser.Email, "Reset Password", $"To reset your password click this <a href='{path}'>link</a>!");
                return View("ForgotPasswordConfirmation");
            }
            return View(model);
        }

        [Route("reset-password.html")]
        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new ResetPasswordModel();
            model.Token = token.Replace(" ", "+");
            model.Email = email;
            return View(model);
        }

        [Route("reset-password.html")]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var foundedUser = await userManager.FindByEmailAsync(model.Email);

            if (foundedUser == null)
            {
                return View(model);
            }
            var result = await userManager.ResetPasswordAsync(foundedUser, model.Token.Replace(" ", "+"), model.Password);

            if (result.Succeeded)
            {
                return View("ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [Route("/logout.html")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "home");
        }
    }
}
