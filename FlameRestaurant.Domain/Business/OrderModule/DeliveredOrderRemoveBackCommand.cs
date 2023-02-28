using MediatR;
using FlameRestaurant.Domain.Models.DataContexts;
using FlameRestaurant.Domain.Models.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FlameRestaurant.Domain.Models.DbContexts;

namespace FlameRestaurant.Domain.Business.OrderModule
{
    public class DeliveredOrderRemoveBackCommand : IRequest<Order>
    {
        public int Id { get; set; }
        public class DeliveredOrderRemoveBackCommandHandler : IRequestHandler<DeliveredOrderRemoveBackCommand, Order>
        {
            private readonly FlameRestaurantDbContext db;

            public DeliveredOrderRemoveBackCommandHandler(FlameRestaurantDbContext db)
            {
                this.db = db;
            }

            public async Task<Order> Handle(DeliveredOrderRemoveBackCommand request, CancellationToken cancellationToken)
            {
                var data = db.Orders.FirstOrDefault(m => m.Id == request.Id && m.IsDelivered == true);

                if (data == null)
                {
                    return null;
                }

                data.IsDelivered = false;

                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}

