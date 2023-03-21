using System.ComponentModel.DataAnnotations;

namespace MiniSurveys.Web.Models.Survey.NewSurvey
{
    public class NewSurveyViewModel
    {
        public NewSurveyViewModel()
        {
            Questions = new List<NewQuestionViewModel>();
        }

        [Required(ErrorMessage = "Не указано название опроса")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Колличество вопросов должно быть больше 0")]
        public ICollection<NewQuestionViewModel> Questions { get; set; }
    }
}
