
namespace Persistence.Data.Configurations.Factory
{
    internal class ReportConfigurations 
        : BaseEntityConfigurations<Report, string>
    {
        public override void Configure(EntityTypeBuilder<Report> builder)
        {
            base.Configure(builder);

            builder.Property(R => R.ReportBody).IsRequired();

        }
    }
}
