
namespace Persistence.Data.Configurations.Factory
{
    internal class EnvironmentalAgentConfigurations : BaseEntityConfigurations<EnvironmentalAgent, string>
    {
        public override void Configure(EntityTypeBuilder<EnvironmentalAgent> builder)
        {
            base.Configure(builder);


            builder.HasMany(E => E.Reports)
                .WithOne(R => R.EnvironmentalAgent)
                .HasForeignKey(R => R.EnvironmentalAgentId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
