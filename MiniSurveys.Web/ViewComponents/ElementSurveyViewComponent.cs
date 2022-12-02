using Microsoft.AspNetCore.Mvc;
using MiniSurveys.Domain.Modals;

namespace MiniSurveys.Web.ViewComponents
{
    public class ElementSurveyViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(Survey survey)
        {
            return Task.FromResult((IViewComponentResult)View(survey));
        }
    }
}
