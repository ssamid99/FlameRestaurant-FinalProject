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
    public class FlameRestaurantRoleRemoveCommand : IRequest<FlameRestaurantRole>
    {
        public int Id { get; set; }
        public class FlameRestaurantRoleRemoveCommandHandler : IRequestHandler<FlameRestaurantRoleRemoveCommand, FlameRestaurantRole>
        {
            private readonly RoleManager<FlameRestaurantRole> roleManager;

            public FlameRestaurantRoleRemoveCommandHandler(RoleManager<FlameRestaurantRole> roleManager)
            {
                this.roleManager = roleManager;
            }
            public async Task<FlameRestaurantRole> Handle(FlameRestaurantRoleRemoveCommand request, CancellationToken cancellationToken)
            {
                var data = await roleManager.Roles.FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
                if(data == null)
                {
                    return null;
                }
                await roleManager.DeleteAsync(data);
                return data;
            }
        }
    }
}
