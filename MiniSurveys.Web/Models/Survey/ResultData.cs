using MiniSurveys.Domain.Modals;

namespace MiniSurveys.Web.Models.Survey
{
    public class ResultData
    {
        public ResultData()
        {

        }

        public ResultData(SurveyResult surveyResult)
        {
            var data = surveyResult.QuestionResults.GroupBy(x => x.QuestionId)
                .Select(x => new 
                {
                    Answers = x.SelectMany(x => x.AnswerResults).ToList(),
                    Title = x.Single().Question.Title,
                }).ToList();

            Questions = data.Select(x => new QuestionResultData(x.Title, x.Answers)).ToList();
        }

        public List<QuestionResultData> Questions { get; set; }
    }

    public class QuestionResultData
    {
        public QuestionResultData()
        {

        }

        public QuestionResultData(string title, ICollection<AnswerResult> answerResults)
        {
            Title = title;
            var data = answerResults.GroupBy(x => x.AnswerId);
            Answers = data.Select(x => new AnswerResultData(x.Key.ToString(), x.Count())).ToList();
        }

        public string Title { get; set; }
        public List<AnswerResultData> Answers { get; set; }
    }

    public class AnswerResultData
    {
        public AnswerResultData()
        {

        }

        public AnswerResultData(string title, int count)
        {
            Title = title;
            Count = count;

        }

        public string Title { get; set; }
        public int Count { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
