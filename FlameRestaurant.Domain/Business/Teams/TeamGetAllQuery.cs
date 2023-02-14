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

namespace FlameRestaurant.Domain.Business.AboutModule.Teams
{
    public class TeamGetAllQuery :PaginateModel, IRequest<PagedViewModel<Team>>
    {
        public class TeamGetAllQueryHandLer : IRequestHandler<TeamGetAllQuery, PagedViewModel<Team>>
        {
            private readonly FlameRestaurantDbContext db;

            public TeamGetAllQueryHandLer(FlameRestaurantDbContext db)
            {
                this.db = db;
            }
            public async Task<PagedViewModel<Team>> Handle(TeamGetAllQuery request, CancellationToken cancellationToken)
            {
                if (request.PageSize < 6)
                {
                    request.PageSize = 6;
                }
                var query = db.Teams
                    .Where(r => r.DeletedDate == null)
                    .AsQueryable();
                if (query == null)
                {
                    return null;
                }
                var pagedModel = new PagedViewModel<Team>(query, request);
                return pagedModel;
            }
        }
    }
}
