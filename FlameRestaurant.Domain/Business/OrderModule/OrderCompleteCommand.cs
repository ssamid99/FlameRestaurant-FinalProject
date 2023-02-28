using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Business.OrderModule
{
    public class OrderCompleteCommand : IRequest<Order>
    {
        public int Id { get; set; }

        public class OrderCompleteCommandHandler : IRequestHandler<OrderCompleteCommand, Order>
        {
            private readonly FlameRestaurantDbContext db;

            public OrderCompleteCommandHandler(FlameRestaurantDbContext db)
            {
                this.db = db;
            }

            public async Task<Order> Handle(OrderCompleteCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Orders.FirstOrDefaultAsync(o => o.Id == request.Id && o.DeletedDate == null);

                if (data == null)
                {
                    return null;
                }

                data.IsDelivered = true;
                await db.SaveChangesAsync(cancellationToken);

                return data;
            }
        }

    }
}
