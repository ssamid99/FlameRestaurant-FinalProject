using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class OrderGetSingleQuery : IRequest<Order>
    {
        public int Id { get; set; }

        public class OrderGetSingleQueryHandler : IRequestHandler<OrderGetSingleQuery, Order>
        {
            private readonly FlameRestaurantDbContext db;

            public OrderGetSingleQueryHandler(FlameRestaurantDbContext db)
            {
                this.db = db;
            }

            public async Task<Order> Handle(OrderGetSingleQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Orders
                    .Include(o => o.OrderProducts.Where(op => op.DeletedDate == null))
                    .ThenInclude(op => op.Product)
                    .FirstOrDefaultAsync(p => p.Id == request.Id);

                return data;
            }
        }

    }
}
