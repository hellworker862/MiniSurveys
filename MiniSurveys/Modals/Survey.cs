using MiniSurveys.Domain.Modals.Base;
using MiniSurveys.Domain.Modals.Enums;

namespace MiniSurveys.Domain.Modals
{
    public class Survey : BaseEntity
    {
        public string Title { get; set; }

        public SurveyStateTypeEnum SurveyState { get; set; } = SurveyStateTypeEnum.NoActive;

        public DateTime StartTime { get; init; }

        public DateTime EndTime { get; set; }

        public bool IsQuestionOrder { get; set; } = false;

        public IEnumerable<Question> Questions { get; set; }
    }
}
