namespace MiniSurveys.Web.Models.Survey
{
    public class QuestionResultData
    {
        public string Title { get; set; }
        public ICollection<ChartItem> Answers { get; set; }
    }
}
