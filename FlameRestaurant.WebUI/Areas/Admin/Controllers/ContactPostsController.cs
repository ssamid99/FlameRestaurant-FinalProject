using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FlameRestaurant.Application.AppCode.Extensions;
using FlameRestaurant.Application.AppCode.Services;
using FlameRestaurant.Domain.Business.ContactPostModule;
using FlameRestaurant.Domain.Models.DbContexts;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FlameRestaurant.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactPostsController : Controller
    {
        private readonly FlameRestaurantDbContext db;
        private readonly IMediator mediator;
        private readonly EmailService emailService;

        public ContactPostsController(FlameRestaurantDbContext db, IMediator mediator, EmailService emailService)
        {
            this.db = db;
            this.mediator = mediator;
            this.emailService = emailService;
        }

        [Authorize("admin.contactposts.index")]
        public async Task<IActionResult> Index(ContactPostGetAllQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }

        [Authorize("admin.contactposts.details")]
        public async Task<IActionResult> Details(ContactPostGetSingleQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }
            ViewBag.UserSN = new Func<int, string>(GetUserNameSurname);
            return View(response);
        }

        [Authorize("admin.contactposts.create")]
        public IActionResult Create()//Bu Action UI ucundur
        {
            var userId = User.GetCurrentUserId();

            if (userId > 0)
            {
                var user = db.Users.FirstOrDefault(u => u.Id == userId);

                if (user != null)
                {
                    ViewBag.Name = user.Name;
                    ViewBag.Surname = user.Surname;
                    ViewBag.Email = user.Email;
                }

            }
            return View();
        }

        // POST: Admin/ContactPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("admin.contactposts.create")]
        public async Task<IActionResult> Create(ContactPostPostCommand command)//Bu Action UI ucundur
        {
            if (ModelState.IsValid)
            {
                var response = await mediator.Send(command);
               
                if (response != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            var userId = User.GetCurrentUserId();

            if (userId > 0)
            {
                var user = db.Users.FirstOrDefault(u => u.Id == userId);

                if (user != null)
                {
                    ViewBag.Name = user.Name;
                    ViewBag.Surname = user.Surname;
                    ViewBag.Email = user.Email;
                }

            }

            return View(command);
        }

        [Authorize("admin.contactposts.edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            var contactPost = await db.ContactPosts.FindAsync(id);
            if (contactPost == null)
            {
                return NotFound();
            }
            var editCommand = new ContactPostPutCommand();
            editCommand.Answer = contactPost.Answer;
            editCommand.AnswerDate = contactPost.AnswerDate;
            return View(contactPost);
        }

        // POST: Admin/ContactPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("admin.contactposts.edit")]
        public async Task<IActionResult> Edit(ContactPostPutCommand command)
        {
            if (ModelState.IsValid)
            {
                var response = await mediator.Send(command);
                if (response !=null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(command);
        }

        // POST: Admin/ContactPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize("admin.contactposts.delete")]
        public async Task<IActionResult> DeleteConfirmed(ContactPostRemoveCommand command)
        {
            var response = await mediator.Send(command);

            if (response != null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(response);
        }

        public string GetUserNameSurname(int id)
        {
            var data = db.Users.FirstOrDefault(u => u.Id == id);
            var ns = $"{data.Name} {data.Surname}";
            return ns;
        }

    }
}
