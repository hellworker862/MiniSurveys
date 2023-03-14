using Microsoft.AspNetCore.Identity;
using MiniSurveys.Domain.Modals;

namespace MiniSurveys.Web.Models.UserView
{
    public class UserViewModel
    {
        public UserViewModel(User user)
        {
            UserName = user.UserName;
            Name = user.Name;
            Surname = user.Surname;
            Patronymic = user.Patronymic;
            Email = user.Email;
            Phone = user.PhoneNumber;
            Department = user.Department;
        }

        public static async Task<UserViewModel> Initialize(User user, UserManager<User> userManager)
        {
            var model = new UserViewModel(user);
            string role = (await userManager.GetRolesAsync(user)).ElementAt(0);
            model.Role = role;

            return model;
        }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public Department Department { get; set; }

        public string Role { get; set; }
    }
}
