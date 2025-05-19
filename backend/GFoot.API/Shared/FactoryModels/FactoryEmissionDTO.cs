
namespace Shared.FactoryModels
{
    public class FactoryEmissionDTO
    {
        public int NumberOfEmployees { get; set; }
        public float OrganizationSize { get; set; }

        // Energy Consumption
        public string ElectricityConsumptionType { get; set; }
        public float ElectricityConsumptionAmount { get; set; }
        public string RenewableElectricitySource { get; set; }
        public string FuelConsumptionType { get; set; }
        public string FuelConsumptionAmount { get; set; }

        // Owned Transportation
        public string OwnedTransportation { get; set; }    // Yes or NO  : if yes will proceed to the truck data
        public string? TruckFuelType { get; set; }
        public float? TruckFuelAmount { get; set; }
        public float? AverageTruckDistance { get; set; }
        public float? AverageTruckWeight { get; set; }
        public string? TruckType { get; set; }

        // Third-Party Shipping
        public string ThirdPartyShipping { get; set; }   // Yes or NO  : if yes will proceed to the Third-Party Shipping data
        public string? ShippingMode { get; set; }
        public float? AverageShippingDistance { get; set; }
        public float? AverageContainerWeight { get; set; }

        // Waste Management
        public float WasteGenerated { get; set; }
        public string WasteType { get; set; }
        public string WasteDisposalMethod { get; set; }

        // Water Usage
        public float WaterConsumption { get; set; }
        public string EnergyIntensiveProcesses { get; set; }   // Yes or NO  ( heating or cooling )

        // Industrial Processes
        public string IndustrialProcesses { get; set; }        // Yes or NO  ( Any industrial processes that emit greenhouse gases )  
        public string IndustrialProcessesDescription { get; set; }

        // Employee Travel & Commuting
        public string EmployeeCommutingWalkOrBicycle { get; set; }
        public string EmployeeCommutingPublic { get; set; }
        public string EmployeeCommutingCar { get; set; }
        public string EmployeeCommutingCarpool { get; set; }

        public float AverageEmployeeCommutingDistance { get; set; }
        public string BusinessTravelFrequency { get; set; }
        public string BusinessTravelType { get; set; }

    }
}
