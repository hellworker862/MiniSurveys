using MiniSurveys.Domain.Modals;
using MiniSurveys.Domain.Modals.Enums;

namespace MiniSurveys.Web.Models.Survey
{
    public class MediaViewModel
    {
        public MediaViewModel()
        {

        }

        public MediaViewModel(Media media)
        {
            Id = media.Id;
            Url = media.Url;
            Title = media.Title;
            Type = media.Type;
        }

        public MediaViewModel(Media media, int number, int maxNumber)
        {
            Id = media.Id;
            Url = media.Url;
            Title = media.Title;
            Type = media.Type;
            Number = number;
            MaxNumber = maxNumber;
        }

        public int Id { get; init; }
        public string Url { get; init; } = null!;

        public string? Title { get; init; }

        public TypeMediaEnum Type { get; set; }

        public int? Number { get; init; }

        public int? MaxNumber { get; init; }
    }
}
