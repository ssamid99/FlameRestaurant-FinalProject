using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlameRestaurant.Application.AppCode.Extensions;
using FlameRestaurant.Domain.Business.ReservationModule;
using FlameRestaurant.Domain.Models.DbContexts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FlameRestaurant.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReservationsController : Controller
    {
        private readonly FlameRestaurantDbContext db;
        private readonly IMediator mediator;

        public ReservationsController(FlameRestaurantDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }

        [Authorize("admin.reservations.index")]
        public async Task<IActionResult> Index(ReservationGetAllQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }
        [HttpGet]
        [Authorize("admin.reservations.details")]
        public async Task<IActionResult> Details(ReservationGetSingleQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }
        [Authorize("admin.reservations.create")]
        public IActionResult Create()
        {
            var userId = User.GetCurrentUserId();

            if (userId > 0)
            {
                var user = db.Users.FirstOrDefault(u => u.Id == userId);

                if (user != null)
                {
                    ViewBag.Name = user.Name;
                }

            }
            return View();
        }

        // POST: Admin/Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("admin.reservations.create")]
        public async Task<IActionResult> Create(ReservationPostCommand command)
        {
            if (ModelState.IsValid)
            {
                var response = await mediator.Send(command);
                
                if (response.Error == false)
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
                }

            }
            return View(command);
        }

        [Authorize("admin.reservations.edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await db.Reservations.FirstOrDefaultAsync(r => r.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }
            
            var editCommand = new ReservationPutCommand();
            editCommand.Id = reservation.Id;
            editCommand.Name = reservation.Name;
            editCommand.NumberofPeople = reservation.NumberofPeople;
            editCommand.Date = reservation.Date;
            return View(editCommand);
        }

        // POST: Admin/Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("admin.reservations.edit")]
        public async Task<IActionResult> Edit(ReservationPutCommand command)
        {
            if (ModelState.IsValid)
            {
                var response = await mediator.Send(command);

                if (response.Error == false)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("admin.reservations.delete")]
        public async Task<IActionResult> Delete(ReservationRemoveCommand command)
        {
            if (ModelState.IsValid)
            {
                var response = await mediator.Send(command);

                if (response != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(command);
        }

    }
}
