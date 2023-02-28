using FlameRestaurant.Application.AppCode.Extensions;
using FlameRestaurant.Application.AppCode.Extenstions;
using FlameRestaurant.Application.AppCode.Infrastructure;
using FlameRestaurant.Domain.Migrations;
using FlameRestaurant.Domain.Models.DataContexts;
using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Business.ProductModule
{
    public class AddToBasketCommand : IRequest<JsonResponse>
    {
        public int ProductId { get; set; }

        public class AddToBasketCommandHandler : IRequestHandler<AddToBasketCommand, JsonResponse>
        {
            private readonly FlameRestaurantDbContext db;
            private readonly IActionContextAccessor ctx;

            public AddToBasketCommandHandler(FlameRestaurantDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }


            public async Task<JsonResponse> Handle(AddToBasketCommand request, CancellationToken cancellationToken)
            {
                var userId = ctx.GetCurrentUserId();

                var alreadyExists = await db.Basket.AnyAsync(b => b.ProductId == request.ProductId && b.UserId == userId, cancellationToken);

                if (alreadyExists)
                {
                    return new JsonResponse
                    {
                        Error = true,
                        Message = "Already added to basket"
                    };
                }


                var basketItem = new Basket
                {
                    UserId = userId,
                    ProductId = request.ProductId,
                    Quantity = 1
                };

                await db.Basket.AddAsync(basketItem, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);

                var value = ctx.GetIntArrayFromCookie("favorites");

                if (value != null)
                {
                    ctx.SetValueToCookie("favorites", value.Where(e => e != request.ProductId).ToArray());
                }


                return new JsonResponse
                {
                    Error = false,
                    Message = "Added to cart"
                };
            }
        }
    }
}
