using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSurveys.Domain.Data;
using MiniSurveys.Domain.Modals;
using MiniSurveys.Web.Helpers;
using MiniSurveys.Web.Models;
using MiniSurveys.Web.Models.Survey;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Drawing.Chart.Style;
using OfficeOpenXml.Style;
using System.Drawing;
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
            ResultData model = await GetResultDataAsync(id, fillter);

            return Json(model);
        }

        [Route("[controller]/{action}")]
        public async Task<ActionResult> ExportToExcel(ResultViewModel viewModel)
        {
            char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            int row = 4;
            int heightRow = 15;
            int fillter = int.Parse(viewModel.SelectedDepartment.Value);
            string surveyedUsersTitle = "Количество прошедших";
            string departmentTitle = fillter switch
            {
                1 => $" по {DepartmentNames.Hr}",
                2 => $" по {DepartmentNames.Dev}",
                3 => $" по {DepartmentNames.Test}",
                _ => "",
            };
            ResultData model = await GetResultDataAsync(viewModel.Id, fillter);
            var package = new ExcelPackage();
            var workSheet = package.Workbook.Worksheets.Add("Результаты опроса");

            workSheet.Cells["A1:N1"].Merge = true;
            workSheet.Cells["A1"].Value = "Результаты опроса " + model.Title + departmentTitle;
            workSheet.Cells["A1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            workSheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["A1"].Style.Font.Bold = true;
            workSheet.Cells["A1"].Style.Font.Size = 20;

            workSheet.Cells["A3:D3"].Merge = true;
            workSheet.Cells["A3"].Value = surveyedUsersTitle;
            workSheet.Cells["A3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            workSheet.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["A3:D3"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A3:D3"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A3:D3"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A3:D3"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A3"].Style.Font.Bold = true;
            workSheet.Cells["A3"].Style.Font.Size = 14;

            for (int i = 0; i <= model.SurveyedUsers.Count; i++)
            {
                workSheet.Cells[$"A{4 + i}:B{4 + i}"].Merge = true;
                workSheet.Cells[$"C{4 + i}:D{4 + i}"].Merge = true;
            }
            List<string> listHeader = new List<string>()
            {
                "A4:B4","C4:D4"
            };
            listHeader.ForEach(c =>
            {
                workSheet.Cells[c].Style.Font.Bold = true;
                workSheet.Cells[c].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[c].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[c].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[c].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            });
            workSheet.Cells[listHeader[0]].Value = "Наименование";
            workSheet.Cells[listHeader[1]].Value = "Количество";

            for (int i = 0; i < model.SurveyedUsers.Count; i++)
            {
                workSheet.Cells[$"A{5 + i}"].Value = model.SurveyedUsers.ElementAt(i).Title;
                workSheet.Cells[$"C{5 + i}"].Value = model.SurveyedUsers.ElementAt(i).Value;
                row++;
            }

            for (int i = 0; i < model.SurveyedUsers.Count; i++)
            {
                for (int r = 1; r <= 4; r++)
                {
                    workSheet.Cells[5 + i, r].Style.Font.Size = 10;
                    workSheet.Cells[5 + i, r].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[5 + i, r].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[5 + i, r].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[5 + i, r].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                }
            }
            var pieChart = workSheet.Drawings.AddPieChart("crtExtensionsSize", ePieChartType.Pie);
            pieChart.SetPosition(2, 0, 5, 0);
            pieChart.SetSize(340, 340);
            pieChart.Series.Add(ExcelRange.GetAddress(5, 3, row, 3), ExcelRange.GetAddress(5, 1, row, 1));
            pieChart.StyleManager.SetChartStyle(ePresetChartStyle.PieChartStyle12);
            pieChart.DataLabel.ShowPercent = true;
            pieChart.DataLabel.Font.Color = Color.White;
            pieChart.Title.Text = surveyedUsersTitle;

            row = 21;
            int chartCount = 1;

            for (int i = 0; i < model.QuestionResultDatas.Count; i++)
            {
                var question = model.QuestionResultDatas.ElementAt(i);
                int maxRow = question.Answers.Count;
                int titleLength = question.Title.Count();

                workSheet.Cells[$"A{row}:B{row}"].Merge = true;
                workSheet.Cells[$"A{row}"].Value = question.Title;
                workSheet.Cells[$"A{row}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheet.Cells[$"A{row}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                workSheet.Cells[$"A{row}:B{row}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[$"A{row}:B{row}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[$"A{row}:B{row}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[$"A{row}:B{row}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Column(1).Width = titleLength / 1.5;
                workSheet.Column(2).Width = titleLength / 1.5;
                workSheet.Cells[$"A{row}"].Style.Font.Bold = true;
                workSheet.Cells[$"A{row}"].Style.Font.Size = 14;

                row++;
                List<string> listQestionHeader = new List<string>()
                {
                $"A{row}",$"B{row}"
                };
                listQestionHeader.ForEach(c =>
                {
                    workSheet.Cells[c].Style.Font.Bold = true;
                    workSheet.Cells[c].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[c].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[c].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[c].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                });
                workSheet.Cells[listQestionHeader[0]].Value = "Ответ";
                workSheet.Cells[listQestionHeader[1]].Value = "Количество";

                for (int j = 0; j < maxRow; j++)
                {
                    row++;
                    workSheet.Row(row).Height = heightRow;
                    workSheet.Cells[$"A{row}"].Value = question.Answers.ElementAt(j).Title;
                    workSheet.Cells[$"B{row}"].Value = question.Answers.ElementAt(j).Value;
                    workSheet.Cells[row, 1].Style.Font.Size = 10;
                    workSheet.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[row, 2].Style.Font.Size = 10;
                    workSheet.Cells[row, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[row, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[row, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[row, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                }

                var chart = workSheet.Drawings.AddBarChart($"char_{chartCount}", eBarChartType.BarClustered);
                chart.SetPosition(row - maxRow - 2, 0, 4, 0);
                chart.SetSize(340, (int)(maxRow * 2.75) * heightRow);
                chart.Series.Add(ExcelRange.GetAddress(row - maxRow + 1, 2, row, 2), ExcelRange.GetAddress(row - maxRow + 1, 1, row, 1));
                chart.StyleManager.SetChartStyle(ePresetChartStyle.PieChartStyle12);
                chart.Legend.Remove();
                chart.DataLabel.ShowPercent = true;
                chart.DataLabel.ShowLeaderLines = false;
                chart.DataLabel.ShowLegendKey = false;
                chart.DataLabel.ShowSeriesName = false;
                chart.DataLabel.ShowCategory = false;
                chart.ShowHiddenData = false;
                chart.DataLabel.Font.Color = Color.White;
                chart.Title.Text = "Выбранные ответы";
                chartCount++;

                row += 5;
            }

            byte[] fs = await package.GetAsByteArrayAsync();
            string fileType = "application/xlsx";
            string fileName = "result.xlsx";

            return File(fs, fileType, fileName);
        }

        private async Task<ResultData> GetResultDataAsync(int id, int fillter)
        {
            var data = _context.Surveys.Where(q => q.Id == id).Include(s => s.Questions).ThenInclude(q => q.Answers).Single();
            var questionResultDatas = new List<QuestionResultData>();

            foreach (Question question in data.Questions)
            {
                var answers = new List<ChartItem>();

                foreach (Answer answer in question.Answers)
                {
                    var count = fillter switch
                    {
                        0 => _context.AnswerResults.Where(a => a.AnswerId == answer.Id).Count(),
                        _ => _context.AnswerResults.Where(a => a.AnswerId == answer.Id && a.QuestionResult.SurveyResult.User.Department.Id == fillter).Count(),
                    };
                    answers.Add(new ChartItem
                    {
                        Title = answer.Title,
                        Value = count,
                    });
                }

                var tmp = new QuestionResultData
                {
                    Title = question.Title,
                    Answers = answers,
                };

                questionResultDatas.Add(tmp);
            }

            int testedUsers = fillter switch
            {
                0 => _context.SurveyResults.Where(x => x.SurveyId == id).Count(),
                _ => _context.SurveyResults.Where(x => x.SurveyId == id && x.User.Department.Id == fillter).Count(),
            };
            int allUsers = fillter switch
            {
                0 => _context.Users.Count(),
                _ => _context.Users.Where(x => x.Department.Id == fillter).Count(),
            };
            var model = new ResultData(data.Title, allUsers, testedUsers, questionResultDatas);

            return model;
        }

    }
}