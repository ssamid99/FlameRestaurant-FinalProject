using FlameRestaurant.Application.AppCode.Infrastructure;
using FlameRestaurant.Domain.AppCode.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlameRestaurant.Domain.Models.Entities
{
    public class BlogPost : BaseEntity, IPageable
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string ImagePath { get; set; }
        public string Slug { get; set; }
        public DateTime? PublishedDate { get; set; }
        public virtual ICollection<BlogPostComment> Comments { get; set; }
    }
}
