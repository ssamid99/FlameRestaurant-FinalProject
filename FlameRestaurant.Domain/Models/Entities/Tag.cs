using FlameRestaurant.Application.AppCode.Infrastructure;
using FlameRestaurant.Domain.AppCode.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Models.Entities
{
    public class Tag : BaseEntity, IPageable
    {
        public string Text { get; set; }
        public virtual ICollection<ProductTagItem> TagCloud { get; set; }
    }
}
