using FlameRestaurant.Application.AppCode.Infrastructure;
using FlameRestaurant.Domain.Models.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Business.ReservationModule
{
    public class ReservationPutCommand : IRequest<JsonResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberofPeople { get; set; }
        public DateTime Date { get; set; }
        public class ReservationPutCommandHandler : IRequestHandler<ReservationPutCommand, JsonResponse>
        {
            private readonly FlameRestaurantDbContext db;

            public ReservationPutCommandHandler(FlameRestaurantDbContext db)
            {
                this.db = db;
            }
            public async Task<JsonResponse> Handle(ReservationPutCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Reservations.FirstOrDefaultAsync(re => re.Id == request.Id && re.DeletedDate == null, cancellationToken);
                if (data == null)
                {
                    return null;
                }

                data.Name = request.Name;
                data.NumberofPeople = request.NumberofPeople;
                data.Date = request.Date;
                
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
