using MediatR;
using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Business.BlogPostModule
{
    public class BlogPostPublishCommand : IRequest<BlogPost>
    {
        public int Id { get; set; }
        public class BlogPostPublishCommandHandler : IRequestHandler<BlogPostPublishCommand, BlogPost>
        {
            private readonly FlameRestaurantDbContext db;

            public BlogPostPublishCommandHandler(FlameRestaurantDbContext db)
            {
                this.db = db;
            }

            public async Task<BlogPost> Handle(BlogPostPublishCommand request, CancellationToken cancellationToken)
            {
                var data = db.BlogPosts.FirstOrDefault(m => m.Id == request.Id && m.PublishedDate == null);

                if (data == null)
                {
                    return null;
                }
                data.PublishedDate = DateTime.UtcNow.AddHours(4);
                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
