using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniSurveys.Domain.Modals;

namespace MiniSurveys.Domain.Data.ModelConfigurations
{
    public class QuestionModelConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(512);
            builder.Property(x => x.Number).HasDefaultValue(-1);
            builder.HasMany(x => x.Media);
        }
    }
}
