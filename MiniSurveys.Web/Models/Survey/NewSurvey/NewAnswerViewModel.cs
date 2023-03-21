using System.ComponentModel.DataAnnotations;

namespace MiniSurveys.Web.Models.Survey.NewSurvey
{
    public class NewAnswerViewModel
    {
        public NewAnswerViewModel()
        {
            Media = new NewMediaViewModel();
        }

        [Required(ErrorMessage = "Не указан текст ответа")]
        public string Title { get; set; }
        public NewMediaViewModel Media { get; set; }
        public bool IsActive { get; set; }
    }
}
