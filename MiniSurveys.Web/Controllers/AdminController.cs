using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSurveys.Domain.Data;
using MiniSurveys.Domain.Modals;
using MiniSurveys.Domain.Modals.Enums;
using MiniSurveys.Web.Helpers;
using MiniSurveys.Web.Models.UserView;

namespace MiniSurveys.Web.Controllers
{
    [Authorize(Roles = "HR")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public AdminController(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<ActionResult> Index(bool isActive = true, string? stringSearch = "")
        {
            IEnumerable<Survey> searchResult;

            if (isActive)
                searchResult = await _context.Surveys.Where(x => x.SurveyState != SurveyStateTypeEnum.Finished && EF.Functions.Like(x.Title, $"%{stringSearch}%")).AsNoTracking().ToArrayAsync();
            else
                searchResult = await _context.Surveys.Where(x => x.SurveyState == SurveyStateTypeEnum.Finished && EF.Functions.Like(x.Title, $"%{stringSearch}%")).AsNoTracking().ToArrayAsync();

            return View(searchResult);
        }

        [HttpGet]
        public ActionResult CreateSurvey()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Users()
        {
            var result = await _context.Users.Where(x => x.LockoutEnabled == true).Include(x => x.Department).ToListAsync();
            List<UserViewModel> users = new List<UserViewModel>();
            foreach (var item in result)
            {
                users.Add(await UserViewModel.Initialize(item, _userManager));
            }

            return View(users);
        }

        [Route("[controller]/{action}")]
        [HttpGet]
        public async Task<ActionResult> GetUsersBySearchString(string searchString)
        {
            var result = await _context.Users.Where(x => EF.Functions.Like(x.Name, $"%{searchString}%")
            | EF.Functions.Like(x.Surname, $"%{searchString}%")
            | EF.Functions.Like(x.Patronymic, $"%{searchString}%")
            | EF.Functions.Like(x.UserName, $"%{searchString}%")
            | EF.Functions.Like(x.Email, $"%{searchString}%") && x.LockoutEnabled == true).Include(x => x.Department).ToListAsync();

            List<UserViewModel> users = new List<UserViewModel>();
            foreach (var item in result)
            {
                users.Add(await UserViewModel.Initialize(item, _userManager));
            }

            return PartialView("UserListPartial", users);
        }

        [HttpGet]
        [NoDirectAccess]
        public async Task<ActionResult> UserEditPartial(string userName)
        {
            var user = await _context.Users.Include(d => d.Department).Where(x => x.LockoutEnabled == true).AsNoTracking().FirstOrDefaultAsync(x => x.UserName == userName);
            var model = await UserEditViewModel.Initialize(user, _userManager, _roleManager, _context);

            return PartialView("UserEditPartial", model);
        }

        [HttpPost]
        [Route("[controller]/{action}")]
        public async Task<ActionResult> UserEditPartial([Bind] UserEditViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                user.Name = model.Name;
                user.Department = model.Department;
                user.Surname = model.Surname;
                user.Patronymic = model.Patronymic;
                user.Email = model.Email;
                user.PhoneNumber = model.Phone;

                var result = await _userManager.UpdateAsync(user);
            }

            var results = await _context.Users.Include(x => x.Department).Where(x => x.LockoutEnabled == true).AsNoTracking().ToListAsync();
            List<UserViewModel> models = new List<UserViewModel>();
            foreach (var item in results)
            {
                models.Add(await UserViewModel.Initialize(item, _userManager));
            }

            var tt = Json(new { isValid = true, html = await JsonHelpers.RenderRazorViewToString(this, "UserListPartial", models) });

            return tt;
        }

        [HttpGet]
        [Route("[controller]/{action}")]
        public async Task<ActionResult> UserCreatePartial()
        {
            return PartialView("UserCreatePartial", await UserCreateViewModel.Initialize(_userManager, _roleManager, _context));
        }

        [HttpPost]
        [Route("[controller]/{action}")]
        public async Task<ActionResult> UserCreatePartial([Bind] UserCreateViewModel model)
        {
            var newUser = new User()
            {
                Name = model.Name,
                Surname = model.Surname,
                Patronymic = model.Patronymic,
                Email = model.Email,
                PhoneNumber = model.Phone,
                EmailConfirmed = true,
                HrefAvatar = "avatar_user5.png",
                UserName = model.UserName,
            };

            var department = _context.Departments.FirstOrDefault(x => x.Name == "Отдел разработки");
            newUser.Department = department;
            var result = _userManager.CreateAsync(newUser, model.Password).Result;

            if (result.Succeeded)
            {
                var employeeUser = await _userManager.FindByNameAsync(newUser.UserName);
                await _userManager.AddToRoleAsync(employeeUser, RoleNames.Employee);
            }

            var results = await _context.Users.Include(x => x.Department).Where(x => x.LockoutEnabled == true).AsNoTracking().ToListAsync();
            List<UserViewModel> models = new List<UserViewModel>();
            foreach (var item in results)
            {
                models.Add(await UserViewModel.Initialize(item, _userManager));
            }

            var tt = Json(new { isValid = true, html = await JsonHelpers.RenderRazorViewToString(this, "UserListPartial", models) });

            return tt;
        }

        [HttpPost]
        [Route("[controller]/{action}")]
        public async Task<ActionResult> UserDelete(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            user.LockoutEnabled = false;
            await _userManager.UpdateAsync(user);

            var results = await _context.Users.Include(x => x.Department).Where(x => x.LockoutEnabled == true).AsNoTracking().ToListAsync();
            List<UserViewModel> models = new List<UserViewModel>();
            foreach (var item in results)
            {
                models.Add(await UserViewModel.Initialize(item, _userManager));
            }

            var tt = Json(new { isValid = true, html = await JsonHelpers.RenderRazorViewToString(this, "UserListPartial", models) });

            return tt;
        }
    }
}
