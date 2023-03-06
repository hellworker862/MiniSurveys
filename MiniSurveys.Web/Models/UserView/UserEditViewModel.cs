using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniSurveys.Domain.Data;
using MiniSurveys.Domain.Modals;
using System.ComponentModel;

namespace MiniSurveys.Web.Models.UserView
{
    public class UserEditViewModel
    {
        public UserEditViewModel()
        {

        }

        public UserEditViewModel(User user)
        {
            UserName = user.UserName;
            Name = user.Name;
            Surname = user.Surname;
            Patronymic = user.Patronymic;
            Email = user.Email;
            Phone = user.PhoneNumber;
            Department = user.Department;
        }

        public static async Task<UserEditViewModel> Initialize(User user, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, ApplicationDbContext context)
        {
            var model = new UserEditViewModel(user);
            var role = (await userManager.GetRolesAsync(user)).ElementAt(0);
            var roleClass = await roleManager.FindByNameAsync(role);
            model.Role = roleClass;
            var roles = roleManager.Roles;
            model.Roles = new SelectList(roles, nameof(model.Role.Name), nameof(model.Role.Name), model.Role.Name);
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

        public string DepartmentName { get; set; }
    }
}
