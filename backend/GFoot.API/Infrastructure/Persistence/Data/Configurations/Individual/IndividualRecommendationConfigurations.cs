
namespace Persistence.Data.Configurations.Individual
{
    internal class IndividualRecommendationConfigurations
    : BaseEntityConfigurations<IndividualRecommendation, string>
    {
        public override void Configure(EntityTypeBuilder<IndividualRecommendation> builder)
        {
            base.Configure(builder);

            builder.Property(N => N.RecHeader).IsRequired();
            builder.Property(N => N.RecBody).IsRequired();
        }
    }
}
