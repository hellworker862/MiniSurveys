using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSurveys.Domain.Data;
using MiniSurveys.Domain.Modals;
using MiniSurveys.Domain.Modals.Enums;
using MiniSurveys.Domain.Services;
using MiniSurveys.Web.Helpers;
using MiniSurveys.Web.Models.Survey.NewSurvey;
using MiniSurveys.Web.Models.UserView;
using System.Data;

namespace MiniSurveys.Web.Controllers
{
    [Authorize(Roles = "HR")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IWebHostEnvironment _appEnvironment;

        public AdminController(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _appEnvironment = appEnvironment;
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

        [HttpGet]
        public ActionResult CreateSurvey()
        {
            var model = new NewSurveyViewModel();
            model.Questions.Add(new NewQuestionViewModel());
            model.Questions.ElementAt(0).Answers.Add(new NewAnswerViewModel());
            model.Questions.ElementAt(0).Answers.Add(new NewAnswerViewModel());
            model.Questions.ElementAt(0).Answers.Add(new NewAnswerViewModel());
            model.Questions.ElementAt(0).Medias.Add(new NewMediaViewModel());



            return View("~/Views/Admin/NewSurvey/CreateSurvey.cshtml", model);
        }

        [HttpPost]
        public ActionResult CreateSurvey(NewSurveyViewModel model)
        {
            if (model.Start < DateTime.Now)
                ModelState.AddModelError("Start", "Дата начала не может быть меньше текущего времени");

            if (model.Start >= model.End)
                ModelState.AddModelError("Start", "Дата начала не может быть больше или равна дате окончания");

            if (_context.Surveys.Any(s => model.Start < s.EndTime && s.StartTime < model.End))
                ModelState.AddModelError("Start", "Этот промежуток времени занят");

            if (model.Questions.Count < 1)
                ModelState.AddModelError("Questions", "Вопросов не может быть меньше 1");

            for (int i = 0; i < model.Questions.Count; i++)
            {
                if (model.Questions[i].Answers.Count < 1)
                    ModelState.AddModelError($"Questions[{i}].Answers", "Ответов не может быть меньше 1");
            }

            if (ModelState.IsValid)
            {
                var survey = new Survey()
                {
                    StartTime = model.Start,
                    EndTime = model.End,
                    Title = model.Title,
                    Questions = new List<Question>(),
                };

                for (int i = 0; i < model.Questions.Count; i++)
                {
                    var modelQuestion = model.Questions[i];

                    var question = new Question()
                    {
                        Title = modelQuestion.QuestionTitle,
                        Media = new List<Media>(),
                        Answers = new List<Answer>(),
                    };

                    for (int j = 0; j < modelQuestion.Medias.Count; j++)
                    {
                        var modelmMedia = modelQuestion.Medias[j];
                        var signature = modelmMedia.Signature;
                        var type = (TypeMedia)int.Parse(modelmMedia.SelectedType.Value);
                        var link = type switch 
                        {
                            TypeMedia.Image => modelmMedia.Link,
                            TypeMedia.Video => YoutubeService.GetVideoId(modelmMedia.Link),
                            _ => null
                        };

                        if (String.IsNullOrEmpty(link))
                        {
                            ModelState.AddModelError($"Questions[{i}].Medias[{j}].Link", "Некорректная ссылка");
                            return View("~/Views/Admin/NewSurvey/CreateSurvey.cshtml", model);
                        }

                        var media = new Media()
                        {
                            Title = signature,
                            Url = link,
                            Type = type,
                        };
                        question.Media.Add(media);
                    }

                    for (int j = 0; j < modelQuestion.Answers.Count; j++)
                    {
                        var modelmAnswer = modelQuestion.Answers[j];
                        var title = modelmAnswer.AnswerTitle;
                        var answer = new Answer()
                        {
                            Title = title,
                        };

                        if (modelmAnswer.IsActive)
                        {
                            var signature = modelmAnswer.SignatureMedia;
                            var type = (TypeMedia)int.Parse(modelmAnswer.SelectedTypeMedia.Value);
                            var link = type switch
                            {
                                TypeMedia.Image => modelmAnswer.LinkMedia,
                                TypeMedia.Video => YoutubeService.GetVideoId(modelmAnswer.LinkMedia),
                                _ => null
                            };

                            if (String.IsNullOrEmpty(link))
                            {
                                ModelState.AddModelError($"Questions[{i}].Medias[{j}].Link", "Некорректная ссылка");
                                return View("~/Views/Admin/NewSurvey/CreateSurvey.cshtml", model);
                            }

                            var media = new Media()
                            {
                                Title = signature,
                                Url = link,
                                Type = type,
                            };
                            answer.Media = media;
                        }
                        question.Answers.Add(answer);
                    }

                    survey.Questions.Add(question);
                }

                _context.Add(survey);
                _context.SaveChanges();

                return RedirectToAction("Index", "Admin");
            }

            return View("~/Views/Admin/NewSurvey/CreateSurvey.cshtml", model);
        }

        [HttpGet]
        public async Task<ActionResult> Users()
        {
            var result = await _context.Users.Where(x => x.LockoutEnabled == false).Include(x => x.Department).ToListAsync();
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
            | EF.Functions.Like(x.Email, $"%{searchString}%") && x.LockoutEnabled == false).Include(x => x.Department).ToListAsync();

            List<UserViewModel> users = new List<UserViewModel>();
            foreach (var item in result)
            {
                users.Add(await UserViewModel.Initialize(item, _userManager));
            }

            return PartialView("UserListPartial", users);
        }

        [HttpGet]
        [Route("[controller]/{action}")]
        [NoDirectAccess]
        public async Task<ActionResult> UserEditPartial(string userName)
        {
            var user = await _context.Users.Include(d => d.Department).Where(x => x.LockoutEnabled == false).AsNoTracking().FirstOrDefaultAsync(x => x.UserName == userName);
            var model = await UserEditViewModel.Initialize(user, _userManager, _roleManager, _context);

            return PartialView("UserEditPartial", model);
        }

        [HttpPost]
        [Route("[controller]/{action}")]
        public async Task<ActionResult> UserEditPartial(UserEditViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());
            var checkEmail = await _userManager.FindByEmailAsync(model.Email);
            var checkLogin = await _userManager.FindByNameAsync(model.UserName);

            if (checkEmail != null && user.Id != checkEmail.Id)
            {
                ModelState.AddModelError("Email", "Эл.почта уже используется");
            }

            if (checkLogin != null && user.Id != checkLogin.Id)
            {
                ModelState.AddModelError("UserName", "Логин уже используется");
            }

            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    user.Name = model.Name;
                    user.Department = _context.Departments.FirstOrDefault(x => x.Id == model.Department.Id);
                    user.Surname = model.Surname;
                    user.Patronymic = model.Patronymic;
                    user.Email = model.Email;
                    user.PhoneNumber = model.Phone;
                    user.HrefAvatar = model.Avatar != null ? model.Avatar.FileName : user.HrefAvatar;

                    await _userManager.UpdateAsync(user);

                    var selectRole = _roleManager.Roles.FirstOrDefault(x => x.Id == model.Role.Id)?.Name;
                    var userRole = (await _userManager.GetRolesAsync(user)).ElementAt(0);

                    if (selectRole != null && userRole != selectRole)
                    {
                        await _userManager.AddToRoleAsync(user, selectRole);
                        await _userManager.RemoveFromRoleAsync(user, userRole);
                    }

                    if (model.Avatar != null)
                    {
                        string path = "/img/UsersImages/" + model.Avatar.FileName;

                        using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                        {
                            await model.Avatar.CopyToAsync(fileStream);
                        }
                    }
                }

