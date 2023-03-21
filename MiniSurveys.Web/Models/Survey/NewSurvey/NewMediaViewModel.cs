using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniSurveys.Domain.Extensions;
using MiniSurveys.Domain.Modals.Enums;
using System.ComponentModel.DataAnnotations;

namespace MiniSurveys.Web.Models.Survey.NewSurvey
{
    public class NewMediaViewModel
    {
        [Required(ErrorMessage = "Не указана подпись мультимедия")]
        public string Signature { get; set; }
        [ValidateNever]
        public SelectListItem SelectedType { get; set; }
        [ValidateNever]
        public ICollection<SelectListItem> TypeSelectedList { get; set; }
        [Required(ErrorMessage = "Укажите сыллку на мультимедия")]
        public string Link { get; set; }

        public NewMediaViewModel()
        {
            TypeSelectedList = new List<SelectListItem>();
            TypeSelectedList.Add(new SelectListItem 
            { 
                Text = TypeMedia.Image.GetEnumDisplayName(), 
                Value = ((int)TypeMedia.Image).ToString()
            });
            TypeSelectedList.Add(new SelectListItem 
            { 
                Text = TypeMedia.Video.GetEnumDisplayName(), 
                Value = ((int)TypeMedia.Video).ToString() 
            });
            SelectedType = TypeSelectedList.ElementAt(0);
        }
    }
}
