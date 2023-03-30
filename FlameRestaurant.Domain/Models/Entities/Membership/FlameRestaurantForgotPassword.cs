using FlameRestaurant.Domain.AppCode.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Models.Entities.Membership
{
    public class FlameRestaurantForgotPassword : BaseEntity
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
