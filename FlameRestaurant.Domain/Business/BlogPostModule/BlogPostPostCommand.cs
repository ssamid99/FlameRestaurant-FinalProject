using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using FlameRestaurant.Application.AppCode.Extensions;
using FlameRestaurant.Application.AppCode.Infrastructure;
using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace FlameRestaurant.Domain.Business.BlogPostModule
{
    public class BlogPostPostCommand : IRequest<JsonResponse>
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }

        public class BlogPostPostCommandHandler : IRequestHandler<BlogPostPostCommand, JsonResponse>
        {
            private readonly FlameRestaurantDbContext db;
            private readonly IHostEnvironment env;
            private readonly IActionContextAccessor ctx;

            public BlogPostPostCommandHandler(FlameRestaurantDbContext db, IHostEnvironment env, IActionContextAccessor ctx)
            {
                this.db = db;
                this.env = env;
                this.ctx = ctx;
            }
            public async Task<JsonResponse> Handle(BlogPostPostCommand request, CancellationToken cancellationToken)
            {
                var entity = new BlogPost();

                entity.Title = request.Title;
                entity.Body = request.Body;
                entity.CreatedByUserId = ctx.GetCurrentUserId();

                if (request.Image == null)
                    goto end;

                string extension = Path.GetExtension(request.Image.FileName);//.jpg

                request.ImagePath = $"blogpost-{Guid.NewGuid().ToString().ToLower()}{extension}";
                string fullPath = env.GetImagePhysicalPath(request.ImagePath);

                using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    request.Image.CopyTo(fs);
                }

                entity.ImagePath = request.ImagePath;

            end:
                entity.Slug = request.Title.ToSlug();

                await db.BlogPosts.AddAsync(entity, cancellationToken);
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
