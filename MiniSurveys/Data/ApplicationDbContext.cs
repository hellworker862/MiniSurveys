using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniSurveys.Domain.Modals;
using MiniSurveys.Domain.Modals.Enums;

namespace MiniSurveys.Domain.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<SurveyResult> SurveyResults { get; set; }
        public DbSet<QuestionResult> QuestionResults { get; set; }
        public DbSet<AnswerResult> AnswerResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var media = new List<Media>()
            {
                new Media
                {
                    Id = 1,
                    QuestionId = 3,
                    Type = TypeMediaEnum.Video,
                    Url = "<iframe width=\"560\" height=\"315\" src=\"https://www.youtube.com/embed/6gS1Bp4LZLc\" title=\"YouTube video player\" frameborder=\"0\" allow=\"accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture\" allowfullscreen></iframe>",
                    Title = "Ёжик в тумане. Мультфильм HD (1975г.)"
                },
                new Media
                {
                    Id = 2,
                    QuestionId = 3,
                    Type = TypeMediaEnum.Image,
                    Url = "https://proprikol.ru/wp-content/uploads/2021/12/kartinki-ezhika-na-avu-32.jpg",
                    Title = "Фото ёжика 1"
                },
                new Media
                {
                    Id = 3,
                    QuestionId = 3,
                    Type = TypeMediaEnum.Image,
                    Url = "https://bugaga.ru/uploads/posts/2016-04/1461680876_ezhik-vampir-11.jpg",
                    Title = "Фото ёжика 2"
                },
                new Media
                {
                    Id = 4,
                    Type = TypeMediaEnum.Image,
                    Url = "https://www.b17.ru/foto/uploaded/upl_1637170559_396979_p74dn.jpg",
                    Title = "С коллегами"
                }
            };


            #region answers
            var answers1 = new List<Answer>()
            {
                new Answer()
                {
                    Id = 1,
                    Title = "😀",
                    QuestionId = 1,
                },
                new Answer()
                {
                    Id = 2,
                    Title = "😐",
                    QuestionId = 1,
                },
                new Answer()
                {
                    Id = 3,
                    Title = "😴",
                    QuestionId = 1,
                },
                new Answer()
                {
                    Id = 4,
                    Title = "😡",
                    QuestionId = 1,
                },
                new Answer()
                {
                    Id = 5,
                    Title = "😭",
                    QuestionId = 1,
                },
            };

            var answers2 = new List<Answer>()
            {
                new Answer()
                {
                    Id = 6,
                    Title = "С коллегами",
                    QuestionId = 2,
                    MediaId = 4,
                },
                new Answer()
                {
                    Id = 7,
                    Title = "Один",
                    QuestionId = 2,
                },
                new Answer()
                {
                    Id = 8,
                    Title = "С семьей",
                    QuestionId = 2,
                },
                new Answer()
                {
                    Id = 9,
                    Title = "С друзьями",
                    QuestionId = 2,
                },
            };

            var answers3 = new List<Answer>()
            {
                new Answer()
                {
                    Id = 10,
                    Title = "Очень хорошо",
                    QuestionId = 3,
                },
                new Answer()
                {
                    Id = 11,
                    Title = "Хорошо",
                    QuestionId = 3,
                },
                new Answer()
                {
                    Id = 12,
                    Title = "Удовлетворительно",
                    QuestionId = 3,
                },
                new Answer()
                {
                    Id = 13,
                    Title = "Мне он не нравится",
                    QuestionId = 3,
                },
            };

            var answers4 = new List<Answer>()
            {
                new Answer()
                {
                    Id = 14,
                    Title = "Ответ 1",
                    QuestionId = 4,
                },
                new Answer()
                {
                    Id = 15,
                    Title = "Ответ 2",
                    QuestionId = 4,
                },
                new Answer()
                {
                    Id = 16,
                    Title = "Ответ 3",
                    QuestionId = 4,
                },
            };

            var answers5 = new List<Answer>()
            {
                new Answer()
                {
                    Id = 17,
                    Title = "Ответ 1",
                    QuestionId = 5,
                },
                new Answer()
                {
                    Id = 18,
                    Title = "Ответ 2"
                    ,
                    QuestionId = 5,
                },
                new Answer()
                {
                    Id = 19,
                    Title = "Ответ 3"
                    ,
                    QuestionId = 5,
                },
            };


            #endregion

            #region questions
            var questions1 = new List<Question>()
            {
                new Question()
                {
                    Id = 1,
                    Title = "Как прошел ваш рабочий день?",
                    Number = 1,
                    SurveyId = 1,
                },
                new Question()
                {
                    Id = 2,
                    Title = "С кем вы обычно ходите обедать?",
                    Number = 2,
                    SurveyId = 1,
                },
                new Question()
                {
                    Id = 3,
                    Title = "Как вы относитесь к своему начальнику?",
                    Number = 3,
                    SurveyId = 1,
                },
            };

            var questions2 = new List<Question>()
            {
                new Question()
                {
                    Id = 4,
                    Title = "Как прошел ваш рабочий день?",
                    Number = 1,
                    SurveyId = 2,
                },
                new Question()
                {
                    Id = 5,
                    Title = "Как прошел ваш рабочий день?",
                    Number = 2,
                    SurveyId = 2,
                },
            };
            #endregion

            #region surveys
            var surveys = new List<Survey>()
            {
                 new Survey
                {
                    Id = 1,
                    Title = "Тест 1",
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddDays(2),
                },

                new Survey
                {
                    Id = 2,
                    Title = "Тест 2",
                    StartTime = DateTime.Now.AddDays(2).AddHours(2),
                    EndTime = DateTime.Now.AddDays(4).AddHours(2),
                },

                new Survey()
                {
                    Id = 3,
                    Title = "Тест 0",
                    StartTime = DateTime.Now.AddDays(-4).AddHours(-2),
                    EndTime = DateTime.Now.AddDays(-2).AddHours(-2),
                },
            };
            #endregion

            var questions = new List<Question>();
            var answers = new List<Answer>();
            questions.AddRange(questions1);
            questions.AddRange(questions2);
            answers.AddRange(answers1);
            answers.AddRange(answers2);
            answers.AddRange(answers3);
            answers.AddRange(answers4);
            answers.AddRange(answers5);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = DepartmentNames.Hr },
                new Department { Id = 2, Name = DepartmentNames.Dev, },
                new Department { Id = 3, Name = DepartmentNames.Test, });
            modelBuilder.Entity<Survey>().HasData(surveys);
            modelBuilder.Entity<Question>().HasData(questions);
            modelBuilder.Entity<Media>().HasData(media);
            modelBuilder.Entity<Answer>().HasData(answers);
            base.OnModelCreating(modelBuilder);
        }
    }
}
