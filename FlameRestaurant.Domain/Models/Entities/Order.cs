using FlameRestaurant.Domain.AppCode.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Models.Entities
{
    public class Order : BaseEntity
    {
        [Required(ErrorMessage = "{0} cannot be left empty")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "{0} cannot be left empty")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "{0} should not be empty!")]
        public string Address { get; set; }
        [Required(ErrorMessage = "{0} should not be empty!")]
        public string PhoneNumber { get; set; }
        public string Notes { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsDelivered { get; set; } = false;
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
