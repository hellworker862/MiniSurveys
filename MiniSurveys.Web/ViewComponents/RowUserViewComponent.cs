using Microsoft.AspNetCore.Mvc;
using MiniSurveys.Web.Models;
using MiniSurveys.Web.Models.Survey;

namespace MiniSurveys.Web.ViewComponents
{
    public class RowUserViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(UserViewModel users)
        {
            return Task.FromResult((IViewComponentResult)View(users));
        }
    }
}
