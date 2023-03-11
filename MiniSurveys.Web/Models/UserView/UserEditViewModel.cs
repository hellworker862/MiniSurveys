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
            model.RolesSelectList = new List<SelectListItem>();
            foreach (var item in roles)
            {
                model.RolesSelectList.Add(new SelectListItem(item.Name, item.Id.ToString()));
            }
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

        [DisplayName("Изображение")]
        public IFormFile Avatar { get; set; }
    }
}
