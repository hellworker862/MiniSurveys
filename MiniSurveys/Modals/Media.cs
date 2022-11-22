using MiniSurveys.Domain.Modals.Enums;

namespace MiniSurveys.Domain.Modals
{
    public class Media
    {
        public string Url { get; set; }

        public string? Title { get; set; }

        public TypeMediaEnum Type { get; set; }
    }
}
