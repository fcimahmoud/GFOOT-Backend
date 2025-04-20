
namespace Domain.Entities.Factory
{
    public class FactoryEmission : BaseEntity<string>
    {
        public int? NumberOfEmployees { get; set; }
        public float? FacilitySize { get; set; }

        // Energy Consumption
        public string? ElectricityConsumptionType { get; set; } //
        public float? ElectricityConsumptionAmount { get; set; }
        public string? RenewableElectricity { get; set; } //
        public string? FuelConsumptionType { get; set; } //
        public float? FuelConsumptionAmount { get; set; }

        // Owned Transportation
        public string? OwnedTransportation { get; set; }    // Yes or NO  : if yes will proceed to the truck data
        public string? TruckFuelType { get; set; }
        public float? TruckFuelAmount { get; set; }
        public float? AverageTruckDistance { get; set; }
        public float? AverageTruckWeight { get; set; }
        public string? TruckType { get; set; }

        // Third-Party Shipping
        public string? ThirdPartyShipping { get; set; }   // Yes or NO  : if yes will proceed to the Third-Party Shipping data
        public string? ShippingMode { get; set; }
        public float? AverageShippingDistance { get; set; }
        public float? AverageContainerWeight { get; set; }

        // Waste Management
        public float? WasteGenerated { get; set; }
        public string? WasteType { get; set; } //
        public string? WasteDisposalMethod { get; set; } //

        // Water Usage
        public float? WaterConsumption { get; set; }
        public string? EnergyIntensiveProcesses { get; set; }   // Yes or NO  ( heating or cooling )

        // Industrial Processes
        public string? IndustrialProcesses { get; set; }        // Yes or NO  ( Any industrial processes that emit greenhouse gases )  
        public string? IndustrialProcessesDescription { get; set; }

        // Employee Travel & Commuting
        public string? EmployeeCommutingMethod { get; set; }
        public float? AverageEmployeeCommutingDistance { get; set; }
        public string? BusinessTravelFrequency { get; set; }
        public string? BusinessTravelType { get; set; }
        public DateOnly Date { get; set; }
        public float CarbonEmission { get; set; }

        // Navigational Property
        public string? FactoryUserId { get; set; }
        public virtual FactoryUser? Factory { get; set; }
    }
}
