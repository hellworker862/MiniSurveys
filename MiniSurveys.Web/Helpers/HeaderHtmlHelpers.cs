using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MiniSurveys.Web.Helpers
{
    public static class HeaderHtmlHelpers
    {
        public static string IsActive(this IHtmlHelper htmlHelper, string controller, string action)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var routeAction = routeData.Values["action"].ToString();
            var routeController = routeData.Values["controller"].ToString();

            var returnActive = (controller == routeController && (action == routeAction || routeAction == "Details"));

            return returnActive ? "header__li_active" : "";
        }
    }
}
