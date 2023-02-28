using FlameRestaurant.Application.AppCode.Infrastructure;
using FlameRestaurant.Domain.Models.DataContexts;
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
    public class ProductFilterQuery : PaginateModel, IRequest<PagedViewModel<Product>>
    {
        public int [] Categories { get; set; }

        public int Min { get; set; }
        public int Max { get; set; }

        public class ProductFilterQueryHandler : IRequestHandler<ProductFilterQuery, PagedViewModel<Product>>
        {
            private readonly FlameRestaurantDbContext db;

            public ProductFilterQueryHandler(FlameRestaurantDbContext db)
            {
                this.db = db;
            }

            public async Task<PagedViewModel<Product>> Handle(ProductFilterQuery request, CancellationToken cancellationToken)
            {
                if (request.PageSize < 12)
                {
                    request.PageSize = 12;
                }

                var query = db.Products.AsQueryable();


                if (request.Categories != null && request.Categories.Length > 0)
                {
                    query = query.Where(q => request.Categories.Contains(q.CategoryId));
                }

                var productIds = await query.Select(q => q.Id)
                    .Distinct()
                    .ToArrayAsync(cancellationToken);

                var productQuery = db.Products
                    .Where(p => productIds.Contains(p.Id))
                    .AsQueryable();

                if (request.Min > 0 && request.Min <= request.Max)
                {
                    productQuery = productQuery.Where(q => q.Price >= request.Min && q.Price <= request.Max);
                }

                productQuery = productQuery
                    .Include(p => p.Category)

                    .OrderByDescending(q => q.Id);

                var pagedModel = new PagedViewModel<Product>(productQuery, request);

                return pagedModel;
            }
        }
    }
}
