using FlameRestaurant.Domain.Business.ProductModule;
using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FlameRestaurant.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly FlameRestaurantDbContext db;
        private readonly IMediator mediator;

        public ProductsController(FlameRestaurantDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }

        [Authorize(Policy = "admin.products.index")]
        public async Task<IActionResult> Index(ProductGetAllQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }


        [Authorize(Policy = "admin.products.details")]
        public async Task<IActionResult> Details(ProductGetSingleQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }


        [Authorize(Policy = "admin.products.create")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(db.Categories.Where(a => a.DeletedDate == null), "Id", "Name");
            ViewBag.Tags = new SelectList(db.Tags.Where(t => t.DeletedDate == null).ToList(), "Id", "Text");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Policy = "admin.products.create")]
        public async Task<IActionResult> Create(ProductPostCommand command)
        {
            var response = await mediator.Send(command);

            if (!ModelState.IsValid)
            {
                ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", command.CategoryId);
                ViewBag.Tags = new SelectList(db.Tags.Where(t => t.DeletedDate == null).ToList(), "Id", "Text");
                return View(command);
            }

            return RedirectToAction(nameof(Index));
        }


        [Authorize(Policy = "admin.products.edit")]
        public async Task<IActionResult> Edit(ProductGetSingleQuery query)
        {
            var product = await mediator.Send(query);
            if (product == null)
            {
                return NotFound();
            }

            var command = new ProductPutCommand();
            command.Name = product.Name;
            command.Price = product.Price;
            command.StockKeepingUnit = product.StockKeepingUnit;
            command.Description = product.Description;
            command.ReceipeDescription = product.ReceipeDescription;
            command.CategoryId = product.CategoryId;
            command.ImagePath = product.ImagePath;
            command.TagIds = product.TagCloud.Select(tc => tc.TagId).ToArray();

            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name");
            ViewBag.Tags = new SelectList(db.Tags.Where(t => t.DeletedDate == null).ToList(), "Id", "Text");
            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.products.edit")]
        public async Task<IActionResult> Edit(ProductPutCommand command)
        {
            
            var response = await mediator.Send(command);

            if (!ModelState.IsValid)
            {
                ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name");
                ViewBag.Tags = new SelectList(db.Tags.Where(t => t.DeletedDate == null).ToList(), "Id", "Text");
                return View(command);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.products.delete")]
        public async Task<IActionResult> DeleteConfirmed(int id, ProductRemoveCommand command)
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
            return db.Products.Any(e => e.Id == id);
        }
    }
}
