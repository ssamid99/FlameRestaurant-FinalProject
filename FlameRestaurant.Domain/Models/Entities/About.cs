using FlameRestaurant.Domain.AppCode.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Models.Entities
{
    public class About : BaseEntity
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
