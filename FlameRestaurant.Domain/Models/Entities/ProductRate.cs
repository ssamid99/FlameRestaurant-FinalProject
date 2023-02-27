using FlameRestaurant.Domain.AppCode.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Models.Entities
{
    public class ProductRate : AutitableEntity
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public byte Rate { get; set; }
    }
}
