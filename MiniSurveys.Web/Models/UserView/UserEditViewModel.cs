using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniSurveys.Domain.Data;
using MiniSurveys.Domain.Modals;
using MiniSurveys.Web.Controllers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MiniSurveys.Web.Models.UserView
{
    public class UserEditViewModel
    {
        public UserEditViewModel()
        {

        }

        public UserEditViewModel(User user)
        {
            Id = user.Id;
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

        public int Id { get; set; }

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

        [DisplayName("Изображение")]
        [ValidateNever]
        public IFormFile? Avatar { get; set; }
    }
}
