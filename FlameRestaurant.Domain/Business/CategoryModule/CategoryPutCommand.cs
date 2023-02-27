using FlameRestaurant.Domain.Models.DataContexts;
using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Business.CategoryModule
{

    public class CategoryPutCommand : IRequest<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public class CategoryEditCommandHandler : IRequestHandler<CategoryPutCommand, Category>
        {
            private readonly FlameRestaurantDbContext db;

            public CategoryEditCommandHandler(FlameRestaurantDbContext db)
            {
                this.db = db;
            }

            public async Task<Category> Handle(CategoryPutCommand request, CancellationToken cancellationToken)
            {
                var entity = await db.Categories
                       .Include(c => c.Parent)
                       .FirstOrDefaultAsync(bp => bp.Id == request.Id && bp.DeletedDate == null);
                if (entity == null)
                {
                    return null;
                }

                entity.Name = request.Name;
                entity.ParentId = request.ParentId;


                await db.SaveChangesAsync(cancellationToken);

                return entity;
            }


        }
    }
}
