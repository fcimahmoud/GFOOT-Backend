
namespace Domain.Entities.Factory
{
    public class FactoryRecommendation : BaseEntity<string>
    {
        public required string RecHeader { get; set; }
        public required string RecBody { get; set; }
        public DateOnly Date { get; set; }


        // Navigational Property
        public string? FactoryId { get; set; }
        public virtual FactoryUser? FactoryUser { get; set; }
    }
}
