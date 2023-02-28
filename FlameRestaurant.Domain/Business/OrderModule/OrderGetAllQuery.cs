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

    public class OrderGetAllQuery : IRequest<List<Order>>
    {
        public class OrderGetAllQueryHandler : IRequestHandler<OrderGetAllQuery, List<Order>>
        {
            private readonly FlameRestaurantDbContext db;

            public OrderGetAllQueryHandler(FlameRestaurantDbContext db)
            {
                this.db = db;
            }
            public async Task<List<Order>> Handle(OrderGetAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Orders
                .Where(o => o.DeletedDate == null && o.IsDelivered == false)
                .ToListAsync(cancellationToken);

                return data;
            }
        }


    }
}
