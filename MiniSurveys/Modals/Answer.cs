using MiniSurveys.Domain.Modals.Base;

namespace MiniSurveys.Domain.Modals
{
    public class Answer : BaseEntity
    {
        public string? Title { get; set; }

        public Media? Media { get; set; }

        public int? MediaId { get; set; }

        public Question Question { get; set; }

        public int QuestionId { get; set; }
    }
}
