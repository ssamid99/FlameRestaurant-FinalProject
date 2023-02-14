using FlameRestaurant.Application.AppCode.Infrastructure;
using FlameRestaurant.Domain.AppCode.Infrastructure;
using System;

namespace FlameRestaurant.Domain.Models.Entities
{
    public class ContactPost : BaseEntity, IPageable
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Answer { get; set; }
        public DateTime? AnswerDate { get; set; }
        public int? AnsweredbyId { get; set; }
    }
}
