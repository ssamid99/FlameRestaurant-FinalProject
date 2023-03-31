using FlameRestaurant.Domain.Business.ProductModule;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FlameRestaurant.WebUI.AppCode.ViewComponents.RelatedProducts
{
    public class RelatedProductsViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public RelatedProductsViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(string categoryName)
        {
            var query = new RelatedProductsQuery() { Category = categoryName };
            var products = await mediator.Send(query);

            return View(products);
        }
    }
}
