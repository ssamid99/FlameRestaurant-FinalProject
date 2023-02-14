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
    public class FlameRestaurantRolePutCommand : IRequest<FlameRestaurantRole>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public class FlameRestaurantRolePutCommandHandler : IRequestHandler<FlameRestaurantRolePutCommand, FlameRestaurantRole>
        {
            private readonly RoleManager<FlameRestaurantRole> roleManager;

            public FlameRestaurantRolePutCommandHandler(RoleManager<FlameRestaurantRole> roleManager)
            {
                this.roleManager = roleManager;
            }
            public async Task<FlameRestaurantRole> Handle(FlameRestaurantRolePutCommand request, CancellationToken cancellationToken)
            {
                var data = await roleManager.Roles.FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
                data.Name = request.Name;
                if(data == null)
                {
                    return null;
                }
                await roleManager.UpdateAsync(data);
                return data;
            }
        }
    }
}
