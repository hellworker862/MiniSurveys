using MiniSurveys.Domain.Modals;

namespace MiniSurveys.Web.Models.Survey
{
    public class ResultData
    {
        public ResultData()
        {

        }

        public ResultData(int allUsers, int testedUsers)
        {
            //var data = surveyResult
            AllUsers = new ChartItem(allUsers - testedUsers, "Не прошедшие");
            TestedUsers = new ChartItem(testedUsers, "Прошедшие");
            //Questions = data.Select(x => new QuestionResultData(x.Title, x.Answers)).ToList();
        }

        public ChartItem AllUsers { get; set; }
        public ChartItem TestedUsers { get; set; }    

        //public List<QuestionResultData> Questions { get; set; }
    }

    public class ChartItem
    {
        public ChartItem()
        {

        }
        public ChartItem(int value, string title)
        {
            Value = value;
            Title = title;
        }

        public int Value { get; set; }
        public string Title { get; set; }
    }

    //public class QuestionResultData
    //{
    //    public QuestionResultData()
    //    {

    //    }

    //    public QuestionResultData(string title, ICollection<AnswerResult> answerResults)
    //    {
    //        Title = title;
    //        var data = answerResults.GroupBy(x => x.AnswerId);
    //        Answers = data.Select(x => new AnswerResultData(x.Key.ToString(), x.Count())).ToList();
    //    }

    //    public string Title { get; set; }
    //    public List<AnswerResultData> Answers { get; set; }
    //}

    //public class AnswerResultData
    //{
    //    public AnswerResultData()
    //    {

    //    }

    //    public AnswerResultData(string title, int count)
    //    {
    //        Title = title;
    //        Count = count;

    //    }

    //    public string Title { get; set; }
    //    public int Count { get; set; }

    //    public override string ToString()
    //    {
    //        return Title;
    //    }
    //}
}
