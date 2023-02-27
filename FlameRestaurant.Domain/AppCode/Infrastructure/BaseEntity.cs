using FlameRestaurant.Application.AppCode.Extensions;
using FlameRestaurant.Domain.Models.Entities.Membership;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;

namespace FlameRestaurant.Domain.AppCode.Infrastructure
{
    public class BaseEntity : AutitableEntity
    {
        public int Id { get; set; }
    }

    public class AutitableEntity
    {
        public int? CreatedByUserId { get; set; }
        public FlameRestaurantUser CreatedByUser { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public int? DeletedByUserId { get; set; }
        public FlameRestaurantUser DeletedByUser { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
