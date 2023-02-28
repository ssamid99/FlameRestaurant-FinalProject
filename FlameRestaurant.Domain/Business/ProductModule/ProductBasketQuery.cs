using FlameRestaurant.Application.AppCode.Extensions;
using FlameRestaurant.Application.AppCode.Extenstions;
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
using System.Threading;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Business.ProductModule
{
    public class ProductBasketQuery : IRequest<IEnumerable<Basket>>
    {
        public class ProductBasketQueryHandler : IRequestHandler<ProductBasketQuery, IEnumerable<Basket>>
        {
            private readonly FlameRestaurantDbContext db;
            private readonly IActionContextAccessor ctx;

            public ProductBasketQueryHandler(FlameRestaurantDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }
            public async Task<IEnumerable<Basket>> Handle(ProductBasketQuery request, CancellationToken cancellationToken)
            {
                if (!ctx.ActionContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    return Enumerable.Empty<Basket>();
                }

                var userId = ctx.GetCurrentUserId();

                var data = await db.Basket
                    .Include(b => b.Product)

                    .Where(b => b.UserId == userId)
                    .ToListAsync(cancellationToken);

                return data;
            }
        }
    }
}
