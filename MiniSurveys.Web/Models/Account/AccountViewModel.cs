using MiniSurveys.Domain.Modals;

namespace MiniSurveys.Web.Models.Account
{
    public class AccountViewModel
    {
        private string defaultAvatar = "avatar_default.png";

        public string Fio { get; set; }

        public string DepartmentName { get; set; }

        public string NumberPhone { get; set; }

        public string Email { get; set; }

        public string HrefAvatar { get; set; }

        public AccountViewModel(User user)
        {
            Fio = user.Surname + " " + user.Name + " " + user.Patronymic;
            DepartmentName = user.Department.Name ?? "Нет данных";
            NumberPhone = user.PhoneNumber ?? "Нет данных";
            Email = user.Email;
            HrefAvatar = user.HrefAvatar ?? defaultAvatar;
        }
    }
}
