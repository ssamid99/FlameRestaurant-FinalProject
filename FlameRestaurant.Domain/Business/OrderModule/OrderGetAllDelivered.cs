using MediatR;
using Microsoft.EntityFrameworkCore;
using FlameRestaurant.Domain.Models.DataContexts;
using FlameRestaurant.Domain.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FlameRestaurant.Domain.Models.DbContexts;

namespace FlameRestaurant.Domain.Business.OrderModule
{
    public class OrderGetAllDeliveredQuery : IRequest<List<Order>>
    {
        public class OrderGetAllDeliveredHandler : IRequestHandler<OrderGetAllDeliveredQuery, List<Order>>
        {
            private readonly FlameRestaurantDbContext db;

            public OrderGetAllDeliveredHandler(FlameRestaurantDbContext db)
            {
                this.db = db;
            }


            public async Task<List<Order>> Handle(OrderGetAllDeliveredQuery request, CancellationToken cancellationToken)
            {
                var completedOrders = await db.Orders
                    .Where(o => o.DeletedDate == null && o.IsDelivered == true)
                    .ToListAsync(cancellationToken);


                return completedOrders;
            }
        }
    }
}
