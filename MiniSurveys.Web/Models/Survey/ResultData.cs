using MiniSurveys.Domain.Modals;

namespace MiniSurveys.Web.Models.Survey
{
    public class ResultData
    {
        public ResultData()
        {

        }

        public ResultData(string title, int allUsers, int testedUsers, ICollection<QuestionResultData> questionResultDatas)
        {
            Title = title;
            SurveyedUsers = new  List<ChartItem>(){ new ChartItem(allUsers - testedUsers, "Не прошедшие"), new ChartItem(testedUsers, "Прошедшие")};
            QuestionResultDatas = questionResultDatas;
        }

        public string Title { get; set; }
        public ICollection<ChartItem> SurveyedUsers { get; set; }
        public ICollection<QuestionResultData> QuestionResultDatas { get; set; } 
    }
}
