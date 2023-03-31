using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Business.ProductModule
{
    public class RelatedProductsQuery : IRequest<List<Product>>
    {
        public string Category { get; set; }


        public class RelatedProductsQueryHandler : IRequestHandler<RelatedProductsQuery, List<Product>>
        {
            private readonly FlameRestaurantDbContext db;

            public RelatedProductsQueryHandler(FlameRestaurantDbContext db)
            {
                this.db = db;
            }

            public async Task<List<Product>> Handle(RelatedProductsQuery request, CancellationToken cancellationToken)
            {

                var books = await db.Products
                    .Include(b => b.Category)
                    .Where(b => b.DeletedDate == null && b.Category.Name.Equals(request.Category))
                    .ToListAsync(cancellationToken);

                return books;
            }
        }
    }
}
