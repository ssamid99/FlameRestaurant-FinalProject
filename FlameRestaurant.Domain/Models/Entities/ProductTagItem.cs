using FlameRestaurant.Domain.AppCode.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Models.Entities
{
    public class ProductTagItem : BaseEntity
    {
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
