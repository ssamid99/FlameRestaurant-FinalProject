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
    public class FlameRestaurantRoleGetSingleQuery : IRequest<FlameRestaurantRole>
    {
        public int Id { get; set; }
        public class FlameRestaurantRoleGetSingleQueryHandler : IRequestHandler<FlameRestaurantRoleGetSingleQuery, FlameRestaurantRole>
        {
            private readonly RoleManager<FlameRestaurantRole> roleManager;

            public FlameRestaurantRoleGetSingleQueryHandler(RoleManager<FlameRestaurantRole> roleManager)
            {
                this.roleManager = roleManager;
            }
            public async Task<FlameRestaurantRole> Handle(FlameRestaurantRoleGetSingleQuery request, CancellationToken cancellationToken)
            {
                var query = await roleManager.Roles.FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
                if(query == null)
                {
                    return null;
                }
                return query;
            }
        }
    }
}
