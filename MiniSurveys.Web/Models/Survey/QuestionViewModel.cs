using MiniSurveys.Domain.Modals;

namespace MiniSurveys.Web.Models.Survey
{
    public class QuestionViewModel
    {
        public QuestionViewModel(Question question)
        {
            int i = 0;
            int j = 0;

            Id = question.Id;
            Title = question.Title;
            Number = question.Number;
            if (question.Media != null)
                Media = question.Media.Select(x => new MediaViewModel(x, ++j, question.Media.Count)).ToArray();
            Answers = question.Answers.Select(x => new AnswerViewModel(x, ++i));
        }

        public QuestionViewModel()
        {

        }

        public int Id { get; init; }

        public string? Title { get; init; }

        public bool IsTitle => !String.IsNullOrWhiteSpace(Title);

        public int Number { get; init; }

        public ICollection<MediaViewModel>? Media { get; init; }

        public bool IsMedia => Media != null && Media.Count > 0;

        public IEnumerable<AnswerViewModel> Answers { get; init; } = null!;

        public bool IsSelectedAnswer => Answers.Any(x => x.isVote);

        public void OnVote(int answerNumber)
        {
            foreach (var item in Answers)
                item.isVote = false;
            Answers.ElementAt(answerNumber).isVote = true;
        }
    }
}
