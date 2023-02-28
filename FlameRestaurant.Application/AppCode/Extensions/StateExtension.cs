using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FlameRestaurant.Application.AppCode.Extenstions
{
    public static partial class Extension
    {
        static public string GetValueFromCookie(this IActionContextAccessor ctx, string cookieName)
        {
            return ctx.ActionContext.HttpContext.Request.Cookies[cookieName];
        }
        static public int[] GetIntArrayFromCookie(this IActionContextAccessor ctx, string cookieName)
        {
            var value = ctx.GetValueFromCookie(cookieName);

            var arrayData = value?.Split(",", StringSplitOptions.RemoveEmptyEntries)
                                   .Where(x => Regex.IsMatch(x, @"\d+"))
                                   .Select(x => int.Parse(x))
                                   .ToArray();


            return arrayData;
        }


        static public IActionContextAccessor SetValueToCookie(this IActionContextAccessor ctx, string cookieName, string cookieValue)
        {
            ctx.ActionContext.HttpContext.Response.Cookies.Append(cookieName, cookieValue);

            return ctx;
        }

        static public IActionContextAccessor SetValueToCookie(this IActionContextAccessor ctx, string cookieName, int[] cookieValue)
        {
            ctx.ActionContext.HttpContext.Response.Cookies.Append(cookieName, string.Join(",", cookieValue));

            return ctx;
        }
    }
}
