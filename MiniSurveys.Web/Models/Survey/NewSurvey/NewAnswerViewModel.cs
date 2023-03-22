using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniSurveys.Domain.Extensions;
using MiniSurveys.Domain.Modals.Enums;
using System.ComponentModel.DataAnnotations;

namespace MiniSurveys.Web.Models.Survey.NewSurvey
{
    public class NewAnswerViewModel
    {
        public NewAnswerViewModel()
        {
            TypeSelectedListMedia = new List<SelectListItem>();
            TypeSelectedListMedia.Add(new SelectListItem
            {
                Text = TypeMedia.Image.GetEnumDisplayName(),
                Value = ((int)TypeMedia.Image).ToString()
            });
            TypeSelectedListMedia.Add(new SelectListItem
            {
                Text = TypeMedia.Video.GetEnumDisplayName(),
                Value = ((int)TypeMedia.Video).ToString()
            });
            SelectedTypeMedia = TypeSelectedListMedia.ElementAt(0);
        }

        [Required(ErrorMessage = "Не указан текст ответа")]
        public string AnswerTitle { get; set; }
        [ValidateNever]
        public string? SignatureMedia { get; set; }
        [ValidateNever]
        public SelectListItem SelectedTypeMedia { get; set; }
        [ValidateNever]
        public ICollection<SelectListItem> TypeSelectedListMedia { get; set; }
        [ValidateNever]
        public string? LinkMedia { get; set; }
        public bool IsActive { get; set; }
    }
}
