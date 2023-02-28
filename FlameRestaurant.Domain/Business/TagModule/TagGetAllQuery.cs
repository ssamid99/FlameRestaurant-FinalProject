using FlameRestaurant.Application.AppCode.Infrastructure;
using FlameRestaurant.Domain.Models.DbContexts;
using FlameRestaurant.Domain.Models.Entities;
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
    public class TagGetAllQuery : PaginateModel, IRequest<PagedViewModel<Tag>>
    {
        public class TagGetAllQueryHandler : IRequestHandler<TagGetAllQuery, PagedViewModel<Tag>>
        {
            private readonly FlameRestaurantDbContext db;

            public TagGetAllQueryHandler(FlameRestaurantDbContext db)
            {
                this.db = db;
            }
            public async Task<PagedViewModel<Tag>> Handle(TagGetAllQuery request, CancellationToken cancellationToken)
            {
                if (request.PageSize < 6)
                {
                    request.PageSize = 6;
                }
                var query = db.Tags
                    .Where(r => r.DeletedDate == null)
                    .OrderByDescending(r => r.CreatedDate)
                    .AsQueryable();
                if (query == null)
                {
                    return null;
                }
                var pagedModel = new PagedViewModel<Tag>(query, request);
                return pagedModel;
            }
        }
    }
}
