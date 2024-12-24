
namespace Persistence.Data.Configurations.Factory
{
    internal class FactoriesEnvironmentalAgentsConfigurations : IEntityTypeConfiguration<FactoriesEnvironmentalAgents>
    {
        public void Configure(EntityTypeBuilder<FactoriesEnvironmentalAgents> builder)
        {

            builder.HasKey(fo => new { fo.FactoryUserId, fo.EnvironmentalAgentId }); // Composite key

            builder.HasOne(fo => fo.Factory)
                   .WithMany(f => f.FactoryOrganizations)
                   .HasForeignKey(fo => fo.FactoryUserId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(fo => fo.EnvironmentalAgent)
                   .WithMany(o => o.FactoryOrganizations)
                   .HasForeignKey(fo => fo.EnvironmentalAgentId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
