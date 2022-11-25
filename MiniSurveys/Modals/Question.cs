using MiniSurveys.Domain.Modals.Base;

namespace MiniSurveys.Domain.Modals
{
    public class Question : BaseEntity
    {
        public string? Title { get; set; }

        public ICollection<Media>? Media { get; set; }

        public ICollection<Answer> Answers { get; set; }

        public int Number { get; set; } = -1;

        public Survey Survey { get; set; }

        public int SurveyId { get; set; }
    }
}
