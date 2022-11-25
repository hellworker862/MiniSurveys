using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniSurveys.Domain.Modals;

namespace MiniSurveys.Domain.Data.ModelConfigurations
{
    public class SurveyModelConfiguration : IEntityTypeConfiguration<Survey>
    {
        public void Configure(EntityTypeBuilder<Survey> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(512);
            builder.Property(x => x.StartTime).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(x => x.EndTime).IsRequired();
            builder.Property(x => x.SurveyState);
            builder.Property(x => x.IsQuestionOrder).HasDefaultValue(false);
            builder.HasMany(x => x.Questions).WithOne(x => x.Survey);
        }
    }
}
