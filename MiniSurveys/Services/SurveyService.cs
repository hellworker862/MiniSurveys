﻿using MiniSurveys.Domain.Data;
using MiniSurveys.Domain.Modals;
using MiniSurveys.Domain.Services.Interfaces;

namespace MiniSurveys.Domain.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly ApplicationDbContext _context;

        public SurveyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Survey>> GetAll()
        {
            return _context.Surveys.ToArray();
        }

        public Task<Survey> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
