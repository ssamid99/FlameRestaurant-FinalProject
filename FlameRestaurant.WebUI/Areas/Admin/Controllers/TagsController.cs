using FlameRestaurant.Domain.Business.TagModule;
using FlameRestaurant.Domain.Models.DbContexts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace FlameRestaurant.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagsController : Controller
    {
        private readonly FlameRestaurantDbContext db;
        private readonly IMediator mediator;

        public TagsController(FlameRestaurantDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }

        [Authorize(Policy = "admin.tags.index")]
        public async Task<IActionResult> Index(TagGetAllQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }


        [Authorize(Policy = "admin.tags.details")]
        public async Task<IActionResult> Details(TagGetSingleQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }


        [Authorize(Policy = "admin.tags.create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.tags.create")]
        public async Task<IActionResult> Create(TagPostCommand command)
        {
            var response = await mediator.Send(command);

            if (!ModelState.IsValid)
            {
                return View(command);
            }

            return RedirectToAction(nameof(Index));
        }


        [Authorize(Policy = "admin.tags.edit")]
        public async Task<IActionResult> Edit(TagGetSingleQuery query)
        {
            var tag = await mediator.Send(query);
            if (tag == null)
            {
                return NotFound();
            }

            var command = new TagPutCommand();
            command.Text = tag.Text;

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.tags.edit")]
        public async Task<IActionResult> Edit(TagPutCommand command)
        {

            var response = await mediator.Send(command);

            if (!ModelState.IsValid)
            {
                return View(command);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.tags.delete")]
        public async Task<IActionResult> DeleteConfirmed(int id, TagRemoveCommand command)
        {
            if (id != command.Id)
            {
                return NotFound();
            }

            var response = await mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return db.Tags.Any(e => e.Id == id);
        }
    }
}
