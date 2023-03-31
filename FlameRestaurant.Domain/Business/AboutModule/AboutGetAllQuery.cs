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
    public class AboutGetAllQuery : IRequest<About>
    {
        public class AboutGetAllQueryHandler : IRequestHandler<AboutGetAllQuery, About>
        {
            private readonly FlameRestaurantDbContext db;

            public AboutGetAllQueryHandler(FlameRestaurantDbContext db)
            {
                this.db = db;
            }
            public async Task<About> Handle(AboutGetAllQuery request, CancellationToken cancellationToken)
            {
                var query = db.Abouts
                    .AsQueryable();
                if(query == null)
                {
                    return null;
                }
                return await query.FirstOrDefaultAsync(r=>r.DeletedDate == null, cancellationToken);
            }
        }
    }
}
