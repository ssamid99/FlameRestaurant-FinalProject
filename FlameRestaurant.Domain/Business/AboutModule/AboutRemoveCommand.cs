using MediatR;
using Microsoft.EntityFrameworkCore;
using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Business.AboutModule
{
    public class AboutRemoveCommand : IRequest<About>
    {
        public int Id { get; set; }
        public class AboutRemoveCommandHandler : IRequestHandler<AboutRemoveCommand, About>
        {
            private readonly FlameRestaurantDbContext db;

            public AboutRemoveCommandHandler(FlameRestaurantDbContext db)
            {
                this.db = db;
            }
            public async Task<About> Handle(AboutRemoveCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Abouts.FirstOrDefaultAsync(r => r.Id == request.Id && r.DeletedDate == null, cancellationToken);
                if (data == null)
                {
                    return null;
                }
                data.DeletedDate = DateTime.Now.AddHours(4);
                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
