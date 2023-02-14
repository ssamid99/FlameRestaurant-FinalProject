using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using FlameRestaurant.Application.AppCode.Extensions;
using FlameRestaurant.Application.AppCode.Infrastructure;
using FlameRestaurant.Domain.Models.DbContexts;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Business.BlogPostModule
{
    public class BlogPostPutCommand : IRequest<JsonResponse>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }

        public class BlogPostPutCommandHandler : IRequestHandler<BlogPostPutCommand, JsonResponse>
        {
            private readonly FlameRestaurantDbContext db;
            private readonly IHostEnvironment env;

            public BlogPostPutCommandHandler(FlameRestaurantDbContext db, IHostEnvironment env)
            {
                this.db = db;
                this.env = env;
            }
            public async Task<JsonResponse> Handle(BlogPostPutCommand request, CancellationToken cancellationToken)
            {
                var entity = db.BlogPosts.FirstOrDefault(bg => bg.Id == request.Id && bg.DeletedDate == null);


                if (entity == null)
                {
                    return null;
                }

                entity.Title = request.Title;
                entity.Body = request.Body;

                if (request.Image == null)
                    goto end;

                string extension = Path.GetExtension(request.Image.FileName); //.jpg-ni goturur
                request.ImagePath = $"blogpost-{Guid.NewGuid().ToString().ToLower()}{extension}";

                string fullPath = env.GetImagePhysicalPath(request.ImagePath);

                using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    request.Image.CopyTo(fs);
                }

                string oldPath = env.GetImagePhysicalPath(entity.ImagePath);


                System.IO.File.Move(oldPath, env.GetImagePhysicalPath($"archive{DateTime.Now:yyyyMMdd}-{entity.ImagePath}"));

                entity.ImagePath = request.ImagePath;

            end:
                if (string.IsNullOrWhiteSpace(entity.Slug))
                {
                    entity.Slug = request.Title.ToSlug();
                }

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
