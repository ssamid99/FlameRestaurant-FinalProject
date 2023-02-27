using FlameRestaurant.Application.AppCode.Extensions;
using FlameRestaurant.Application.AppCode.Extenstions;
using FlameRestaurant.Application.AppCode.Infrastructure;
using FlameRestaurant.Domain.Models.DataContexts;
using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Business.ProductModule
{
    public class ProductPutCommand : IRequest<JsonResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public IFormFile Image { get; set; }

        public string StockKeepingUnit { get; set; }
        public string Description { get; set; }
        public string ReceipeDescription { get; set; }
        public int CategoryId { get; set; }

        public class BookEditCommandHandler : IRequestHandler<ProductPutCommand, JsonResponse>
        {
            private readonly FlameRestaurantDbContext db;
            private readonly IHostEnvironment env;
            private readonly IActionContextAccessor ctx;

            public BookEditCommandHandler(FlameRestaurantDbContext db, IHostEnvironment env, IActionContextAccessor ctx)
            {
                this.db = db;
                this.env = env;
                this.ctx = ctx;
            }
            public async Task<JsonResponse> Handle(ProductPutCommand request, CancellationToken cancellationToken)
            {

                var product = await db.Products
                    .FirstOrDefaultAsync(bp => bp.Id == request.Id && bp.DeletedDate == null);
                if (product == null)
                {
                    throw new Exception("Book not found");
                }

                product.Name = request.Name;
                product.StockKeepingUnit = request.StockKeepingUnit;
                product.Price = request.Price;
                product.Description = request.Description;
                product.StockKeepingUnit = request.StockKeepingUnit;
                product.ReceipeDescription = request.ReceipeDescription;
                product.CategoryId = request.CategoryId;

                if (request.Image == null)
                    goto end;

                string extension = Path.GetExtension(request.Image.FileName);//.jpg

                request.ImagePath = $"book-{Guid.NewGuid().ToString().ToLower()}{extension}";
                string fullPath = env.GetImagePhysicalPath(request.ImagePath);

                using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    request.Image.CopyTo(fs);
                }

                env.ArchiveImages(product.ImagePath);

                product.ImagePath = request.ImagePath;

            end:

                await db.SaveChangesAsync(cancellationToken);


                return new JsonResponse
                {
                    Error = false,
                    Message = "Success"
                };
            }
        }
    }
}
