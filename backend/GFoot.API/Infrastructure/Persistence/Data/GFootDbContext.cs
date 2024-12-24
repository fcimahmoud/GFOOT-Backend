
namespace Persistence.Data
{
    public class GFootDbContext
        : IdentityDbContext<ApplicationUser>
    {
        public GFootDbContext(DbContextOptions<GFootDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(AssemblyRefrence).Assembly);
        }

        public DbSet<IndividualUser> IndividualUsers { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<IndividualRecommendation> IndividualRecommendations { get; set; }
        public DbSet<FactoryRecommendation> FactoryRecommendations { get; set; }
        public DbSet<FactoryUser> FactoryUsers { get; set; }
        public DbSet<FactoryEmission> FactoryEmissions { get; set; }
        public DbSet<SensorData> SensorData { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<EnvironmentalAgent> EnvironmentalAgents { get; set; }
    }
}
