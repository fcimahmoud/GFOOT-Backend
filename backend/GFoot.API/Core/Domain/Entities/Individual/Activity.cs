
namespace Domain.Entities.Individual
{
    public class Activity : BaseEntity<string>
    {
        public required string Sex { get; set; }
        public required string Diet { get; set; }
        public required string BodyType { get; set; }
        public required string ShowerFreq { get; set; }
        public required string HeatingSource { get; set; }
        public required string AirTravelFreq { get; set; }
        public required string VehicleType { get; set; }
        public required string CookingMethods { get; set; }
        public required string SocialActivity { get; set; }
        public required string Transport { get; set; }
        public required string WasteBagSize { get; set; }
        public required string RecyclingOptions { get; set; }
        public int DailyTvTime { get; set; }
        public int MonthlyClothingPurchases { get; set; }
        public int DailyInternetUsage { get; set; }
        public int WasteBagWeeklyCount { get; set; }
        public float VehicleDistanceKm { get; set; }
        public float EnergyEfficiency { get; set; }
        public float GroceryBill { get; set; }
        public DateOnly Date { get; set; }
        public float CarbonEmission { get; set; }

        // Navigational Property
        public string? UserId { get; set; }
        public virtual IndividualUser? User { get; set; }
    }
}
