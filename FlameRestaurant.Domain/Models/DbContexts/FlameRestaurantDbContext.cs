﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FlameRestaurant.Domain.Models.Entities.Membership;
using FlameRestaurant.Domain.Models.Entities;

namespace FlameRestaurant.Domain.Models.DbContexts
{
    public partial class FlameRestaurantDbContext : IdentityDbContext<FlameRestaurantUser, FlameRestaurantRole, int, FlameRestaurantUserClaim, FlameRestaurantUserRole, FlameRestaurantUserLogin, FlameRestaurantRoleClaim, FlameRestaurantUserToken>
    {
        public FlameRestaurantDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogPostComment> BlogPostComments { get; set; }
        public DbSet<ContactPost> ContactPosts { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(FlameRestaurantDbContext).Assembly);
        }
    }
}