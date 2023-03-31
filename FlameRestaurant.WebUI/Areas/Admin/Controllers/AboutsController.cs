using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using FlameRestaurant.Domain.Business.AboutModule;

namespace FlameRestaurant.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutsController : Controller
    {
        private readonly FlameRestaurantDbContext db;
        private readonly IMediator mediator;

        public AboutsController(FlameRestaurantDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }
        [Authorize("admin.abouts.index")]
        // GET: Admin/Abouts
        public async Task<IActionResult> Index(AboutGetAllQuery query)
        {
            var response = await mediator.Send(query);
            bool isTableEmpty = !db.Abouts.Any();
            ViewBag.IsTableEmpty = isTableEmpty;
            return View(response);
        }
        [Authorize("admin.abouts.create")]
        // GET: Admin/Abouts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Abouts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("admin.abouts.create")]
        public async Task<IActionResult> Create(AboutPostCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", command);
            }
            else
            {
                var reponse = await mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
        }
        [Authorize("admin.abouts.edit")]
        // GET: Admin/Abouts/Edit/5
        public async Task<IActionResult> Edit(AboutGetAllQuery query)
        {
            var about = await mediator.Send(query);
            if (about == null)
            {
                return NotFound();
            }
            var editCommand = new AboutPutCommand();
            editCommand.Title = about.Title;
            editCommand.Text = about.Text;
            return View(editCommand);
        }

        // POST: Admin/Abouts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("admin.about.edit")]
        public async Task<IActionResult> Edit(AboutPutCommand command)
        {
            if (ModelState.IsValid)
            {
                var response = await mediator.Send(command);
                if (response == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }
    }
}
