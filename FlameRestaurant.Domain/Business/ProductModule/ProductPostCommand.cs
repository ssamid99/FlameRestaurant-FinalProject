using FlameRestaurant.Application.AppCode.Extensions;
using FlameRestaurant.Domain.Migrations;
using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Business.ProductModule
{
    public class ProductPostCommand : IRequest<Product>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public IFormFile Image { get; set; }

        public string StockKeepingUnit { get; set; }
        public string Description { get; set; }
        public string ReceipeDescription { get; set; }
        public int CategoryId { get; set; }
        public int[] TagIds { get; set; }

        public class ProductPostCommandHandler : IRequestHandler<ProductPostCommand, Product>
        {
            private readonly FlameRestaurantDbContext db;
            private readonly IHostEnvironment env;
            private readonly IConfiguration configuration;
            private readonly IActionContextAccessor ctx;

            public ProductPostCommandHandler(FlameRestaurantDbContext db, IHostEnvironment env, IConfiguration configuration, IActionContextAccessor ctx)
            {
                this.db = db;
                this.env = env;
                this.configuration = configuration;
                this.ctx = ctx;
            }
            public async Task<Product> Handle(ProductPostCommand request, CancellationToken cancellationToken)
            {
                if (!ctx.IsValid())
                    return null;

                var product = new Product();
                product.TagCloud = new List<ProductTagItem>();
                product.CreatedByUserId = ctx.GetCurrentUserId();
                product.Name = request.Name;
                product.StockKeepingUnit = request.StockKeepingUnit;
                product.Price = request.Price;
                product.Description = request.Description;
                product.StockKeepingUnit = request.StockKeepingUnit;
                product.ReceipeDescription = request.ReceipeDescription;
                product.CategoryId = request.CategoryId;

                string extension = Path.GetExtension(request.Image.FileName);//.jpg

                request.ImagePath = $"book-{Guid.NewGuid().ToString().ToLower()}{extension}";

                var folder = configuration["uploads:folder"];

                string fullPath = null;

                if (!string.IsNullOrWhiteSpace(folder))
                {
                    fullPath = folder.GetImagePhysicalPath(request.ImagePath);
                }
                else
                {
                    fullPath = env.GetImagePhysicalPath(request.ImagePath);
                }

                using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    request.Image.CopyTo(fs);
                }

                product.ImagePath = request.ImagePath;

                if (request.TagIds != null)
                {
                    foreach (var exceptedId in request.TagIds)
                    {
                        var tagItem = new ProductTagItem();
                        tagItem.TagId = exceptedId;
                        product.TagCloud.Add(tagItem);
                    }
                }

                await db.Products.AddAsync(product, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);


                return product;
            }
        }
    }
}
