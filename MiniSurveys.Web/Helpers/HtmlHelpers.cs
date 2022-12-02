using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MiniSurveys.Domain.Modals.Enums;

namespace MiniSurveys.Web.Helpers
{
    public static class HtmlHelpers
    {
        public static string IsActiveLiHeader(this IHtmlHelper htmlHelper, string controller, string action)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var routeAction = routeData.Values["action"]!.ToString();
            var routeController = routeData.Values["controller"]!.ToString();

            var returnActive = (controller == routeController && (action == routeAction || routeAction == "Details"));

            return returnActive ? "header__li_active" : "";
        }

        public static string isActiveSurvey(this IHtmlHelper htmlHelper, SurveyStateTypeEnum type)
        {
            if (type != SurveyStateTypeEnum.Active) return "link-disable";
            else return String.Empty;
        }

        public static string isDisableButton(this IHtmlHelper htmlHelper, bool isDisable)
        {
            if (isDisable) return "disabled";
            else return String.Empty;
        }
    }
}
