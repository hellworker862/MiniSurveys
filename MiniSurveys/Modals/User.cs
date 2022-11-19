using Microsoft.AspNetCore.Identity;

namespace MiniSurveys.Domain.Modals
{
    public class User : IdentityUser<int>
    {
        public string Surname { get; init; }

        public string Name { get; init; }

        public string Patronymic { get; init; }

        public Department Department { get; set; }
    }
}
