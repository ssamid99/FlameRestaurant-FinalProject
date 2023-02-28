using FlameRestaurant.Application.AppCode.Infrastructure;
using FlameRestaurant.Domain.AppCode.Infrastructure;
using System.Collections.Generic;

namespace FlameRestaurant.Domain.Models.Entities
{
    public class Product : BaseEntity, IPageable
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public string StockKeepingUnit { get; set; }
        public double Rate { get; set; }
        public string Description { get; set; }
        public string ReceipeDescription { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<ProductTagItem> TagCloud { get; set; }

        //public virtual ICollection<Order> Orders { get; set; }

        //public virtual ICollection<OrderBook> OrderBooks { get; set; }
    }
}
