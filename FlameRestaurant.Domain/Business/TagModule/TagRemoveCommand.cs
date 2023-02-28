using FlameRestaurant.Application.AppCode.Extensions;
using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Business.TagModule
{
    public class TagRemoveCommand : IRequest<Tag>
    {
        public int Id { get; set; }
        public class TagRemoveCommandHandler : IRequestHandler<TagRemoveCommand, Tag>
        {
            private readonly FlameRestaurantDbContext db;
            private readonly IActionContextAccessor ctx;

            public TagRemoveCommandHandler(FlameRestaurantDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }
            public async Task<Tag> Handle(TagRemoveCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Tags.FirstOrDefaultAsync(r => r.Id == request.Id && r.DeletedDate == null, cancellationToken);
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
