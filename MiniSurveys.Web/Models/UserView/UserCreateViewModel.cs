using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniSurveys.Domain.Data;
using MiniSurveys.Domain.Modals;
using System.ComponentModel;
using System.Data;

namespace MiniSurveys.Web.Models.UserView
{
    public class UserCreateViewModel
    {
        public UserCreateViewModel()
        {

        }

        public UserCreateViewModel(User user)
        {
            UserName = user.UserName;
            Name = user.Name;
            Surname = user.Surname;
            Patronymic = user.Patronymic;
            Email = user.Email;
            Phone = user.PhoneNumber;
            Department = user.Department;
        }

        public static async Task<UserCreateViewModel> Initialize(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, ApplicationDbContext context)
        {
            var model = new UserCreateViewModel();

            model.Role = await roleManager.FindByNameAsync(RoleNames.Employee);
            var roles = roleManager.Roles;
            model.Roles = new SelectList(roles, nameof(model.Role.Name), nameof(model.Role.Name), model.Role.Name);

            model.Department = context.Departments.FirstOrDefault(x => x.Name == "Отдел разработки");
            var departments = context.Departments;
            model.Departments = new SelectList(departments, nameof(model.Department.Id), nameof(model.Department.Name), model.Department.Id);

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

        [DisplayName("Роль")]
        public SelectList Roles { get; set; }

        [DisplayName("Подразделение")]
        public SelectList Departments { get; set; }

        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}
