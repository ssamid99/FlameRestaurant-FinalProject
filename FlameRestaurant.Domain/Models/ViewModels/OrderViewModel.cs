using FlameRestaurant.Domain.Models.Entities;
using System.Collections.Generic;

namespace FlameRestaurant.Domain.Models.ViewModels
{
    public class OrderViewModel
    {
        public IEnumerable<Basket> BasketProducts { get; set; }

        public Order OrderDetails { get; set; }
    }
}
