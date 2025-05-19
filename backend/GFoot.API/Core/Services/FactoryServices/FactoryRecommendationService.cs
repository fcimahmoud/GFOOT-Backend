
using Shared.FactoryModels;

namespace Services.FactoryServices
{
    public class FactoryRecommendationService(IUnitOfWork unitOfWork, HttpClient httpClient) : IFactoryRecommendationService
    {
        public async Task CreateRecommendationAsync(string userId, FactoryEmissionDTO factoryEmissionDTO, float emission)
        {
            var queryParams = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "number_of_employees", factoryEmissionDTO.NumberOfEmployees.ToString() },
                    { "facility_size", factoryEmissionDTO.OrganizationSize.ToString() },
                    { "electricity_consumption_type", factoryEmissionDTO.ElectricityConsumptionType },
                    { "electricity_consumption_amount", factoryEmissionDTO.ElectricityConsumptionAmount.ToString() },
                    { "renewable_electricity_source", factoryEmissionDTO.RenewableElectricitySource },
                    { "fuel_consumption_type", factoryEmissionDTO.FuelConsumptionType },
                    { "fuel_consumption_amount", factoryEmissionDTO.FuelConsumptionAmount },
                    { "owned_transportation", factoryEmissionDTO.OwnedTransportation },
                    { "truck_fuel_type", factoryEmissionDTO.TruckFuelType },
                    { "truck_fuel_amount", factoryEmissionDTO.TruckFuelAmount.ToString() },
                    { "average_truck_distance", factoryEmissionDTO.AverageTruckDistance.ToString() },
                    { "average_truck_weight", factoryEmissionDTO.AverageTruckWeight.ToString() },
                    { "truck_type", factoryEmissionDTO.TruckType },
                    { "third_party_shipping", factoryEmissionDTO.ThirdPartyShipping },
                    { "shipping_mode", factoryEmissionDTO.ShippingMode },
                    { "average_shipping_distance", factoryEmissionDTO.AverageShippingDistance.ToString() },
                    { "average_container_weight", factoryEmissionDTO.AverageContainerWeight.ToString() },
                    { "waste_generated", factoryEmissionDTO.WasteGenerated.ToString() },
                    { "waste_type", factoryEmissionDTO.WasteType },
                    { "waste_disposal_method", factoryEmissionDTO.WasteDisposalMethod },
                    { "water_consumption", factoryEmissionDTO.WaterConsumption.ToString() },
                    { "Energy_intensive_processes", factoryEmissionDTO.EnergyIntensiveProcesses },
                    { "industrial_processes", factoryEmissionDTO.IndustrialProcesses },
                    { "industrial_processes_description", factoryEmissionDTO.IndustrialProcessesDescription },

                    { "employee_commuting_walkOrBicycle", factoryEmissionDTO.EmployeeCommutingWalkOrBicycle },
                    { "employee_commuting_public", factoryEmissionDTO.EmployeeCommutingPublic },
                    { "employee_commuting_car", factoryEmissionDTO.EmployeeCommutingCar },
                    { "employee_commuting_carpool", factoryEmissionDTO.EmployeeCommutingCarpool },

                    { "average_employee_commuting_distance", factoryEmissionDTO.AverageEmployeeCommutingDistance.ToString() },
                    { "business_travel_frequency", factoryEmissionDTO.BusinessTravelFrequency },
                    { "business_travel_type", factoryEmissionDTO.BusinessTravelType },
                    { "carbon_footprint",  emission.ToString()}
                });


            // Send a POST Request to the specified URL Containing the value serialized as JSON in the Request Body.
            var response = await httpClient.GetAsync($"https://carbon-footprint-estimate.up.railway.app/tips-org?{await queryParams.ReadAsStringAsync()}");

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to fetch factory recommendations: {response.StatusCode} - {errorResponse}");
            }

            // Read the JSON response to Deserialize it to store in the database.
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var parsedResponse = JsonSerializer.Deserialize<OrganizationRecommendationsResponseML>(jsonResponse);

            if (parsedResponse == null || parsedResponse.OrganizationRecommendations == null)
                throw new Exception("Invalid response format from recommendations API.");

            var recommendationsText = parsedResponse.OrganizationRecommendations;

            var factoryUser = await unitOfWork.GetRepository<FactoryUser, string>()
                    .GetByConditionAsync(user => user.ApplicationUserId == userId);
            
            var recRepo = unitOfWork.GetRepository<FactoryRecommendation, string>();

            // Delete Old Recommendations
            var oldRecommendations = await recRepo.GetAllByConditionAsync(r => r.FactoryId == userId);
            if (oldRecommendations.Any())
            {
                foreach (var rec in oldRecommendations)
                {
                    recRepo.Delete(rec);
                    await unitOfWork.SaveChangesAsync();
                }
            }

            // Add New Recommendations
            var recommendations = ParseRecommendations(factoryUser!.Id, recommendationsText);
            foreach (var rec in recommendations)
            {
                await recRepo.AddAsync(rec);
                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<FactoryRecommendationDTO>> GetAllRecommendationsAsync(string appUserId)
        {
            var factoryUser = await unitOfWork.GetRepository<FactoryUser, string>()
                       .GetByConditionAsync(user => user.ApplicationUserId == appUserId);


            var recommendations = await unitOfWork.GetRepository<FactoryRecommendation, string>().GetAllByConditionAsync(r => r.FactoryId == factoryUser.Id);

            var recommendationsDto = recommendations.Select(r => new FactoryRecommendationDTO
            {
                Id = r.Id,
                RecHeader = r.RecHeader,
                RecBody = r.RecBody,
                Date = r.Date
            });
            return recommendationsDto;
        }

        public async Task<FactoryRecommendationDTO> GetRecommendationAsync(string recId)
        {
            var recommendation = await unitOfWork.GetRepository<FactoryRecommendation, string>().GetAsync(recId);
            var recommendationDto = new FactoryRecommendationDTO
            {
                Id = recommendation.Id,
                RecHeader = recommendation.RecHeader,
                RecBody = recommendation.RecBody,
                Date = recommendation.Date
            };
            return recommendationDto;
        }

        private List<FactoryRecommendation> ParseRecommendations(string userId, Dictionary<string, string> recommendationsDict)
        {
            var tips = new List<FactoryRecommendation>();

            foreach (var entry in recommendationsDict)
            {
                var title = entry.Key?.Trim();
                var body = entry.Value?.Trim();

                if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(body))
                {
                    tips.Add(new FactoryRecommendation
                    {
                        FactoryId = userId,
                        RecHeader = title,
                        RecBody = body
                    });
                }
            }

            return tips;
        }
    }
}
