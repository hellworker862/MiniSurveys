using MiniSurveys.Domain.Modals;

namespace MiniSurveys.Domain.Services.Interfaces
{
    public interface ISurveyService
    {
        public Task<IEnumerable<Survey>> GetAll();

        public Task<Survey> GetById(int id);
    }
}
