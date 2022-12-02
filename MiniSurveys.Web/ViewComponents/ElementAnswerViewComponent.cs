using Microsoft.AspNetCore.Mvc;
using MiniSurveys.Web.Models.Survey;

namespace MiniSurveys.Web.ViewComponents
{
    public class ElementAnswerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(AnswerViewModel answer)
        {
            return Task.FromResult((IViewComponentResult)View(answer));
        }
    }
}
