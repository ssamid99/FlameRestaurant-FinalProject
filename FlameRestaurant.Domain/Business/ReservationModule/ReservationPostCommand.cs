using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using FlameRestaurant.Application.AppCode.Infrastructure;
using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using FlameRestaurant.Application.AppCode.Extensions;
using FlameRestaurant.Domain.Models.FormData;
using Microsoft.AspNetCore.Identity;
using FlameRestaurant.Domain.Models.Entities.Membership;

namespace FlameRestaurant.Domain.Business.ReservationModule
{
    public class ReservationPostCommand : IRequest<JsonResponse>
    {
        public string Name { get; set; }
        public int NumberofPeople { get; set; }
        public DateTime Date { get; set; }
        public class ReservationPostCommandHandler : IRequestHandler<ReservationPostCommand, JsonResponse>
        {
            private readonly FlameRestaurantDbContext db;
            private readonly IActionContextAccessor ctx;

            public ReservationPostCommandHandler(FlameRestaurantDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }
            public async Task<JsonResponse> Handle(ReservationPostCommand request, CancellationToken cancellationToken)
            {
                var data = new Reservation();
                data.Name = request.Name;
                data.NumberofPeople = request.NumberofPeople;
                data.Date = request.Date;
                await db.Reservations.AddAsync(data, cancellationToken);
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
