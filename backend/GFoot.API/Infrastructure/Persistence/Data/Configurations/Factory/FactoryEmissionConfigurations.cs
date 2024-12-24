
namespace Persistence.Data.Configurations.Factory
{
    internal class FactoryEmissionConfigurations 
        : BaseEntityConfigurations<FactoryEmission, string>
    {
        public override void Configure(EntityTypeBuilder<FactoryEmission> builder)
        {
            base.Configure(builder);

            builder.Property(E => E.RawMaterials).IsRequired();
            builder.Property(E => E.PurchasedElectricity).HasColumnType("decimal(8,2)");
            builder.Property(E => E.FuelCombustion).HasColumnType("decimal(8,2)");
            builder.Property(E => E.TransportationEnergyCombustion).HasColumnType("decimal(8,2)");
            builder.Property(E => E.WasteEmission).HasColumnType("decimal(8,2)");
            builder.Property(E => E.CarbonEmission).HasColumnType("decimal(12,4)");

        }
    }
}
