using System.ComponentModel.DataAnnotations;

namespace MiniSurveys.Web.Models.Survey.NewSurvey
{
    public class NewSurveyViewModel
    {
        public NewSurveyViewModel()
        {
            Questions = new List<NewQuestionViewModel>();
            Start = DateTime.Now.AddHours(1);
            End = DateTime.Now.AddHours(1);
        }

        [Required(ErrorMessage = "Не указано название опроса")]
        public string Title { get; set; }
        public IList<NewQuestionViewModel> Questions { get; set; }
        [Required(ErrorMessage = "Не указана дата начала")]
        public  DateTime Start { get; set; }
        [Required(ErrorMessage = "Не указана дата начала")]
        public DateTime End { get; set; }
    }
}
