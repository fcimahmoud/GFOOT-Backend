
namespace Persistence.Data.Configurations.Factory
{
    internal class FactoryRecommendationConfigurations
        : BaseEntityConfigurations<FactoryRecommendation, string>
    {
        public override void Configure(EntityTypeBuilder<FactoryRecommendation> builder)
        {
            base.Configure(builder);

            builder.Property(N => N.RecHeader).IsRequired();
            builder.Property(N => N.RecBody).IsRequired();
        }
    }
}
