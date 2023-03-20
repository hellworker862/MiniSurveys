using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniSurveys.Domain.Data;
using MiniSurveys.Domain.Modals;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MiniSurveys.Web.Models.UserView
{
    public class UserCreateViewModel
    {
        public UserCreateViewModel()
        {

        }

        public static async Task<UserCreateViewModel> Initialize(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, ApplicationDbContext context)
        {
            var model = new UserCreateViewModel();
            model.Role = await roleManager.FindByNameAsync(RoleNames.Employee);
            var roles = roleManager.Roles;
            model.RolesSelectList = new List<SelectListItem>();

            foreach (var item in roles)
            {
                model.RolesSelectList.Add(new SelectListItem(item.Name, item.Id.ToString()));
            }

            model.Department = context.Departments.FirstOrDefault(x => x.Name == "Отдел разработки");
            var departments = context.Departments;
            model.DepartmentsSelectList = new List<SelectListItem>();

            foreach (var item in departments)
            {
                model.DepartmentsSelectList.Add(new SelectListItem(item.Name, item.Id.ToString()));
            }

            return model;
        }

        public void InitializeSelectList(RoleManager<IdentityRole<int>> roleManager, ApplicationDbContext context)
        {
            var roles = roleManager.Roles;
            RolesSelectList = new List<SelectListItem>();
            foreach (var item in roles)
            {
                RolesSelectList.Add(new SelectListItem(item.Name, item.Id.ToString()));
            }
            var departments = context.Departments;
            DepartmentsSelectList = new List<SelectListItem>();
            foreach (var item in departments)
            {
                DepartmentsSelectList.Add(new SelectListItem(item.Name, item.Id.ToString()));
            }
        }

        [DisplayName("Логин")]
        [Required(ErrorMessage = "Не указан логин")]
        public string UserName { get; set; }

        [DisplayName("Имя")]
        [Required(ErrorMessage = "Не указано имя")]
        public string Name { get; set; }

        [DisplayName("Фамилия")]
        [Required(ErrorMessage = "Не указана фамилия")]
        public string Surname { get; set; }

        [DisplayName("Отчетсво")]
        [Required(ErrorMessage = "Не указано отчество")]
        public string Patronymic { get; set; }

        [DisplayName("E-mail")]
        [EmailAddress]
        [Required(ErrorMessage = "Не указана эл.почта")]
        public string Email { get; set; }

        [DisplayName("Телефон")]
        [Required(ErrorMessage = "Не указан телефон")]
        public string Phone { get; set; }

        [DisplayName("Подразделение")]
        [ValidateNever]
        public Department Department { get; set; }

        [DisplayName("Роль")]
        [ValidateNever]
        public IdentityRole<int> Role { get; set; }

        [DisplayName("Роли")]
        [ValidateNever]
        public List<SelectListItem> RolesSelectList { get; set; }

        [DisplayName("Подразделения")]
        [ValidateNever]
        public List<SelectListItem> DepartmentsSelectList { get; set; }

        [DisplayName("Пароль")]
        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }

        [DisplayName("Изображение")]
        [Required(ErrorMessage = "Не выбрано изображение")]
        public IFormFile Avatar { get; set; }
    }
}
