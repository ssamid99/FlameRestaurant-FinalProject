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

namespace FlameRestaurant.Domain.Business.AboutModule.Teams
{
    public class TeamPostCommand : IRequest<JsonResponse>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Text { get; set; }
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }

        public class TeamPostCommandHandler : IRequestHandler<TeamPostCommand, JsonResponse>
        {
            private readonly FlameRestaurantDbContext db;
            private readonly IHostEnvironment env;
            private readonly IActionContextAccessor ctx;

            public TeamPostCommandHandler(FlameRestaurantDbContext db, IHostEnvironment env, IActionContextAccessor ctx)
            {
                this.db = db;
                this.env = env;
                this.ctx = ctx;
            }
            public async Task<JsonResponse> Handle(TeamPostCommand request, CancellationToken cancellationToken)
            {
                var entity = new Team();

                entity.Name = request.Name;
                entity.Surname = request.Surname;
                entity.Text = request.Text;
                entity.CreatedByUserId = ctx.GetCurrentUserId();

                if (request.Image == null)
                    goto end;

                string extension = Path.GetExtension(request.Image.FileName);//.jpg

                request.ImagePath = $"team-{Guid.NewGuid().ToString().ToLower()}{extension}";
                string fullPath = env.GetImagePhysicalPath(request.ImagePath);

                using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    request.Image.CopyTo(fs);
                }

                entity.ImagePath = request.ImagePath;

            end:

                await db.Teams.AddAsync(entity, cancellationToken);
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
