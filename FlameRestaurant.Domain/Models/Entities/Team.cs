using FlameRestaurant.Application.AppCode.Infrastructure;
using FlameRestaurant.Domain.AppCode.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Models.Entities
{
    public class Team : BaseEntity, IPageable
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Text { get; set; }
        public string ImagePath { get; set; }
    }
}
