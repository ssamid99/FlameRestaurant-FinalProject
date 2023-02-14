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

namespace FlameRestaurant.Domain.Business.ReservationModule
{
    public class ReservationGetSingleQuery : IRequest<Reservation>
    {
        public int Id { get; set; }
        public class ReservationGetSingleQueryHandler : IRequestHandler<ReservationGetSingleQuery, Reservation>
        {
            private readonly FlameRestaurantDbContext db;

            public ReservationGetSingleQueryHandler(FlameRestaurantDbContext db)
            {
                this.db = db;
            }
            public async Task<Reservation> Handle(ReservationGetSingleQuery request, CancellationToken cancellationToken)
            {
                var query = await db.Reservations.FirstOrDefaultAsync(re => re.Id == request.Id && re.DeletedDate == null, cancellationToken);
                
                return query;
            }
        }
    }
}
