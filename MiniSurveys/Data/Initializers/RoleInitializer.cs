using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MiniSurveys.Domain.Modals;

namespace MiniSurveys.Domain.Data.Initializers
{
    public static class RoleInitializer
    {
        private const string defaultPassword = "qwerty123";

        public static async Task InitializeAsync(IServiceProvider provider)
        {
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole<int>>>();
            foreach (var roleName in RoleNames.AllRoles)
            {
                var role = roleManager.FindByNameAsync(roleName).Result;

                if (role == null)
                {
                    var result = roleManager.CreateAsync(new IdentityRole<int> { Name = roleName }).Result;
                    if (!result.Succeeded) throw new Exception(result.Errors.First().Description);
                }
            }

            var userManager = provider.GetRequiredService<UserManager<User>>();
            var context = provider.GetRequiredService<ApplicationDbContext>();

            foreach (var employee in DefaultUsers.Employees)
            {
                var department = context.Departments.FirstOrDefault(x => x.Name == "Отдел разработки");
                employee.Department = department;
                var result = userManager.CreateAsync(employee, defaultPassword).Result;

                if (result.Succeeded)
                {
                    var employeeUser = await userManager.FindByNameAsync(employee.UserName);
                    await userManager.AddToRoleAsync(employeeUser, RoleNames.Employee);
                }
            }

            foreach (var employee in DefaultUsers.Admins)
            {
                var department = context.Departments.FirstOrDefault(x => x.Name == "Кадровая служба");
                employee.Department = department;
                var result = userManager.CreateAsync(employee, defaultPassword).Result;

                if (result.Succeeded)
                {
                    var employeeUser = await userManager.FindByNameAsync(employee.UserName);
                    await userManager.AddToRoleAsync(employeeUser, RoleNames.Administrator);
                }
            }

            foreach (var employee in DefaultUsers.Heads)
            {
                var department = context.Departments.FirstOrDefault(x => x.Name == "Отдел разработки");
                employee.Department = department;
                var result = userManager.CreateAsync(employee, defaultPassword).Result;

                if (result.Succeeded)
                {
                    var employeeUser = await userManager.FindByNameAsync(employee.UserName);
                    await userManager.AddToRoleAsync(employeeUser, RoleNames.Head);
                }
            }
        }
    }

    public static class RoleNames
    {
        public const string Administrator = "HR";
        public const string Employee = "Сотрудник";
        public const string Head = "Руководитель отдела";

        public static IEnumerable<string> AllRoles
        {
            get
            {
                yield return Administrator;
                yield return Employee;
                yield return Head;
            }
        }
    }

    public static class DefaultUsers
    {
        private static User employee1 = new User()
        {
            UserName = "user1",
            Surname = "Петров",
            Name = "Георгий",
            Patronymic = "Степанович",
            Email = "kdawson@yahoo.ca",
            EmailConfirmed = true,
            HrefAvatar = "avatar_user1.png"
        };

        private static User employee2 = new User()
        {
            UserName = "user2",
            Surname = "Федоров",
            Name = "Тимофей",
            Patronymic = "Яковлевич",
            Email = "mrobshaw@icloud.com",
            EmailConfirmed = true,
            HrefAvatar = "avatar_user2.png"
        };
        private static User employee3 = new User()
        {
            UserName = "user3",
            Surname = "Фролов",
            Name = "Александр",
            Patronymic = "Иванович",
            Email = "facet@yahoo.com",
            EmailConfirmed = true,
            HrefAvatar = "avatar_user3.png"
        };

        private static User employee4 = new User()
        {
            UserName = "user4",
            Surname = "Медведев",
            Name = "Денис",
            Patronymic = "Миронович",
            Email = "uncled@verizon.net",
            EmailConfirmed = true,
            HrefAvatar = "avatar_user4.png"
        };

        private static User head1 = new User()
        {
            UserName = "head1",
            Surname = "Сидорова",
            Name = "Александра",
            Patronymic = "Глебовна",
            Email = "raides@gmail.com",
            EmailConfirmed = true,
            HrefAvatar = "avatar_head1.png"
        };

        private static User admin1 = new User()
        {
            UserName = "admin1",
            Surname = "Иванов",
            Name = "Иван",
            Patronymic = "Иванович",
            Email = "myemail@mail.ru",
            EmailConfirmed = true,
            HrefAvatar = "avatar_admin1.png"
        };


        public static IEnumerable<User> Employees
        {
            get
            {
                yield return employee1;
                yield return employee2;
                yield return employee3;
                yield return employee4;
            }
        }

        public static IEnumerable<User> Heads
        {
            get
            {
                yield return head1;
            }
        }

        public static IEnumerable<User> Admins
        {
            get
            {
                yield return admin1;
            }
        }
    }
}
