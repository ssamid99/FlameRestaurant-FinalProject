using FlameRestaurant.Domain.Models.DataContexts;
using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Business.BookModule
{
    public class WishlistQuery : Application.AppCode.Infrastructure.PaginateModel, IRequest<IEnumerable<Product>>
    {
        public class WishlistQueryHandler : IRequestHandler<WishlistQuery, IEnumerable<Product>>
        {
            private readonly FlameRestaurantDbContext db;
            private readonly IActionContextAccessor ctx;

            public WishlistQueryHandler(FlameRestaurantDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            public async Task<IEnumerable<Product>> Handle(WishlistQuery request, CancellationToken cancellationToken)
            {
                var favorites = ctx.ActionContext.HttpContext.Request.Cookies["favorites"]?
                                   .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                   .Where(x => Regex.IsMatch(x, @"\d+"))
                                   .Select(x => int.Parse(x))
                                   .ToArray();

                if (favorites == null || favorites.Length == 0)
                {
                    return Enumerable.Empty<Product>();
                }

                var favs = await db.Products
                    .Where(p => favorites.Contains(p.Id) && p.DeletedByUserId == null)
                    .ToListAsync(cancellationToken);

                return favs;
            }
        }
    }
}
