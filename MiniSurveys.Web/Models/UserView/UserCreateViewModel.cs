using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniSurveys.Domain.Data;
using MiniSurveys.Domain.Modals;
using System.ComponentModel;

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

        [DisplayName("Логин")]
        public string UserName { get; set; }

        [DisplayName("Имя")]
        public string Name { get; set; }

        [DisplayName("Фамилия")]
        public string Surname { get; set; }

        [DisplayName("Отчетсво")]
        public string Patronymic { get; set; }

        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("Телефон")]
        public string Phone { get; set; }

        [DisplayName("Подразделение")]
        public Department Department { get; set; }

        [DisplayName("Роль")]
        public IdentityRole<int> Role { get; set; }

        [DisplayName("Роли")]
        public List<SelectListItem> RolesSelectList { get; set; }

        [DisplayName("Подразделения")]
        public List<SelectListItem> DepartmentsSelectList { get; set; }

        [DisplayName("Пароль")]
        public string Password { get; set; }

        [DisplayName("Аватарака")]
        public IFormFile Avatar { get; set; }
    }
}