                var results = await _context.Users.Include(x => x.Department).Where(x => x.LockoutEnabled == false).AsNoTracking().ToListAsync();
                List<UserViewModel> models = new List<UserViewModel>();
                foreach (var item in results)
                {
                    models.Add(await UserViewModel.Initialize(item, _userManager));
                }

                var tt = Json(new { isValid = true, html = await JsonHelpers.RenderRazorViewToString(this, "UserListPartial", models) });

                return tt;
            }
            model.InitializeSelectList(_roleManager, _context);

            return PartialView("UserEditPartial", model);
        }

        [HttpGet]
        [Route("[controller]/{action}")]
        public async Task<ActionResult> UserCreatePartial()
        {
            return PartialView("UserCreatePartial", await UserCreateViewModel.Initialize(_userManager, _roleManager, _context));
        }

        [HttpPost]
        [Route("[controller]/{action}")]
        public async Task<ActionResult> UserCreatePartial(UserCreateViewModel model)
        {
            if (model.Email != null && await _userManager.FindByEmailAsync(model.Email) != null)
            {
                ModelState.AddModelError("Email", "Эл.почта уже используется");
            }

            if (model.UserName != null && await _userManager.FindByNameAsync(model.UserName) != null)
            {
                ModelState.AddModelError("UserName", "Логин уже используется");
            }

            if (ModelState.IsValid)
            {
                var newUser = new User()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Patronymic = model.Patronymic,
                    Email = model.Email,
                    PhoneNumber = model.Phone,
                    EmailConfirmed = true,
                    HrefAvatar = model.Avatar.FileName ?? "avatar_default.png",
                    UserName = model.UserName,
                };

                var department = _context.Departments.FirstOrDefault(x => x.Id == model.Department.Id);
                newUser.Department = department;
                var result = _userManager.CreateAsync(newUser, model.Password).Result;

                if (result.Succeeded)
                {
                    var employeeUser = await _userManager.FindByNameAsync(newUser.UserName);
                    var selectRole = _roleManager.Roles.FirstOrDefault(x => x.Id == model.Role.Id)?.Name;
                    await _userManager.AddToRoleAsync(employeeUser, selectRole);

                    string path = "/img/UsersImages/" + model.Avatar.FileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await model.Avatar.CopyToAsync(fileStream);
                    }
                }

                var results = await _context.Users.Include(x => x.Department).Where(x => x.LockoutEnabled == false).AsNoTracking().ToListAsync();
                List<UserViewModel> models = new List<UserViewModel>();
                foreach (var item in results)
                {
                    models.Add(await UserViewModel.Initialize(item, _userManager));
                }

                var tt = Json(new { isValid = true, html = await JsonHelpers.RenderRazorViewToString(this, "UserListPartial", models) });

                return tt;
            }
            model.InitializeSelectList(_roleManager, _context);

            return PartialView("UserCreatePartial", model);
        }

        [HttpPost]
        [Route("[controller]/{action}")]
        public async Task<ActionResult> UserDelete(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            user.LockoutEnabled = true;
            user.LockoutEnd = DateTime.MaxValue;
            await _userManager.UpdateAsync(user);

            var results = await _context.Users.Include(x => x.Department).Where(x => x.LockoutEnabled == false).AsNoTracking().ToListAsync();
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
