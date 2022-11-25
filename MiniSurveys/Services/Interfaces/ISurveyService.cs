using MiniSurveys.Domain.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSurveys.Domain.Services.Interfaces
{
    public interface ISurveyService
    {
        public Task<IEnumerable<Survey>> GetAll();

        public Task<Survey> GetById(int id);
    }
}
