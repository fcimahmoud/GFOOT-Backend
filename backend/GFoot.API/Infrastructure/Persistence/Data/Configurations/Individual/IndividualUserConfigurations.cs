
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data.Configurations.Individual
{
    internal class IndividualUserConfigurations
    : BaseEntityConfigurations<IndividualUser, string>
    {
        public override void Configure(EntityTypeBuilder<IndividualUser> builder)
        {
            base.Configure(builder);


            // Relationships
            builder.HasMany(N => N.Notifications)
                    .WithOne(I => I.User)
                    .HasForeignKey(I => I.UserId)
                    .OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(N => N.Activities)
                    .WithOne(I => I.User)
                    .HasForeignKey(I => I.UserId)
                    .OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(N => N.IndividualRecommendations)
                    .WithOne(I => I.User)
                    .HasForeignKey(I => I.UserId)
            .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(i => i.ApplicationUser)
                .WithOne(a => a.IndividualUser)
                .HasForeignKey<IndividualUser>(I => I.ApplicationUserId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
