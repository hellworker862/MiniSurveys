using Microsoft.AspNetCore.Mvc;
using MiniSurveys.Web.Models.UserView;

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
