
namespace Domain.Entities.Individual
{
    public class IndividualRecommendation : BaseEntity<string>
    {
        public required string RecHeader { get; set; }
        public required string RecBody { get; set; }

        // Navigational Property
        public string? UserId { get; set; }
        public virtual IndividualUser? User { get; set; }
    }
}
