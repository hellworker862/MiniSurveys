using MiniSurveys.Domain.Modals.Enums;

namespace MiniSurveys.Web.Models
{
    public class SurveyMenuViewModel
    {
        public SurveyMenuViewModel(SurveyStateTypeEnum? surveyState, string? stringSearch)
        {
            if(surveyState != null) SurveyState = surveyState;
            StringSearch = stringSearch;
        }

        public SurveyStateTypeEnum? SurveyState { get; set; } = SurveyStateTypeEnum.Active;

        public string? StringSearch { get; set; }

    }
}
