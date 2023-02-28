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

namespace FlameRestaurant.Domain.Business.TagModule
{
    public class TagGetSingleQuery : IRequest<Tag>
    {
        public int Id { get; set; }
        public class TagGetSingleQueryHandler : IRequestHandler<TagGetSingleQuery, Tag>
        {
            private readonly FlameRestaurantDbContext db;

            public TagGetSingleQueryHandler(FlameRestaurantDbContext db)
            {
                this.db = db;
            }
            public async Task<Tag> Handle(TagGetSingleQuery request, CancellationToken cancellationToken)
            {
                var query = await db.Tags.FirstOrDefaultAsync(t => t.Id == request.Id && t.DeletedDate == null, cancellationToken);
                if(query == null)
                {
                    return null;
                }

                return query;
            }
        }
    }
}
