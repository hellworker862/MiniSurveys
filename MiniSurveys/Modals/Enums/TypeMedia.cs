using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MiniSurveys.Domain.Modals.Enums
{
    public enum TypeMedia : byte
    {
        [Display(Name = "Картинка")]
        Image = 1,
        [Display(Name = "Видео")]
        Video = 2,
    }
}
