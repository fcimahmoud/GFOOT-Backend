using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateFactoryEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FuelCombustion",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "PurchasedElectricity",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "RawMaterials",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "TransportationEnergyCombustion",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "WasteEmission",
                table: "FactoryEmissions");

            migrationBuilder.AddColumn<string>(
                name: "IndustryDescription",
                table: "FactoryUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Phone",
                table: "FactoryUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "FactoryRecommendations",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "FactoryEmissions",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<decimal>(
                name: "CarbonEmission",
                table: "FactoryEmissions",
                type: "decimal(12,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,4)");

            migrationBuilder.AddColumn<float>(
                name: "AverageContainerWeight",
                table: "FactoryEmissions",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AverageEmployeeCommutingDistance",
                table: "FactoryEmissions",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AverageShippingDistance",
                table: "FactoryEmissions",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AverageTruckDistance",
                table: "FactoryEmissions",
                type: "decimal(8,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AverageTruckWeight",
                table: "FactoryEmissions",
                type: "decimal(8,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessTravelFrequency",
                table: "FactoryEmissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessTravelType",
                table: "FactoryEmissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ElectricityConsumptionAmount",
                table: "FactoryEmissions",
                type: "decimal(8,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ElectricityConsumptionType",
                table: "FactoryEmissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCommutingMethod",
                table: "FactoryEmissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnergyIntensiveProcesses",
                table: "FactoryEmissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "FacilitySize",
                table: "FactoryEmissions",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FuelConsumptionAmount",
                table: "FactoryEmissions",
                type: "decimal(8,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FuelConsumptionType",
                table: "FactoryEmissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndustrialProcesses",
                table: "FactoryEmissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndustrialProcessesDescription",
                table: "FactoryEmissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfEmployees",
                table: "FactoryEmissions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnedTransportation",
                table: "FactoryEmissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RenewableElectricity",
                table: "FactoryEmissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingMode",
                table: "FactoryEmissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThirdPartyShipping",
                table: "FactoryEmissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TruckFuelAmount",
                table: "FactoryEmissions",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TruckFuelType",
                table: "FactoryEmissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TruckType",
                table: "FactoryEmissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WasteDisposalMethod",
                table: "FactoryEmissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WasteGenerated",
                table: "FactoryEmissions",
                type: "decimal(8,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WasteType",
                table: "FactoryEmissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "WaterConsumption",
                table: "FactoryEmissions",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IndustryDescription",
                table: "FactoryUsers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "FactoryUsers");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "FactoryRecommendations");

            migrationBuilder.DropColumn(
                name: "AverageContainerWeight",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "AverageEmployeeCommutingDistance",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "AverageShippingDistance",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "AverageTruckDistance",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "AverageTruckWeight",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "BusinessTravelFrequency",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "BusinessTravelType",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "ElectricityConsumptionAmount",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "ElectricityConsumptionType",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "EmployeeCommutingMethod",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "EnergyIntensiveProcesses",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "FacilitySize",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "FuelConsumptionAmount",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "FuelConsumptionType",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "IndustrialProcesses",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "IndustrialProcessesDescription",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "NumberOfEmployees",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "OwnedTransportation",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "RenewableElectricity",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "ShippingMode",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "ThirdPartyShipping",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "TruckFuelAmount",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "TruckFuelType",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "TruckType",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "WasteDisposalMethod",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "WasteGenerated",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "WasteType",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "WaterConsumption",
                table: "FactoryEmissions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "FactoryEmissions",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<decimal>(
                name: "CarbonEmission",
                table: "FactoryEmissions",
                type: "decimal(12,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,4)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FuelCombustion",
                table: "FactoryEmissions",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PurchasedElectricity",
                table: "FactoryEmissions",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "RawMaterials",
                table: "FactoryEmissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TransportationEnergyCombustion",
                table: "FactoryEmissions",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WasteEmission",
                table: "FactoryEmissions",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
