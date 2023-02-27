using FlameRestaurant.Application.AppCode.Extensions;
using FlameRestaurant.Application.AppCode.Infrastructure;
using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Business.ProductModule
{
    public class SetRateCommand : IRequest<JsonResponse>
    {
        public int ProductId { get; set; }
        public byte Rate { get; set; }

        public class SetRateCommandHandler : IRequestHandler<SetRateCommand, JsonResponse>
        {
            private readonly FlameRestaurantDbContext db;
            private readonly IActionContextAccessor ctx;

            public SetRateCommandHandler(FlameRestaurantDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }
            public async Task<JsonResponse> Handle(SetRateCommand request, CancellationToken cancellationToken)
            {
                var userId = ctx.GetCurrentUserId();

                var rateEntry = await db.ProductRates.FirstOrDefaultAsync(m => m.ProductId == request.ProductId && m.CreatedByUserId == userId, cancellationToken);

                if (rateEntry != null)
                {
                    rateEntry.Rate = request.Rate;
                    rateEntry.CreatedDate = DateTime.UtcNow.AddHours(4);
                    await db.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    rateEntry = new ProductRate
                    {
                        ProductId = request.ProductId,
                        Rate = request.Rate,
                        CreatedByUserId = userId,
                        CreatedDate = DateTime.UtcNow.AddHours(4)
                    };
                    await db.ProductRates.AddAsync(rateEntry, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);
                };

                var avgRate = db.ProductRates.Where(m => m.ProductId == request.ProductId).Average(m => m.Rate);
                var book = await db.Products.FirstOrDefaultAsync(m => m.Id == request.ProductId, cancellationToken);

                book.Rate = avgRate;
                await db.SaveChangesAsync(cancellationToken);

                return new JsonResponse
                {
                    Error = false,
                    Message = "Okay",
                    Value = avgRate
                };
            }
        }
    }
}
