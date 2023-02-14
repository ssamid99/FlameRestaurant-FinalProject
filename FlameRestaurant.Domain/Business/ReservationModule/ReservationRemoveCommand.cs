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

namespace FlameRestaurant.Domain.Business.ReservationModule
{
    public class ReservationRemoveCommand : IRequest<Reservation>
    {
        public int Id { get; set; }
        public class ReservationRemoveCommandHandler : IRequestHandler<ReservationRemoveCommand, Reservation>
        {
            private readonly FlameRestaurantDbContext db;
            private readonly IActionContextAccessor ctx;

            public ReservationRemoveCommandHandler(FlameRestaurantDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }
            public async Task<Reservation> Handle(ReservationRemoveCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Reservations.FirstOrDefaultAsync(r => r.Id == request.Id && r.DeletedDate == null, cancellationToken);
                if(data == null)
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
