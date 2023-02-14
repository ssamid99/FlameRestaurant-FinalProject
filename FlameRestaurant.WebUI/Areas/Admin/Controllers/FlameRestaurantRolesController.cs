using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlameRestaurant.Domain.Business.FlameRestaurantRoleModule;
using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities.Membership;

namespace FlameRestaurant.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FlameRestaurantRolesController : Controller
    {
        private readonly FlameRestaurantDbContext _context;
        private readonly RoleManager<FlameRestaurantRole> roleManager;
        private readonly IMediator mediator;

        public FlameRestaurantRolesController(FlameRestaurantDbContext context,RoleManager<FlameRestaurantRole> roleManager, IMediator mediator)
        {
            _context = context;
            this.roleManager = roleManager;
            this.mediator = mediator;
        }

        [Authorize("admin.flamerestaurantroles.index")]
        public async Task<IActionResult> Index(FlameRestaurantRoleGetAllQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }

        [Authorize("admin.flamerestaurantroles.details")]
        public async Task<IActionResult> Details(FlameRestaurantRoleGetSingleQuery query)
        {
            var response = await mediator.Send(query);
            if(response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        [Authorize("admin.flamerestaurantroles.create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/RadissonRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("admin.flamerestaurantroles.create")]
        public async Task<IActionResult> Create(FlameRestaurantRolePostCommand command)
        {
            if (ModelState.IsValid)
            {
                var response = await mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }

        [Authorize("admin.flamerestaurantroles.edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var radissonRole = await _context.FlameRestaurantRoles.FindAsync(id);
            if (radissonRole == null)
            {
                return NotFound();
            }
            var editCommand = new FlameRestaurantRolePutCommand();
            editCommand.Name = radissonRole.Name;
            return View(editCommand);
        }

        // POST: Admin/RadissonRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("admin.flamerestaurantroles.edit")]
        public async Task<IActionResult> Edit(FlameRestaurantRolePutCommand command)
        {
            if (ModelState.IsValid)
            {
                var response = await mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }


        // POST: Admin/RadissonRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize("admin.flamerestaurantroles.delete")]
        public async Task<IActionResult> DeleteConfirmed(FlameRestaurantRoleRemoveCommand command)
        {
            var respone = await mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

    }
}
