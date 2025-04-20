
using Microsoft.EntityFrameworkCore;

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
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(F => F.SensorData)
                .WithOne(S => S.Factory)
                .HasForeignKey(S => S.FactoryUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(E => E.FactoryEmissions)
                .WithOne(F => F.Factory)
                .HasForeignKey(F => F.FactoryUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(N => N.FactoryRecomendations)
                .WithOne(F => F.FactoryUser)
                .HasForeignKey(F => F.FactoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(i => i.ApplicationUser)
                .WithOne(a => a.FactoryUser)
                .HasForeignKey<FactoryUser>(I => I.ApplicationUserId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
