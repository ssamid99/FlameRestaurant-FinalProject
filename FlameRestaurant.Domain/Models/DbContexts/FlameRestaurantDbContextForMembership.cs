using Microsoft.EntityFrameworkCore;
using FlameRestaurant.Domain.Models.Entities.Membership;

namespace FlameRestaurant.Domain.Models.DbContexts
{
    public partial class FlameRestaurantDbContext
    {
        public DbSet<FlameRestaurantRole> FlameRestaurantRoles { get; set; }
        public DbSet<FlameRestaurantRoleClaim> FlameRestaurantRoleClaims { get; set; }
        public DbSet<FlameRestaurantUser> FlameRestaurantUsers { get; set; }
        public DbSet<FlameRestaurantUserClaim> FlameRestaurantUserClaims { get; set; }
        public DbSet<FlameRestaurantUserLogin> FlameRestaurantUserLogins { get; set; }
        public DbSet<FlameRestaurantUserRole> FlameRestaurantUserRoles { get; set; }
        public DbSet<FlameRestaurantUserToken> FlameRestaurantUserTokens { get; set; }
    }
}
