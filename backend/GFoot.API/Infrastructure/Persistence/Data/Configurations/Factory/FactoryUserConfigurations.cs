
namespace Persistence.Data.Configurations.Factory
{
    internal class FactoryUserConfigurations 
        : BaseEntityConfigurations<FactoryUser, string>
    {
        public override void Configure(EntityTypeBuilder<FactoryUser> builder)
        {
            base.Configure(builder);

            //builder.OwnsOne(L => L.Address, location => location.WithOwner());

            // Relationships
            builder.HasMany(R => R.Reports)
                .WithOne(F => F.Factory)
                .HasForeignKey(R => R.FactoryUserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(F => F.SensorData)
                .WithOne(S => S.Factory)
                .HasForeignKey(S => S.FactoryUserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(E => E.FactoryEmissions)
                .WithOne(F => F.Factory)
                .HasForeignKey(F => F.FactoryUserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(N => N.FactoryRecomendations)
                .WithOne(F => F.FactoryUser)
                .HasForeignKey(F => F.FactoryId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
