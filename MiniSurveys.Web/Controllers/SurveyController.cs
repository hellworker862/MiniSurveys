using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSurveys.Domain.Data;
using MiniSurveys.Domain.Modals;
using MiniSurveys.Web.Helpers;
using MiniSurveys.Web.Models;
using MiniSurveys.Web.Models.Survey;
using Newtonsoft.Json;
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

        [Route("[controller]/{action}")]
        [HttpGet]
        public async Task<ActionResult> Save(IEnumerable<bool> answers)
        {
            var user = await _userManager.GetUserAsync(User);

            string nameKey = $"survey_{this.User.FindFirst(ClaimTypes.NameIdentifier).Value}";

            var model = HttpContext.Session.Get<SurveyViewModel>(nameKey);

            for (int i = 0; i < answers.Count(); i++)
            {
                if (answers.ElementAt(i) == true)
                    model!.GetQuestion().OnVote(i);
            }

            List<QuestionResult> questionResults = new List<QuestionResult>();

            foreach (QuestionViewModel question in model.Questions)
            {
                List<AnswerResult> answerResults = new List<AnswerResult>();

                foreach (AnswerViewModel answer in question.Answers)
                {
                    if (answer.isVote)
                        answerResults.Add(new AnswerResult()
                        {
                            AnswerId = answer.Id,
                        });
                }

                var tmp = new QuestionResult()
                {
                    QuestionId = question.Id,
                    AnswerResults = answerResults,
                };
                questionResults.Add(tmp);
            }

            var result = new SurveyResult()
            {
                UserId = user.Id,
                SurveyId = model.Id,
                DateTime = DateTime.Now,
                QuestionResults = questionResults,
            };

            HttpContext.Session.Clear();

            _context.SurveyResults.Add(result);
            _context.SaveChanges();

            return base.Content("/");
        }

        public async Task<ActionResult> ResultsAsync(int id)
        {
            User user = await _userManager.GetUserAsync(User);
            user = _context.Users.Include(u => u.Department).Single(u => u.Id == user.Id);
            string roles = (await _userManager.GetRolesAsync(user)).ElementAt(0);
            var allDepartmentItem = new Department() { Name = "По всем отделам", Id = 0 };
            var allDepartments = _context.Departments.AsNoTracking().ToArray();
            var surveyName = _context.Surveys.Single(s => s.Id == id).Title;

            ICollection<Department> departments = roles switch
            {
                RoleNames.Employee => new List<Department>() { allDepartmentItem },
                RoleNames.Head => new List<Department>() { allDepartmentItem, allDepartments.Single(d => d.Id == user.Department.Id) },
                RoleNames.Administrator => new List<Department>(allDepartments) { allDepartmentItem },
            };
            departments = departments.OrderBy(d => d.Id).ToArray();

            var model = new ResultViewModel(departments, surveyName, id);

            return View(model);
        }

        [Route("[controller]/{action}")]
        [HttpGet]
        public async Task<ActionResult> GetResult(int id, int fillter)
        {
            var surveyResult = fillter switch
            {
                0 => _context.SurveyResults.Include(s => s.QuestionResults)
                                           .ThenInclude(q => q.Question)
                                           .Include(q => q.QuestionResults)
                                           .ThenInclude(q => q.AnswerResults)
                                           .ThenInclude(a => a.Answer)
                                           .Single(s => s.Id == id),
                _ => _context.SurveyResults.Include(s => s.QuestionResults)
                                           .ThenInclude(q => q.Question)
                                           .Include(q => q.QuestionResults)
                                           .ThenInclude(q => q.AnswerResults)
                                           .ThenInclude(a => a.Answer)
                                           .Single(s => s.Id == id && s.User.Department.Id == fillter),
            };

            var model = new ResultData(surveyResult);


            return Json(model);
        }
    }
}