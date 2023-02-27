using FlameRestaurant.Application.AppCode.Infrastructure;
using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Business.ProductModule
{
    public class ProductGetAllQuery : PaginateModel, IRequest<PagedViewModel<Product>>
    {
        public class ProductGetAllQueryHandler : IRequestHandler<ProductGetAllQuery, PagedViewModel<Product>>
        {
            private readonly FlameRestaurantDbContext db;

            public ProductGetAllQueryHandler(FlameRestaurantDbContext db)
            {
                this.db = db;
            }

            public async Task<PagedViewModel<Product>> Handle(ProductGetAllQuery request, CancellationToken cancellationToken)
            {
                if (request.PageSize < 12)
                {
                    request.PageSize = 12;
                }
                var query = db.Products
                    .Include(p => p.Category)
                    .Where(m => m.DeletedDate == null)
                    .OrderByDescending(p => p.Id)
                    .AsQueryable();

                var pagedDate = new PagedViewModel<Product>(query, request);

                return pagedDate;
            }
        }
    }
}
