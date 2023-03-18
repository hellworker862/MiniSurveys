using Microsoft.AspNetCore.Mvc.Rendering;
using MiniSurveys.Domain.Modals;

namespace MiniSurveys.Web.Models
{
    public class ResultViewModel
    {
        public ResultViewModel()
        {

        }

        public ResultViewModel(ICollection<Department> departments, string surveyName, int id)
        {
            DepartmentsSelectList = new List<SelectListItem>();

            foreach (Department item in departments)
                DepartmentsSelectList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });

            SelectedDepartment = DepartmentsSelectList.ElementAt(0);
            SurveyName = surveyName;
            Id = id;
        }

        public int Id { get; set; }
        public string SurveyName { get; set; }
        public SelectListItem SelectedDepartment { get; set; }
        public List<SelectListItem> DepartmentsSelectList { get; set; }
    }
}
