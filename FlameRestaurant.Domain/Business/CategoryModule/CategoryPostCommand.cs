using FlameRestaurant.Application.AppCode.Extensions;
using FlameRestaurant.Domain.Models.DataContexts;
using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Business.CategoryModule
{
    public class CategoryPostCommand : IRequest<Category>
    {
        public string Name { get; set; }

        public int? ParentId { get; set; }

        public class CategoryCreateCommandHandler : IRequestHandler<CategoryPostCommand, Category>
        {
            private readonly FlameRestaurantDbContext db;
            private readonly IActionContextAccessor ctx;

            public CategoryCreateCommandHandler(FlameRestaurantDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            public async Task<Category> Handle(CategoryPostCommand request, CancellationToken cancellationToken)
            {
                if (!ctx.IsValid())
                    return null;

                var entity = new Category();

                entity.CreatedByUserId = ctx.GetCurrentUserId();
                entity.Name = request.Name;
                entity.ParentId = request.ParentId;
                

                await db.Categories.AddAsync(entity, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);

                return entity;
            }
        }
    }

}
