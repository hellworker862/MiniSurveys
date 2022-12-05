using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSurveys.Domain.Data;
using MiniSurveys.Domain.Modals;
using MiniSurveys.Domain.Modals.Enums;
using MiniSurveys.Web.Helpers;
using MiniSurveys.Web.Models.Survey;
using System.Security.Claims;

namespace MiniSurveys.Web.Controllers
{
    [Authorize]
    public class SurveyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SurveyController(ApplicationDbContext context)
        {
            _context = context;
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

        [Route("[controller]/{action}")]
        [HttpGet]
        public async Task<ActionResult> GetSurveyListPartial(bool isActive, string stringSearch)
        {
            IEnumerable<Survey> searchResult;

            if (isActive)
                searchResult = await _context.Surveys.Where(x => x.SurveyState != SurveyStateTypeEnum.Finished && EF.Functions.Like(x.Title, $"%{stringSearch}%")).AsNoTracking().ToArrayAsync();
            else
                searchResult = await _context.Surveys.Where(x => x.SurveyState == SurveyStateTypeEnum.Finished && EF.Functions.Like(x.Title, $"%{stringSearch}%")).AsNoTracking().ToArrayAsync();

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
    }
}
