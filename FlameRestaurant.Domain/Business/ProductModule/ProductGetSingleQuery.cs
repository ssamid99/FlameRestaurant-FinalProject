using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Business.ProductModule
{
    public class ProductGetSingleQuery : IRequest<Product>
    {
        public int Id { get; set; }

        public class ProductGetSingleQueryHandler : IRequestHandler<ProductGetSingleQuery, Product>
        {
            private readonly FlameRestaurantDbContext db;

            public ProductGetSingleQueryHandler(FlameRestaurantDbContext db)
            {
                this.db = db;
            }
            public async Task<Product> Handle(ProductGetSingleQuery request, CancellationToken cancellationToken)
            {
                var book = await db.Products
                    .Include(b => b.Category)
                    .ThenInclude(bc=>bc.Parent)
                    .Include(bp => bp.TagCloud)
                    .ThenInclude(bp => bp.Tag)
                    .FirstOrDefaultAsync(p => p.Id == request.Id);

                return book;
            }
        }
    }
}
