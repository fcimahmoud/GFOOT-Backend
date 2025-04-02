global using Microsoft.Extensions.Logging;
global using Shared.IndividualModels;
global using System.Text.Json;

namespace Services.IndividualServices
{
    public class CalculationsService (
        IUnitOfWork unitOfWork,
        HttpClient httpClient,
        ILogger<CalculationsService> logger
        ) : ICalculationsService
    {
        public async Task<float> CalculateCarbonFootPrintAsync(ActivityDTO request)
        {
            try
            {
                var queryParams = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "body_type", request.BodyType },
                    { "sex", request.Sex },
                    { "diet", request.Diet },
                    { "how_often_shower", request.HowOftenShower },
                    { "heating_energy_source", request.HeatingEnergySource },
                    { "transport", request.Transport },
                    { "vehicle_type", request.VehicleType },
                    { "social_activity", request.SocialActivity },
                    { "monthly_grocery_bill", request.MonthlyGroceryBill.ToString() },
                    { "frequency_of_traveling_by_air", request.FrequencyOfTravelingByAir },
                    { "vehicle_monthly_distance_km", request.VehicleMonthlyDistanceKm.ToString() },
                    { "waste_bag_size", request.WasteBagSize },
                    { "waste_bag_weekly_count", request.WasteBagWeeklyCount.ToString() },
                    { "how_long_tv_pc_daily_hour", request.HowLongTvPcDailyHour.ToString() },
                    { "how_long_internet_daily_hour", request.HowLongInternetDailyHour.ToString() },
                    { "how_many_new_clothes_monthly", request.HowManyNewClothesMonthly.ToString() },
                    { "energy_efficiency", request.EnergyEfficiency }
                });

                var response = await httpClient.GetAsync($"https://footprint-estimate.up.railway.app/calculate?{await queryParams.ReadAsStringAsync()}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to calculate carbon footprint: {response.StatusCode} - {errorResponse}");
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine("API Response: " + jsonResponse);

                var result = JsonSerializer.Deserialize<CalculationResponseDto>(jsonResponse,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });


                return result?.CarbonEmission ?? 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while calling carbon footprint API: {ex.Message}");
            }
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
                ShowerFreq = activityDTO.HowOftenShower,
                HeatingSource = activityDTO.HeatingEnergySource,
                AirTravelFreq = activityDTO.FrequencyOfTravelingByAir,
                VehicleType = activityDTO.VehicleType,
                SocialActivity = activityDTO.SocialActivity,
                Transport = activityDTO.Transport,
                WasteBagSize = activityDTO.WasteBagSize,
                DailyTvTime = activityDTO.HowLongTvPcDailyHour,
                MonthlyClothingPurchases = activityDTO.HowManyNewClothesMonthly,
                DailyInternetUsage = activityDTO.HowLongInternetDailyHour,
                WasteBagWeeklyCount = activityDTO.WasteBagWeeklyCount,
                VehicleDistanceKm = activityDTO.VehicleMonthlyDistanceKm,
                EnergyEfficiency = activityDTO.EnergyEfficiency,
                GroceryBill = activityDTO.MonthlyGroceryBill,
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
