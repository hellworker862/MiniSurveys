using MiniSurveys.Domain.Modals.Base;

namespace MiniSurveys.Domain.Modals
{
    public class Survey : BaseEntity
    {
        public string Title { get; set; }

        public DateTime StartTime { get; init; }

        public DateTime EndTime { get; set; }

        public bool IsQuestionOrder { get; set; } = false;

        public ICollection<Question> Questions { get; set; }
    }
}
