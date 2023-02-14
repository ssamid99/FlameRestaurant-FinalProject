using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FlameRestaurant.Domain.Models.Entities.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Business.FlameRestaurantRoleModule
{
    public class FlameRestaurantRoleGetAllQuery : IRequest<List<FlameRestaurantRole>>
    {
        public class FlameRestaurantRoleGetAllQueryHandler : IRequestHandler<FlameRestaurantRoleGetAllQuery, List<FlameRestaurantRole>>
        {
            private readonly RoleManager<FlameRestaurantRole> roleManager;

            public FlameRestaurantRoleGetAllQueryHandler(RoleManager<FlameRestaurantRole> roleManager)
            {
                this.roleManager = roleManager;
            }
            public async Task<List<FlameRestaurantRole>> Handle(FlameRestaurantRoleGetAllQuery request, CancellationToken cancellationToken)
            {
                var query = await roleManager.Roles.ToListAsync(cancellationToken);
                if(query == null)
                {
                    return null;
                }
                return query;
            }
        }
    }
}
