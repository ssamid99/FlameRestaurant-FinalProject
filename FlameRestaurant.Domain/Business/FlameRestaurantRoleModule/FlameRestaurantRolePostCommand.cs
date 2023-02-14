using MediatR;
using Microsoft.AspNetCore.Identity;
using FlameRestaurant.Domain.Models.Entities.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Business.FlameRestaurantRoleModule
{
    public class FlameRestaurantRolePostCommand : IRequest<FlameRestaurantRole>
    {
        public string Name { get; set; }
        public class FlameRestaurantRolePostCommandHandler : IRequestHandler<FlameRestaurantRolePostCommand, FlameRestaurantRole>
        {
            private readonly RoleManager<FlameRestaurantRole> roleManager;

            public FlameRestaurantRolePostCommandHandler(RoleManager<FlameRestaurantRole> roleManager)
            {
                this.roleManager = roleManager;
            }
            public async Task<FlameRestaurantRole> Handle(FlameRestaurantRolePostCommand request, CancellationToken cancellationToken)
            {
                var data = new FlameRestaurantRole();
                data.Name = request.Name;
                await roleManager.CreateAsync(data);
                //await roleManager.SetRoleNameAsync
                return data;
            }
        }
    }
}
