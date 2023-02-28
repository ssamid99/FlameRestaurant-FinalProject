using MediatR;
using FlameRestaurant.Domain.Models.DataContexts;
using FlameRestaurant.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlameRestaurant.Domain.Models.DbContexts;

namespace FlameRestaurant.Domain.Business.OrderModule
{

    public class OrderRemoveCommand : IRequest<Order>
    {
        public int Id { get; set; }

        public class OrderRemoveCommandHandler : IRequestHandler<OrderRemoveCommand, Order>
        {
            private readonly FlameRestaurantDbContext db;

            public OrderRemoveCommandHandler(FlameRestaurantDbContext db)
            {
                this.db = db;
            }

            public async Task<Order> Handle(OrderRemoveCommand request, CancellationToken cancellationToken)
            {
                var data = db.Orders.FirstOrDefault(m => m.Id == request.Id && m.DeletedDate == null);

                if (data == null)
                {
                    return null;
                }

                data.DeletedDate = DateTime.UtcNow.AddHours(4);

                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
