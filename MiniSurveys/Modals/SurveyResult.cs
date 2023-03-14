using MiniSurveys.Domain.Modals.Base;

namespace MiniSurveys.Domain.Modals
{
    public class SurveyResult : BaseEntity
    {
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
        public int SurveyId { get; set; }
        public User User { get; set; } = null!;
        public Survey Survey { get; set; } = null!;
        public ICollection<QuestionResult> QuestionResults { get; set; }
    }
}
