
namespace Persistence.Data.Configurations.Individual
{
    internal class NotificationConfigurations
    : BaseEntityConfigurations<Notification, string>
    {
        public override void Configure(EntityTypeBuilder<Notification> builder)
        {
            base.Configure(builder);

            builder.Property(N => N.NotificationType).IsRequired();
            builder.Property(N => N.NotificationBody).IsRequired();
            builder.Property(N => N.DateSent).IsRequired().HasDefaultValueSql("GETUTCDate()");

        }
    }
}
