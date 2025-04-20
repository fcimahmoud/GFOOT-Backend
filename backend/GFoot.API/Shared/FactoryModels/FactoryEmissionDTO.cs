
namespace Shared.FactoryModels
{
    public class FactoryEmissionDTO
    {

        public int NumberOfEmployees { get; set; }
        public float FacilitySize { get; set; }

        // Energy Consumption
        public required string ElectricityConsumptionType { get; set; }
        public float ElectricityConsumptionAmount { get; set; }
        public required string RenewableElectricity { get; set; }
        public required string FuelConsumptionType { get; set; }
        public float FuelConsumptionAmount { get; set; }

        // Owned Transportation
        public required string OwnedTransportation { get; set; }    // Yes or NO  : if yes will proceed to the truck data
        public string? TruckFuelType { get; set; }
        public float? TruckFuelAmount { get; set; }
        public float? AverageTruckDistance { get; set; }
        public float? AverageTruckWeight { get; set; }
        public string? TruckType { get; set; }

        // Third-Party Shipping
        public required string ThirdPartyShipping { get; set; }   // Yes or NO  : if yes will proceed to the Third-Party Shipping data
        public string? ShippingMode { get; set; }
        public float? AverageShippingDistance { get; set; }
        public float? AverageContainerWeight { get; set; }

        // Waste Management
        public float WasteGenerated { get; set; }
        public required string WasteType { get; set; }
        public required string WasteDisposalMethod { get; set; }

        // Water Usage
        public float WaterConsumption { get; set; }
        public required string EnergyIntensiveProcesses { get; set; }   // Yes or NO  ( heating or cooling )

        // Industrial Processes
        public required string IndustrialProcesses { get; set; }        // Yes or NO  ( Any industrial processes that emit greenhouse gases )  
        public required string IndustrialProcessesDescription { get; set; }

        // Employee Travel & Commuting
        public required string EmployeeCommutingMethod { get; set; }
        public float AverageEmployeeCommutingDistance { get; set; }
        public required string BusinessTravelFrequency { get; set; }
        public required string BusinessTravelType { get; set; }

        public DateOnly Date { get; set; }

    }
}
