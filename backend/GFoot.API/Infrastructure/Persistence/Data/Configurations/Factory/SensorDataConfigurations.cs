
namespace Persistence.Data.Configurations.Factory
{
    internal class SensorDataConfigurations
    : BaseEntityConfigurations<SensorData, string>
    {
        public override void Configure(EntityTypeBuilder<SensorData> builder)
        {
            base.Configure(builder);
            builder.Property(S => S.SensorType).IsRequired();
            builder.Property(S => S.Status).IsRequired();
            builder.Property(S => S.DataValue).HasColumnType("decimal(8,2)");
        }
    }
}
