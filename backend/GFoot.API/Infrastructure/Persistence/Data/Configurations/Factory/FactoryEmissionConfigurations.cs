
namespace Persistence.Data.Configurations.Factory
{
    internal class FactoryEmissionConfigurations 
        : BaseEntityConfigurations<FactoryEmission, string>
    {
        public override void Configure(EntityTypeBuilder<FactoryEmission> builder)
        {
            base.Configure(builder);

            builder.Property(E => E.ElectricityConsumptionAmount).HasColumnType("decimal(8,2)");
            builder.Property(E => E.FuelConsumptionAmount).HasColumnType("decimal(8,2)");
            builder.Property(E => E.AverageTruckDistance).HasColumnType("decimal(8,2)");
            builder.Property(E => E.AverageTruckWeight).HasColumnType("decimal(8,2)");
            builder.Property(E => E.WasteGenerated).HasColumnType("decimal(8,2)");
            builder.Property(E => E.CarbonEmission).HasColumnType("decimal(12,4)");

        }
    }
}
