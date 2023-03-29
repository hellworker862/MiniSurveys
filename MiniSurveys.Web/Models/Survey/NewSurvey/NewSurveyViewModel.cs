using System.ComponentModel.DataAnnotations;

namespace MiniSurveys.Web.Models.Survey.NewSurvey
{
    public class NewSurveyViewModel
    {
        public NewSurveyViewModel()
        {
            Questions = new List<NewQuestionViewModel>();
            Start = DateTime.Now.AddHours(1);
        }

        [Required(ErrorMessage = "Не указано название опроса")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Колличество вопросов должно быть больше 0")]
        public IList<NewQuestionViewModel> Questions { get; set; }
        [Required(ErrorMessage = "Не указана дата начала")]
        public  DateTime Start { get; set; }
        [Required(ErrorMessage = "Не указана дата начала")]
        public DateTime End { get; set; }
    }
}
