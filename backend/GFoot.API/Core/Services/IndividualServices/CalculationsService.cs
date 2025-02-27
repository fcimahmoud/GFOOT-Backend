using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace Services.IndividualServices
{
    public class CalculationsService (
        IUnitOfWork unitOfWork,
        HttpClient httpClient,
        ILogger<CalculationsService> logger
        ) : ICalculationsService
    {
        public async Task<float> CalculateCarbonFootPrintAsync(ActivityDTO activityDTO)
        {

            /*            var payload = new 
                        {
                            activityDTO.Sex,
                            activityDTO.Diet,
                            activityDTO.BodyType,
                            activityDTO.ShowerFreq,
                            activityDTO.HeatingSource,
                            activityDTO.AirTravelFreq,
                            activityDTO.VehicleType,
                            activityDTO.CookingMethods,
                            activityDTO.SocialActivity,
                            activityDTO.Transport,
                            activityDTO.WasteBagSize,
                            activityDTO.RecyclingOptions,
                            activityDTO.DailyTvTime,
                            activityDTO.MonthlyClothingPurchases,
                            activityDTO.DailyInternetUsage,
                            activityDTO.WasteBagWeeklyCount,
                            activityDTO.VehicleDistanceKm,
                            activityDTO.EnergyEfficiency,
                            activityDTO.GroceryBill,
                        };

                        // Send a POST Request to the specified URL Containing the value serialized as JSON in the Request Body.
                        var response = await httpClient.PostAsJsonAsync("http://localhost:5000/predict", payload);

                        if (!response.IsSuccessStatusCode)
                            throw new Exception("AI Model failed");

                        // Read the HTTP Content and returns the value that results from deserializing the content.
                        var result = await response.Content.ReadFromJsonAsync<float>();
                        return result;*/

            // Placeholder: Replace with actual AI Model logic
            await Task.Delay(100); // Simulate processing time
            return new Random().Next(100, 500); // Mock Carbon Footprint in kg CO2
        }

        public async Task<Activity> LogActivityAsync(string userId, ActivityDTO activityDTO)
        {
            var individualUser = await GetIndividualByAppUserIdAsync(userId);
            if (individualUser == null) throw new Exception("User not found.");

            var activity = new Activity
            {
                Sex = activityDTO.Sex,
                Diet = activityDTO.Diet,
                BodyType = activityDTO.BodyType,
                ShowerFreq = activityDTO.ShowerFreq,
                HeatingSource = activityDTO.HeatingSource,
                AirTravelFreq = activityDTO.AirTravelFreq,
                VehicleType = activityDTO.VehicleType,
                CookingMethods = activityDTO.CookingMethods,
                SocialActivity = activityDTO.SocialActivity,
                Transport = activityDTO.Transport,
                WasteBagSize = activityDTO.WasteBagSize,
                RecyclingOptions = activityDTO.RecyclingOptions,
                DailyTvTime = activityDTO.DailyTvTime,
                MonthlyClothingPurchases = activityDTO.MonthlyClothingPurchases,
                DailyInternetUsage = activityDTO.DailyInternetUsage,
                WasteBagWeeklyCount = activityDTO.WasteBagWeeklyCount,
                VehicleDistanceKm = activityDTO.VehicleDistanceKm,
                EnergyEfficiency = activityDTO.EnergyEfficiency,
                GroceryBill = activityDTO.GroceryBill,
            };

            activity.Id = Guid.NewGuid().ToString();
            activity.UserId = individualUser.Id;
            activity.Date = DateOnly.FromDateTime(DateTime.UtcNow);
            activity.CarbonEmission = await CalculateCarbonFootPrintAsync(activityDTO);

            await unitOfWork.GetRepository<Activity, string>().AddAsync(activity);
            await unitOfWork.SaveChangesAsync();

            logger.LogInformation($"Activity logged for user {userId}, Carbon Footprint: {activity.CarbonEmission}");

            return activity;
        }

        public async Task<IndividualUser?> GetIndividualByAppUserIdAsync(string applicationUserId)
        {
            return await unitOfWork.GetRepository<IndividualUser, string>().GetByConditionAsync(user => user.ApplicationUserId == applicationUserId);
        }
    }
}
