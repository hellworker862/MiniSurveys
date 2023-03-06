using MiniSurveys.Domain.Modals.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSurveys.Domain.Modals
{
    public class Result : BaseEntity
    {
        public DateTime DateTimeEnd { get; set; }

        public int UserId { get; set; }

        public int SurveyId { get; set; }

        public User User { get; set; }

        public Survey Survey { get; set;}
    }
}
