using MiniSurveys.Domain.Modals.Base;
using MiniSurveys.Domain.Modals.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSurveys.Domain.Modals
{
    public class Question : BaseEntity
    {
        public string? Title { get; set; }

        public ICollection<Media>? Media { get; set; }

        public ICollection<Answer> Answers { get; set; }

        public int Number { get; set; } = -1;
    }
}
