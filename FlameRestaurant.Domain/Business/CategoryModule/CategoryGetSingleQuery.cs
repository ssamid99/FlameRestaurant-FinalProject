using FlameRestaurant.Domain.Models.DataContexts;
using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Business.CategoryModule
{
    public class CategoryGetSingleQuery : IRequest<Category>
    {
        public int Id { get; set; }

        public class CategoryGetSingleQueryHandler : IRequestHandler<CategoryGetSingleQuery, Category>
        {
            private readonly FlameRestaurantDbContext db;

            public CategoryGetSingleQueryHandler(FlameRestaurantDbContext db)
            {
                this.db = db;
            }

            public async Task<Category> Handle(CategoryGetSingleQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Categories
                    .Include(c => c.Parent)
                    .FirstOrDefaultAsync(p => p.Id == request.Id);

                return data;
            }
        }

    }
}
