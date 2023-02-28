using FlameRestaurant.Domain.Business.OrderModule;
using FlameRestaurant.Domain.Models.DataContexts;
using FlameRestaurant.Domain.Models.DbContexts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace FlameRestaurant.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly FlameRestaurantDbContext db;
        private readonly IMediator mediator;

        public OrdersController(FlameRestaurantDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }

        [Authorize(Policy = "admin.orders.index")]
        public async Task<IActionResult> Index(OrderGetAllQuery query)
        {
            var response = await mediator.Send(query);

            if (response == null)
            {
                return NotFound();
            }

            return View(response);
        }

        [Authorize(Policy = "admin.orders.details")]
        public async Task<IActionResult> Details(OrderGetSingleQuery query)
        {
            var response = await mediator.Send(query);

            if (response == null)
            {
                return NotFound();
            }

            return View(response);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.orders.delete")]
        public async Task<IActionResult> DeleteConfirmed(OrderRemoveCommand command)
        {
            var response = await mediator.Send(command);

            if (response == null)
            {
                return NotFound();
            }


            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.orders.completeorder")]
        public async Task<IActionResult> CompleteOrder(OrderCompleteCommand command)
        {
            var response = await mediator.Send(command);

            if (response == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = "admin.orders.deliveredorders")]
        public async Task<IActionResult> DeliveredOrders(OrderGetAllDeliveredQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }

        [Authorize(Policy = "admin.orders.cancelledorders")]
        public async Task<IActionResult> CancelledOrders(OrderGetAllCancelledQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.orders.cancelledordersbacktoindex")]
        public async Task<IActionResult> CancelledOrdersBackToIndex(CancelledOrderRemoveBackCommand command)
        {
            var response = await mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.orders.deliveredordersbacktoindex")]
        public async Task<IActionResult> DeliveredOrdersBackToIndex(DeliveredOrderRemoveBackCommand command)
        {
            var response = await mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Clear")]
        [Authorize(Policy = "admin.orders.clearcancelledorders")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearCancelledOrders(int id, OrderClearCommand command)
        {
            if (id != command.Id)
            {
                return NotFound();
            }

            var response = await mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Any(e => e.Id == id);
        }
    }
}
