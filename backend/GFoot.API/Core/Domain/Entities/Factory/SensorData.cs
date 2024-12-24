
namespace Domain.Entities.Factory
{
    public class SensorData : BaseEntity<string>
    {
        public required string SensorType { get; set; }
        public required string Status { get; set; }
        public float DataValue { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

        // Navigational Property
        public string? FactoryUserId { get; set; }
        public virtual FactoryUser? Factory { get; set; }
    }
}
