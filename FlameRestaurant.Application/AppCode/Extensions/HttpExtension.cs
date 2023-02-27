using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlameRestaurant.Application.AppCode.Extensions
{
    public static partial class Extension
    {
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            return request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

        public static bool IsValid(this IActionContextAccessor ctx)
        {
            return ctx.ActionContext.ModelState.IsValid;
        }

        public static Dictionary<string, object> AddFromHeader(this Dictionary<string, object> items,
            HttpRequest request, string keyName)
        {
            if (request.Headers.TryGetValue(keyName, out StringValues formats))
            {
                items.Add(keyName, formats.FirstOrDefault());
            }

            return items;
        }
    }
}
