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
    public class OrderGetAllCancelledQuery : IRequest<List<Order>>
    {
        public class OrderGetAllCancelledHandler : IRequestHandler<OrderGetAllCancelledQuery, List<Order>>
        {
            private readonly FlameRestaurantDbContext db;

            public OrderGetAllCancelledHandler(FlameRestaurantDbContext db)
            {
                this.db = db;
            }


            public async Task<List<Order>> Handle(OrderGetAllCancelledQuery request, CancellationToken cancellationToken)
            {
                var completedOrders = await db.Orders
                    .Where(o => o.DeletedDate != null)
                    .ToListAsync(cancellationToken);


                return completedOrders;
            }
        }
    }
}
