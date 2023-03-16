namespace MiniSurveys.Domain.Modals
{
    public class QuestionResult
    {
        public int Id { get; set; }
        public Question Question { get; set; } = null!;
        public int QuestionId { get; set; }
        public ICollection<AnswerResult> AnswerResults { get; set; }
        public int SurveyResultId { get; set; }
        public SurveyResult SurveyResult { get; set; }
    }
}
