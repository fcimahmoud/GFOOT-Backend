
namespace Services.FactoryServices
{
    public class FactoryCalculationsService
        (IUnitOfWork unitOfWork,
        HttpClient httpClient,
        ILogger<FactoryCalculationsService> logger,
        IMapper mapper) : IFactoryCalculationsService
    {
        public async Task<float> CalculateCarbonFootPrintAsync(FactoryEmissionDTO factoryEmissionDTO)
        {
            try
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
                });

                var response = await httpClient.GetAsync($"https://footprint-estimate.up.railway.app/icalc?{await queryParams.ReadAsStringAsync()}");

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

        public async Task<FactoryEmission> LogCarbonFootPrintAsync(string applicationUserId, FactoryEmissionDTO factoryEmissionDTO)
        {
            var factoryUser = await unitOfWork.GetRepository<FactoryUser, string>().GetByConditionAsync(user => user.ApplicationUserId == applicationUserId);
            if (factoryUser == null) throw new Exception("Factory User not found.");

            var factoryEmission = mapper.Map<FactoryEmissionDTO, FactoryEmission>(factoryEmissionDTO);


            /*  var factoryEmission = new FactoryEmission
            {
                NumberOfEmployees = factoryEmissionDTO.NumberOfEmployees,
                OrganizationSize = factoryEmissionDTO.OrganizationSize,
                ElectricityConsumptionType = factoryEmissionDTO.ElectricityConsumptionType,
                ElectricityConsumptionAmount = factoryEmissionDTO.ElectricityConsumptionAmount,
                RenewableElectricitySource = factoryEmissionDTO.RenewableElectricitySource,
                FuelConsumptionType = factoryEmissionDTO.FuelConsumptionType,
                FuelConsumptionAmount = factoryEmissionDTO.FuelConsumptionAmount,
                OwnedTransportation = factoryEmissionDTO.OwnedTransportation,
                TruckFuelType = factoryEmissionDTO.TruckFuelType,
                TruckFuelAmount = factoryEmissionDTO.TruckFuelAmount,
                AverageTruckDistance = factoryEmissionDTO.AverageTruckDistance,
                AverageTruckWeight = factoryEmissionDTO.AverageTruckWeight,
                TruckType = factoryEmissionDTO.TruckType,
                ThirdPartyShipping = factoryEmissionDTO.ThirdPartyShipping,
                ShippingMode = factoryEmissionDTO.ShippingMode,
                AverageShippingDistance = factoryEmissionDTO.AverageShippingDistance,
                AverageContainerWeight = factoryEmissionDTO.AverageContainerWeight,
                WasteGenerated = factoryEmissionDTO.WasteGenerated,
                WasteType = factoryEmissionDTO.WasteType,
                WasteDisposalMethod = factoryEmissionDTO.WasteDisposalMethod,
                WaterConsumption = factoryEmissionDTO.WaterConsumption,
                EnergyIntensiveProcesses = factoryEmissionDTO.EnergyIntensiveProcesses,
                IndustrialProcesses = factoryEmissionDTO.IndustrialProcesses,
                IndustrialProcessesDescription = factoryEmissionDTO.IndustrialProcessesDescription,
                EmployeeCommutingMethod = factoryEmissionDTO.EmployeeCommutingMethod,
                AverageEmployeeCommutingDistance = factoryEmissionDTO.AverageEmployeeCommutingDistance,
                BusinessTravelFrequency = factoryEmissionDTO.BusinessTravelFrequency,
                BusinessTravelType = factoryEmissionDTO.BusinessTravelType
            };*/

            factoryEmission.Id = Guid.NewGuid().ToString();
            factoryEmission.FactoryUserId = factoryUser.Id;
            factoryEmission.Date = DateTime.UtcNow;
            factoryEmission.CarbonEmission = 3000;// await CalculateCarbonFootPrintAsync(factoryEmissionDTO);


            await unitOfWork.GetRepository<FactoryEmission, string>().AddAsync(factoryEmission);
            await unitOfWork.SaveChangesAsync();

            logger.LogInformation($"Organization Emission logged for organization {applicationUserId}, Carbon Footprint: {factoryEmission.CarbonEmission}");

            return factoryEmission;
        }

        public async Task<float> GetFootPrintAsync(string applicationUserId)
        {
            var factoryUser = await unitOfWork.GetRepository<FactoryUser, string>().GetByConditionAsync(user => user.ApplicationUserId == applicationUserId);
            if (factoryUser == null) throw new Exception("User not found.");

            var factoryEmission = await unitOfWork.GetRepository<FactoryEmission, string>().GetAllByConditionSortedAsync(a => a.FactoryUserId == factoryUser.Id, a => a.Date, false);
            if (factoryEmission == null) throw new Exception("Activity Not Found");
            return factoryEmission.FirstOrDefault()!.CarbonEmission;
        }
    }
}
