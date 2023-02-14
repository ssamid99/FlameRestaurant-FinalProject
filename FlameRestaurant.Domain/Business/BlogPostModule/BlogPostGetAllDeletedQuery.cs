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

namespace FlameRestaurant.Domain.Business.BlogPostModule
{
    public class BlogPostGetAllDeletedQuery : PaginateModel, IRequest<PagedViewModel<BlogPost>>
    {
        public class BlogPostGetAllDeletedHandler : IRequestHandler<BlogPostGetAllDeletedQuery, PagedViewModel<BlogPost>>
        {
            private readonly FlameRestaurantDbContext db;

            public BlogPostGetAllDeletedHandler(FlameRestaurantDbContext db)
            {
                this.db = db;
            }


            public async Task<PagedViewModel<BlogPost>> Handle(BlogPostGetAllDeletedQuery request, CancellationToken cancellationToken)
            {
                if (request.PageSize < 6)
                {
                    request.PageSize = 6;
                }
                var query = db.BlogPosts
                    .Where(bp => bp.DeletedDate != null)
                    .AsQueryable();
                if (query == null)
                {
                    return null;
                }
                var pagedModel = new PagedViewModel<BlogPost>(query, request);
                return pagedModel;
            }
        }
    }
}
