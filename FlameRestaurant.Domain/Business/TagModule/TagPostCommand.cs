using FlameRestaurant.Application.AppCode.Extensions;
using FlameRestaurant.Application.AppCode.Infrastructure;
using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Business.TagModule
{
    public class TagPostCommand : IRequest<JsonResponse>
    {
        public string Text { get; set; }
        public class TagPostCommandHandler : IRequestHandler<TagPostCommand, JsonResponse>
        {
            private readonly FlameRestaurantDbContext db;
            private readonly IActionContextAccessor ctx;

            public TagPostCommandHandler(FlameRestaurantDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }
            public async Task<JsonResponse> Handle(TagPostCommand request, CancellationToken cancellationToken)
            {
                var data = new Tag();
                data.Text = request.Text;
                data.CreatedByUserId = ctx.GetCurrentUserId();
                await db.Tags.AddAsync(data, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
                return new JsonResponse
                {
                    Error = false,
                    Message = "Success"
                };
            }
        }
    }
}
