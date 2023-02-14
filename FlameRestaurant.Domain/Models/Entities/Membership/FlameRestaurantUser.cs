using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FlameRestaurant.Domain.Models.Entities.Membership
{
    public class FlameRestaurantUser : IdentityUser<int>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
    }
}
