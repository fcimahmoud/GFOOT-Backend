
namespace Domain.Entities.Factory
{
    public class FactoryEmission : BaseEntity<string>
    {
        //28 question => 31 variable + Date
        public DateTime Date { get; set; }
        public int? NumberOfEmployees { get; set; }
        public float? FacilitySize { get; set; } //=> OrganizationSize

        // Energy Consumption
        public string? ElectricityConsumptionType { get; set; } //
        public float? ElectricityConsumptionAmount { get; set; }
        public string? RenewableElectricitySource { get; set; } //=> RenewableElectricitySource
        public string? FuelConsumptionType { get; set; } //
        public string? FuelConsumptionAmount { get; set; } //=> string

        // Owned Transportation
        public string? OwnedTransportation { get; set; }    // Yes or NO
        public string? TruckFuelType { get; set; }
        public float? TruckFuelAmount { get; set; }
        public float? AverageTruckDistance { get; set; }
        public float? AverageTruckWeight { get; set; }
        public string? TruckType { get; set; }

        // Third-Party Shipping
        public string? ThirdPartyShipping { get; set; }   // Yes or NO
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
        public string? EmployeeCommutingWalkOrCycle { get; set; }
        public string? EmployeeCommutingPublic { get; set; }
        public string? EmployeeCommutingCar { get; set; }
        public string? EmployeeCommutingCarpool { get; set; }

        public float? AverageEmployeeCommutingDistance { get; set; }
        public string? BusinessTravelFrequency { get; set; }
        public string? BusinessTravelType { get; set; }
        public float CarbonEmission { get; set; }

        // Navigational Property
        public string? FactoryUserId { get; set; }
        public virtual FactoryUser? Factory { get; set; }
    }
}
