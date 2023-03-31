using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using FlameRestaurant.Application.AppCode.Extensions;
using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Business.AboutModule
{
    public class AboutPutCommand : IRequest<About>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public class AboutPutCommandHandler : IRequestHandler<AboutPutCommand, About>
        {
            private readonly FlameRestaurantDbContext db;
            private readonly IHostEnvironment env;
            private readonly IActionContextAccessor ctx;

            public AboutPutCommandHandler(FlameRestaurantDbContext db, IHostEnvironment env, IActionContextAccessor ctx)
            {
                this.db = db;
                this.env = env;
                this.ctx = ctx;
            }
            public async Task<About> Handle(AboutPutCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var entity = await db.Abouts.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

                    if (entity == null)
                    {
                        return null;
                    }

                    entity.Title = request.Title;
                    entity.Text = request.Text;
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
