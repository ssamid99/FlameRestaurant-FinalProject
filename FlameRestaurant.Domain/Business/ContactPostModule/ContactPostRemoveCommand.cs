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
using Microsoft.AspNetCore.Mvc.Infrastructure;
using FlameRestaurant.Application.AppCode.Extensions;

namespace FlameRestaurant.Domain.Business.ContactPostModule
{
    public class ContactPostRemoveCommand : IRequest<ContactPost>
    {
        public int Id { get; set; }
        public class ContactPostRemoveCommandHandler : IRequestHandler<ContactPostRemoveCommand, ContactPost>
        {
            private readonly FlameRestaurantDbContext db;
            private readonly IActionContextAccessor ctx;

            public ContactPostRemoveCommandHandler(FlameRestaurantDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }
            public async Task<ContactPost> Handle(ContactPostRemoveCommand request, CancellationToken cancellationToken)
            {
                var data = await db.ContactPosts.FirstOrDefaultAsync(c => c.Id == request.Id && c.DeletedDate == null, cancellationToken);
                if(data == null)
                {
                    return null;
                }
                data.DeletedDate = DateTime.Now.AddHours(4);
                data.DeletedByUserId = ctx.GetCurrentUserId();
                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
