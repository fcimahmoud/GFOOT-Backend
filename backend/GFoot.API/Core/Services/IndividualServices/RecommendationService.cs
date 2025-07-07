
namespace Services.IndividualServices
{
    public class RecommendationService(
        IUnitOfWork unitOfWork,
        HttpClient httpClient
        )
        : IRecommendationService
    {
        public async Task CreateRecommendationAsync(string userId, ActivityDTO request, float emission)
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
                { "energy_efficiency", request.EnergyEfficiency },
                { "footprint",  emission.ToString()}
            });

            // Send a POST Request to the specified URL Containing the value serialized as JSON in the Request Body.
            var response = await httpClient.GetAsync($"https://gfoot-eveyfcgrepa3bcb3.francecentral-01.azurewebsites.net/individual/tips?{await queryParams.ReadAsStringAsync()}");

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to fetch recommendations: {response.StatusCode} - {errorResponse}");
            }

            // Read the JSON response to Deserialize it to store in the database.
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var recommendationsText = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonResponse)
                                        ["Personalized Recommendation"];

            var individualUser = await unitOfWork.GetRepository<IndividualUser, string>()
                    .GetByConditionAsync(user => user.ApplicationUserId == userId);

            var recRepo = unitOfWork.GetRepository<IndividualRecommendation, string>();

            // Delete Old Recommendations
            var oldRecommendations = await recRepo.GetAllByConditionAsync(r => r.UserId == individualUser.Id);
            if(oldRecommendations.Any())
            {
                foreach (var rec in oldRecommendations)
                {
                    recRepo.Delete(rec);
                    await unitOfWork.SaveChangesAsync();
                }
            }

            // Add New Recommendations
            var newRecommendations = ParseRecommendations(individualUser!.Id, recommendationsText);
            foreach (var rec in newRecommendations)
            {
                await recRepo.AddAsync(rec);
                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<RecommendationDTO>> GetAllRecommendationsAsync(string appUserId)
        {
            var individualUser = await unitOfWork.GetRepository<IndividualUser, string>()
                      .GetByConditionAsync(user => user.ApplicationUserId == appUserId);


            var recommendations = await unitOfWork.GetRepository<IndividualRecommendation, string>().GetAllByConditionAsync(r => r.UserId == individualUser.Id);

            var recommendationsDto = recommendations.Select(r => new RecommendationDTO
            {
                Id = r.Id,
                RecHeader = r.RecHeader,
                RecBody = r.RecBody,
            });
            return recommendationsDto;
        }
        public async Task<RecommendationDTO> GetRecommendationAsync(string recId)
        {
            var recommendation = await unitOfWork.GetRepository<IndividualRecommendation, string>().GetAsync(recId);
            return new RecommendationDTO
            {
                Id = recommendation.Id,
                RecHeader = recommendation.RecHeader,
                RecBody = recommendation.RecBody,
            };
        }

        private List<IndividualRecommendation> ParseRecommendations(string userId, string recommendationsText)
        {
            var tips = new List<IndividualRecommendation>();
            var lines = recommendationsText.Split("\n");

            foreach (var line in lines)
            {
                string modifiedLine = string.Empty;
                if(line.Length >= 3) modifiedLine = line.Remove(0 , 3);
                if (modifiedLine.StartsWith("**")) // Title detection
                {
                    var parts = modifiedLine.Split(":", 2);
                    if (parts.Length == 2)
                    {
                        var title = parts[0].Replace("**", "").Trim();
                        var body = parts[1].Trim();
                        tips.Add(new IndividualRecommendation { UserId = userId, RecHeader = title, RecBody = body });
                    }
                }
            }
            return tips;
        }
    }
}
