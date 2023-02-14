using MediatR;
using Microsoft.EntityFrameworkCore;
using FlameRestaurant.Application.AppCode.Infrastructure;
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
    public class ReservationGetAllQuery : PaginateModel, IRequest<PagedViewModel<Reservation>>
    {
        public class ReservationGetAllQueryHandler : IRequestHandler<ReservationGetAllQuery, PagedViewModel<Reservation>>
        {
            private readonly FlameRestaurantDbContext db;

            public ReservationGetAllQueryHandler(FlameRestaurantDbContext db)
            {
                this.db = db;
            }
            public async Task<PagedViewModel<Reservation>> Handle(ReservationGetAllQuery request, CancellationToken cancellationToken)
            {
                if (request.PageSize < 6)
                {
                    request.PageSize = 6;
                }
                var query = db.Reservations
                    .Where(r => r.DeletedDate == null)
                    .OrderByDescending(r=>r.CreatedDate)
                    .AsQueryable();
                if (query == null)
                {
                    return null;
                }
                var pagedModel = new PagedViewModel<Reservation>(query, request);
                return pagedModel;
            }
        }
    }
}
