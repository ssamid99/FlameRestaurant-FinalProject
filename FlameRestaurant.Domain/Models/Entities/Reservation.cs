using FlameRestaurant.Application.AppCode.Infrastructure;
using FlameRestaurant.Domain.AppCode.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Models.Entities
{
    public class Reservation : BaseEntity, IPageable
    {
        public string Name { get; set; }
        public int NumberofPeople { get; set; }
        public DateTime Date { get; set; }
    }
}
