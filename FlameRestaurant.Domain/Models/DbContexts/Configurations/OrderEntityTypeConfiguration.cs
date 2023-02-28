using FlameRestaurant.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Models.DataContexts.Configurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Id)
                .UseIdentityColumn();

            builder.Property(entity => entity.Address)
                .IsRequired();

            builder.Property(entity => entity.PhoneNumber)
                .IsRequired();
        }
    }
}
