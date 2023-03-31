using FlameRestaurant.Application.AppCode.Extensions;
using FlameRestaurant.Domain.Business.AboutModule;
using FlameRestaurant.Domain.Business.AboutModule.Teams;
using FlameRestaurant.Domain.Business.CategoryModule;
using FlameRestaurant.Domain.Business.ContactPostModule;
using FlameRestaurant.Domain.Business.ReservationModule;
using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.WebUI.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FlameRestaurant.WebUI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly FlameRestaurantDbContext db;
        private readonly IMediator mediator;

        public HomeController(FlameRestaurantDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> About(AboutGetAllQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        public async Task<IActionResult> Menu(CategoryGetAllQuery query)
        {
            var response = await mediator.Send(query);
            if(response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        public async Task<IActionResult> Team(TeamGetAllQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        [HttpGet]
        public IActionResult Contact()
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactPostPostCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", command);
            }
            else
            {
                var reponse = await mediator.Send(command);

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
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReservation(ReservationPostCommand command)
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
            if (!ModelState.IsValid)
            {
                return View("_ReservationForm", command);
            }
            else
            {
                var response = await mediator.Send(command);

                if (response.Error == false)
                {
                    return RedirectToAction(nameof(Index));
                }

            }

            return View("_ReservationForm", command);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
