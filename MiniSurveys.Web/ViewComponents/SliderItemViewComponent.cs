using Microsoft.AspNetCore.Mvc;
using MiniSurveys.Web.Models.Survey;

namespace MiniSurveys.Web.ViewComponents
{
    public class SliderItemViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(MediaViewModel media)
        {
            return await Task.FromResult((IViewComponentResult)View(media));
        }
    }
}
