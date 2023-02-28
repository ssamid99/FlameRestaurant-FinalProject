using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Business.OrderModule
{
    public class CancelledOrderRemoveBackCommand : IRequest<Order>
    {
        public int Id { get; set; }
        public class CancelledOrderRemoveBackCommandHandler : IRequestHandler<CancelledOrderRemoveBackCommand, Order>
        {
            private readonly FlameRestaurantDbContext db;

            public CancelledOrderRemoveBackCommandHandler(FlameRestaurantDbContext db)
            {
                this.db = db;
            }

            public async Task<Order> Handle(CancelledOrderRemoveBackCommand request, CancellationToken cancellationToken)
            {
                var data = db.Orders.FirstOrDefault(m => m.Id == request.Id && m.DeletedDate != null);

                if (data == null)
                {
                    return null;
                }

                data.DeletedDate = null;
                data.IsDelivered = false;

                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}

