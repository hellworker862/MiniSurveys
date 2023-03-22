using System.ComponentModel.DataAnnotations;

namespace MiniSurveys.Web.Models.Survey.NewSurvey
{
    public class NewQuestionViewModel
    {
        public NewQuestionViewModel()
        {
            Medias = new List<NewMediaViewModel>();
            Answers = new List<NewAnswerViewModel>();
        }

        [Required(ErrorMessage = "Не указан текст вопроса")]
        public string QuestionTitle { get; set; }
        public ICollection<NewMediaViewModel> Medias { get; set; }
        public ICollection<NewAnswerViewModel> Answers { get; set; }
    }
}
