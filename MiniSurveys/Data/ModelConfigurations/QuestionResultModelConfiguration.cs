using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniSurveys.Domain.Modals;

namespace MiniSurveys.Domain.Data.ModelConfigurations
{
    public class QuestionResultModelConfiguration : IEntityTypeConfiguration<QuestionResult>
    {
        public void Configure(EntityTypeBuilder<QuestionResult> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();
            builder.HasOne(x => x.Question);
            builder.HasMany(x => x.AnswerResults).WithOne(a => a.QuestionResult);
            builder.HasOne(x => x.SurveyResult).WithMany(s => s.QuestionResults).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
