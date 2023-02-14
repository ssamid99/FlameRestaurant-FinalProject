using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlameRestaurant.Domain.Models.Entities.Membership;

namespace FlameRestaurant.Domain.Models.DataContexts.Configurations.Membership
{
    public class FlameRestaurantUserRoleEntityTypeConfiguration : IEntityTypeConfiguration<FlameRestaurantUserRole>
    {
        public void Configure(EntityTypeBuilder<FlameRestaurantUserRole> builder)
        {
            builder.ToTable("UserRoles", "Membership");
        }
    }
}
