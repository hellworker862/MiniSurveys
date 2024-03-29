﻿using MiniSurveys.Domain.Modals.Base;
using MiniSurveys.Domain.Modals.Enums;

namespace MiniSurveys.Domain.Modals
{
    public class Media : BaseEntity
    {
        public string Url { get; set; } = null!;

        public string? Title { get; set; }

        public TypeMedia Type { get; set; }

        public int? QuestionId { get; set; }

        public Question? Question { get; set; }
    }
}
