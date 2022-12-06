using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSurveys.Domain.Modals.Enums;
using MiniSurveys.Domain.Modals;
using MiniSurveys.Domain.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MiniSurveys.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace MiniSurveys.Web.Controllers
{
    [Authorize(Roles ="HR")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
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
    }
}
