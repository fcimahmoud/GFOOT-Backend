
namespace Persistence.Data.Configurations.Identity
{
    internal class ApplicationUserConfigurations
    : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(U => U.DisplayName)
                  .HasColumnType("varchar")
                  .HasMaxLength(100)
                  .IsRequired();

            builder.HasOne(A => A.IndividualUser)
                   .WithOne(I => I.ApplicationUser)
                   .HasForeignKey<IndividualUser>(I => I.ApplicationUserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(A => A.FactoryUser)
                   .WithOne(F => F.ApplicationUser)
                   .HasForeignKey<FactoryUser>(F => F.ApplicationUserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(A => A.EnvironmentalAgent)
                   .WithOne(E => E.ApplicationUser)
                   .HasForeignKey<EnvironmentalAgent>(E => E.ApplicationUserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
