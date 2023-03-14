using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniSurveys.Domain.Modals;

namespace MiniSurveys.Domain.Data.ModelConfigurations
{
    public class AnswerResultModelConfiguration : IEntityTypeConfiguration<AnswerResult>
    {
        public void Configure(EntityTypeBuilder<AnswerResult> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();
            builder.HasOne(x => x.Answer);
        }
    }
}
