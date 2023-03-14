using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MiniSurveys.Web.Models
{
    public class ErrorViewModel : PageModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string ExceptionMessage { get; set; }
    }
}