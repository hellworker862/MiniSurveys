using MiniSurveys.Domain.Modals;

namespace MiniSurveys.Web.Models.Survey
{
    public class AnswerViewModel
    {
        public AnswerViewModel(Answer answer, int number)
        {
            Id = answer.Id;
            Title = answer.Title;
            if(answer.Media != null)
                Media = new MediaViewModel(answer.Media);
            Number = number;
        }

        public AnswerViewModel()
        {

        }

        public int Id { get; init; }

        public string? Title { get; init; }

        public bool IsTitle => !String.IsNullOrWhiteSpace(Title);

        public MediaViewModel? Media { get; init; }

        public bool IsMedia => Media != null;

        public int Number { get; init; }

        public bool isVote { get; set; }
    }
}
