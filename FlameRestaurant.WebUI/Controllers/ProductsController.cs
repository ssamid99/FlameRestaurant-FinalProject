using FlameRestaurant.Application.AppCode.Extensions;
using FlameRestaurant.Domain.Business.BookModule;
using FlameRestaurant.Domain.Business.ProductModule;
using FlameRestaurant.Domain.Models;
using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using FlameRestaurant.Domain.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlameRestaurant.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly FlameRestaurantDbContext db;
        private readonly IMediator mediator;

        public ProductsController(FlameRestaurantDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(ProductFilterQuery query)
        {
            var response = await mediator.Send(query);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Products", response);
            }

            return View(response);
        }


        [AllowAnonymous]
        public async Task<IActionResult> Details(ProductGetSingleQuery query)
        {
            var book = await mediator.Send(query);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }


        public async Task<IActionResult> Wishlist(WishlistQuery query)
        {
            var favs = await mediator.Send(query);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_WishlistBody", favs);
            }

            return View(favs);
        }


        [HttpGet]
        [ActionName("Basket")]
        //[Route("/basket")]
        public async Task<IActionResult> Basket(ProductBasketQuery query)
        {
            var response = await mediator.Send(query);

            return View(response);
        }

        [HttpPost]
        //[Route("/basket")]
        public async Task<IActionResult> Basket(AddToBasketCommand command)
        {
            var response = await mediator.Send(command);

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromBasket(RemoveFromBasketCommand command)
        {
            var response = await mediator.Send(command);

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeBasketQuantity(ChangeBasketQuantityCommand command)
        {
            var response = await mediator.Send(command);

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> SetProductRate(SetRateCommand command)
        {
            var response = await mediator.Send(command);

            return Json(response);
        }

        [HttpGet]
        //[Route("/checkout")]
        public async Task<IActionResult> Checkout(ProductBasketQuery query)
        {
            var response = await mediator.Send(query);

            return View(new OrderViewModel
            {
                BasketProducts = response
            });
        }

        [HttpPost]
        //[Route("/checkout")]
        public async Task<IActionResult> Checkout(OrderViewModel vm, int[] productIds, int[] quantities)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(vm.OrderDetails);
                await db.SaveChangesAsync();

                vm.OrderDetails.OrderProducts = new List<OrderProduct>();

                for (int i = 0; i < productIds.Length; i++)
                {
                    var product = db.Products.Find(productIds[i]);
                    vm.OrderDetails.OrderProducts.Add(new OrderProduct
                    {
                        OrderId = vm.OrderDetails.Id,
                        ProductId = product.Id,
                        Quantity = quantities[i]
                    });
                }
                await db.SaveChangesAsync();

                var response = new
                {
                    error = false,
                    message = "Your order was completed"
                };

                return Json(response);
            }

            var responseError = new
            {
                error = true,
                message = "The error was occurred while completing your order",
                state = ModelState.GetErrors()
            };
            return Json(responseError);
        }
    }
}
