using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSurveys.Domain.Data;
using MiniSurveys.Domain.Modals;
using MiniSurveys.Web.Helpers;
using MiniSurveys.Web.Models;
using MiniSurveys.Web.Models.Survey;
using System.Security.Claims;

namespace MiniSurveys.Web.Controllers
{
    [Authorize]
    public class SurveyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public SurveyController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult> Index(bool isActive = true, string? stringSearch = "")
        {
            IEnumerable<Survey> searchResult;

            if (isActive)
                searchResult = await _context.Surveys.Where(x => x.EndTime > DateTime.Now && EF.Functions.Like(x.Title, $"%{stringSearch}%")).AsNoTracking().ToArrayAsync();
            else
                searchResult = await _context.Surveys.Where(x => x.EndTime <= DateTime.Now && EF.Functions.Like(x.Title, $"%{stringSearch}%")).AsNoTracking().ToArrayAsync();

            return View(searchResult);
        }

        [Route("[controller]/{action}")]
        [HttpGet]
        public async Task<ActionResult> GetSurveyListPartial(bool isActive, string stringSearch)
        {
            IEnumerable<Survey> searchResult;

            if (isActive)
                searchResult = await _context.Surveys.Where(x => x.EndTime > DateTime.Now && EF.Functions.Like(x.Title, $"%{stringSearch}%")).AsNoTracking().ToArrayAsync();
            else
                searchResult = await _context.Surveys.Where(x => x.EndTime <= DateTime.Now && EF.Functions.Like(x.Title, $"%{stringSearch}%")).AsNoTracking().ToArrayAsync();

            return PartialView("SurveyListPartial", searchResult);
        }

        [Route("[controller]/{id}")]
        [HttpGet]
        public async Task<ActionResult> Survey(int id)
        {
            string nameKey = $"survey_{this.User.FindFirst(ClaimTypes.NameIdentifier).Value}";

            if (HttpContext.Session.Keys.Contains(nameKey))
            {
                var model = HttpContext.Session.Get<SurveyViewModel>(nameKey);
                return View(model);
            }

            var survey = await _context.Surveys.Include(x => x.Questions)
                                                            .ThenInclude(n => n.Answers)
                                                                .ThenInclude(am => am.Media)
                                                .Include(x => x.Questions)
                                                            .ThenInclude(m => m.Media)
                                                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);


            if (survey != null)
            {
                var viewModel = new SurveyViewModel(survey);
                HttpContext.Session.Set<SurveyViewModel>(nameKey, viewModel);
                return View(viewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("[controller]/{action}")]
        [HttpGet]
        public async Task<ActionResult> GetQuestion(bool isNext, IEnumerable<bool> answers)
        {
            string nameKey = $"survey_{this.User.FindFirst(ClaimTypes.NameIdentifier).Value}";

            var model = HttpContext.Session.Get<SurveyViewModel>(nameKey);

            for (int i = 0; i < answers.Count(); i++)
            {
                if (answers.ElementAt(i) == true)
                    model!.GetQuestion().OnVote(i);
            }

            if (isNext)
            {
                if (model!.isNext())
                {
                    model.Next();
                }
            }
            else
            {
                if (model!.isBack())
                {
                    model.Back();
                }
            }
            HttpContext.Session.Set<SurveyViewModel>(nameKey, model);

            return await Task.FromResult(PartialView("QuestionPartial", model));
        }

        [Route("/Save")]
        [HttpGet]
        public async Task<ActionResult> Save()
        {
            var user = await _userManager.GetUserAsync(User);

            string nameKey = $"survey_{this.User.FindFirst(ClaimTypes.NameIdentifier).Value}";

            var model = HttpContext.Session.Get<SurveyViewModel>(nameKey);

            var result = new SurveyResult()
            {
                UserId = user.Id,
                SurveyId = model.Id,
                DateTime = DateTime.Now,
            };

            HttpContext.Session.Clear();

            _context.SurveyResults.Add(result);
            _context.SaveChanges();

            return Redirect("/");
        }

        public ActionResult Results(int id)
        {
            return View(id);
        }

        [Route("[controller]/{action}")]
        [HttpGet]
        public async Task<ActionResult> GetResult(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            int ot1 = _context.SurveyResults.Where(o => o.SurveyId == id && o.User.Department.Name == "Отдел разработки").Select(o => o.User).Count();
            int ot2 = _context.SurveyResults.Where(o => o.SurveyId == id && o.User.Department.Name == "Отдел тестирования").Select(o => o.User).Count();
            int ot3 = _context.SurveyResults.Where(o => o.SurveyId == id && o.User.Department.Name == "Кадровая служба").Select(o => o.User).Count();

            var model = new ResultViewModel()
            {
                Data = new int[]
                {
                    ot1,
                    ot2,
                    ot3,
                },

                Title = new string[]
                {
                    "Отдел разработки",
                    "Отдел тестирования",
                    "Кадровая служба"
                }

            };

            var json = Json(model);

            return json;
        }
    }
}