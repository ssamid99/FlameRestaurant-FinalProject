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
using Microsoft.AspNetCore.Identity;
using FlameRestaurant.Domain.Models.FormData;

namespace FlameRestaurant.Domain.Business.ContactPostModule
{
    public class ContactPostPostCommand : IRequest<ContactPost>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public class ContactPostPostCommandHandler : IRequestHandler<ContactPostPostCommand, ContactPost>
        {
            private readonly FlameRestaurantDbContext db;
            private readonly IActionContextAccessor ctx;

            public ContactPostPostCommandHandler(FlameRestaurantDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }
            public async Task<ContactPost> Handle(ContactPostPostCommand request, CancellationToken cancellationToken)
            {
                var data = new ContactPost();
                data.Name = request.Name;
                data.Email = request.Email;
                data.Message = request.Message;
                await db.ContactPosts.AddAsync(data, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
