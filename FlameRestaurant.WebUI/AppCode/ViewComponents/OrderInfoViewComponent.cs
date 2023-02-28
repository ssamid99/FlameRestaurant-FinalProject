using FlameRestaurant.Domain.Models.DataContexts;
using FlameRestaurant.Domain.Models.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FlameRestaurant.WebUI.AppCode.ViewComponents
{
    public class OrderInfoViewComponent : ViewComponent
    {
        private readonly FlameRestaurantDbContext db;

        public OrderInfoViewComponent(FlameRestaurantDbContext db)
        {
            this.db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync(string viewName)
        {
            var data = await db.Basket
                .Include(d => d.Product)
                .ToListAsync();

            if (data == null)
            {
                return null;
            }

            return View(await Task.FromResult(data));
        }
    }
}
