using System.ComponentModel.DataAnnotations;

namespace MiniSurveys.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Поле логин обязательно для заполнения.")]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Поле пароль обязательно для заполнения.")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
    }
}
