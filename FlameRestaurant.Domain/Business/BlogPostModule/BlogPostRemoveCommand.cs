using MediatR;
using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using FlameRestaurant.Application.AppCode.Extensions;

namespace FlameRestaurant.Domain.Business.BlogPostModule
{
    public class BlogPostRemoveCommand : IRequest<BlogPost>
    {
        public int Id { get; set; }
        public class BlogPostRemoveCommandHandler : IRequestHandler<BlogPostRemoveCommand, BlogPost>
        {
            private readonly FlameRestaurantDbContext db;
            private readonly IActionContextAccessor ctx;

            public BlogPostRemoveCommandHandler(FlameRestaurantDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            public async Task<BlogPost> Handle(BlogPostRemoveCommand request, CancellationToken cancellationToken)
            {
                var data = db.BlogPosts.FirstOrDefault(m => m.Id == request.Id && m.DeletedDate == null);

                if (data == null)
                {
                    return null;
                }
                data.DeletedDate = DateTime.UtcNow.AddHours(4);
                data.DeletedByUserId = ctx.GetCurrentUserId();
                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
