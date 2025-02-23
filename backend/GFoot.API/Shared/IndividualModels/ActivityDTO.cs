
namespace Shared.Individual
{
    public class ActivityDTO
    {
        public required string Sex { get; init; }
        public required string Diet { get; init; }
        public required string BodyType { get; init; }
        public required string ShowerFreq { get; init; }
        public required string HeatingSource { get; init; }
        public required string AirTravelFreq { get; init; }
        public required string VehicleType { get; init; }
        public required string CookingMethods { get; init; }
        public required string SocialActivity { get; init; }
        public required string Transport { get; init; }
        public required string WasteBagSize { get; init; }
        public required string RecyclingOptions { get; init; }
        public int DailyTvTime { get; init; }
        public int MonthlyClothingPurchases { get; init; }
        public int DailyInternetUsage { get; init; }
        public int WasteBagWeeklyCount { get; init; }
        public float VehicleDistanceKm { get; init; }
        public float EnergyEfficiency { get; init; }
        public float GroceryBill { get; init; }
    }
}
