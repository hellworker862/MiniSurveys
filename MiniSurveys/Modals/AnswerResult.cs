﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSurveys.Domain.Modals
{
    public class AnswerResult
    {
        public int Id { get; set; }
        public Answer Answer { get; set; } = null!;
        public int AnswerId { get; set; }
        public int QuestionResultId { get; set; }
        public QuestionResult QuestionResult { get; set; } = null!;
    }
}
