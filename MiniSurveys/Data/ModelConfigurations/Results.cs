using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniSurveys.Domain.Modals;

namespace MiniSurveys.Domain.Data.ModelConfigurations
{
    public class Results : IEntityTypeConfiguration<Result>
    {
        public void Configure(EntityTypeBuilder<Result> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();
            builder.HasOne(x => x.User);
            builder.HasOne(x => x.Survey);
        }
    }
}
