namespace MiniSurveys.Web.Models.Survey
{
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
}
