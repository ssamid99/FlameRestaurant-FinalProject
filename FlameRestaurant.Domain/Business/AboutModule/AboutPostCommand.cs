using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Hosting;
using FlameRestaurant.Application.AppCode.Extensions;
using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Business.AboutModule
{
    public class AboutPostCommand : IRequest<About>
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public class AboutPostCommandHandler : IRequestHandler<AboutPostCommand, About>
        {
            private readonly FlameRestaurantDbContext db;
            private readonly IHostEnvironment env;
            private readonly IActionContextAccessor ctx;

            public AboutPostCommandHandler(FlameRestaurantDbContext db, IHostEnvironment env, IActionContextAccessor ctx)
            {
                this.db = db;
                this.env = env;
                this.ctx = ctx;
            }
            public async Task<About> Handle(AboutPostCommand request, CancellationToken cancellationToken)
            {
                try
                {

                    var entity = new About();
                    entity.Title = request.Title;
                    entity.Text = request.Text;
                    entity.CreatedByUserId = ctx.GetCurrentUserId();
                    
                    await db.Abouts.AddAsync(entity, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);
                    return entity;
                }
                catch (System.Exception)
                {
                    return null;
                }
            }
        }
    }
}
