using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Business.BlogPostModule
{
    public class BlogPostClearCommand : IRequest<BlogPost>
    {
        public int Id { get; set; }
        public class BlogPostClearCommandHandler : IRequestHandler<BlogPostClearCommand, BlogPost>
        {
            private readonly FlameRestaurantDbContext db;

            public BlogPostClearCommandHandler(FlameRestaurantDbContext db)
            {
                this.db = db;
            }

            public async Task<BlogPost> Handle(BlogPostClearCommand request, CancellationToken cancellationToken)
            {
                var data = db.BlogPosts.FirstOrDefault(m => m.Id == request.Id && m.DeletedDate != null);

                if (data == null)
                {
                    return null;
                }
                db.BlogPosts.Remove(data);
                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
