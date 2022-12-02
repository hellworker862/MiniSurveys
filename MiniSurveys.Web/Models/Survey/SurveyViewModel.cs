using MiniSurveys.Domain.Modals;

namespace MiniSurveys.Web.Models.Survey
{
    public class SurveyViewModel
    {
        public SurveyViewModel(Domain.Modals.Survey survey)
        {
            Id = survey.Id;
            Title = survey.Title;
            IsQuestionOrder = survey.IsQuestionOrder;
            Questions = survey.Questions.Select(x => new QuestionViewModel(x));
        }

        public SurveyViewModel()
        {

        }

        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsQuestionOrder { get; set; }

        public IEnumerable<QuestionViewModel> Questions { get; set; }

        public int CurrentQuestion { get; set; } = 1;

        public void Next()
        {
            if (CurrentQuestion < Questions.Count()) CurrentQuestion++;
        }

        public void Back()
        {
            if (CurrentQuestion > 1) CurrentQuestion--;
        }

        public bool isNext() => CurrentQuestion < Questions.Count();

        public bool isBack() => CurrentQuestion > 1;

        public QuestionViewModel GetQuestion() => Questions.ElementAt(CurrentQuestion - 1);
    }
}
