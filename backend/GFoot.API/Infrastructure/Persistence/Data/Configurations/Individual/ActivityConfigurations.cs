
namespace Persistence.Data.Configurations.Individual
{
    internal class ActivityConfigurations
    : BaseEntityConfigurations<Activity, string>
    {
        public override void Configure(EntityTypeBuilder<Activity> builder)
        {
            base.Configure(builder);

            builder.Property(A => A.Sex).IsRequired();
            builder.Property(A => A.Diet).IsRequired();
            builder.Property(A => A.BodyType).IsRequired();
            builder.Property(A => A.ShowerFreq).IsRequired();
            builder.Property(A => A.HeatingSource).IsRequired();
            builder.Property(A => A.AirTravelFreq).IsRequired();
            builder.Property(A => A.VehicleType).IsRequired();
            builder.Property(A => A.CookingMethods).IsRequired();
            builder.Property(A => A.SocialActivity).IsRequired();
            builder.Property(A => A.Transport).IsRequired();
            builder.Property(A => A.WasteBagSize).IsRequired();
            builder.Property(A => A.RecyclingOptions).IsRequired();

            builder.Property(A => A.VehicleDistanceKm).HasColumnType("decimal(8,2)");
            builder.Property(A => A.EnergyEfficiency).HasColumnType("decimal(8,2)");
            builder.Property(A => A.GroceryBill).HasColumnType("decimal(8,2)");
            builder.Property(A => A.CarbonEmission).HasColumnType("decimal(12,4)");

        }
    }
}
