using FlameRestaurant.Application.AppCode.Infrastructure;
using FlameRestaurant.Domain.Models.DbContexts;
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
    public class TagPutCommand : IRequest<JsonResponse>
    {
        public int Id { get; set; }
        public string Text { get; set; }
       
        public class TagPutCommandHandler : IRequestHandler<TagPutCommand, JsonResponse>
        {
            private readonly FlameRestaurantDbContext db;

            public TagPutCommandHandler(FlameRestaurantDbContext db)
            {
                this.db = db;
            }
            public async Task<JsonResponse> Handle(TagPutCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Tags.FirstOrDefaultAsync(re => re.Id == request.Id && re.DeletedDate == null, cancellationToken);
                if (data == null)
                {
                    return null;
                }

                data.Text = request.Text;

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
