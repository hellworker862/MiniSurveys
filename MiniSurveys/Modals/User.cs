using Microsoft.AspNetCore.Identity;

namespace MiniSurveys.Domain.Modals
{
    public class User : IdentityUser<int>
    {
        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public Department Department { get; set; }

        public string HrefAvatar { get; set; }
    }
}
