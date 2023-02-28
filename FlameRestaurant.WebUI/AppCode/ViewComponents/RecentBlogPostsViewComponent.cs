using MediatR;
using Microsoft.AspNetCore.Mvc;
using FlameRestaurant.Domain.Business.BlogPostModule;
using System.Threading.Tasks;

namespace FlameRestaurant.WebUI.AppCode.ViewComponents
{
    public class RecentBlogPostsViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public RecentBlogPostsViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        //public async Task<IViewComponentResult> InvokeAsync()
        //{
        //    var query = new RecentBlogPostQuery() { Size = 5 };
        //    var posts = await mediator.Send(query);

        //    return View(posts);
        //}
    }
}
