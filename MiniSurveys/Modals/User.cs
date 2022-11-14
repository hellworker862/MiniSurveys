using Microsoft.AspNetCore.Identity;

namespace MiniSurveys.Domain.Modals
{
    public class User : IdentityUser<int>
    {
        public string Surname { get; }

        public string Name { get; }

        public string Patronymic { get; }

        public string Email { get; }

        public Department Department { get; }
    }
}
