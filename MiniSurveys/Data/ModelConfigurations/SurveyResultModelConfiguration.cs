using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniSurveys.Domain.Modals;

namespace MiniSurveys.Domain.Data.ModelConfigurations
{
    public class SurveyResultModelConfiguration : IEntityTypeConfiguration<SurveyResult>
    {
        public void Configure(EntityTypeBuilder<SurveyResult> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();
            builder.HasOne(x => x.User);
            builder.HasOne(x => x.Survey);
        }
    }
}
