using MiniSurveys.Domain.Modals.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSurveys.Domain.Modals
{
    public class Answer : BaseEntity
    {
        public string? Title { get; set; }

        public Media? Media { get; set; }

        public Question Question { get; set; }

        public int QuestionId { get; set; }
    }
}
