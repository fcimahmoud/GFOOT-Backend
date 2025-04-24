using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateFactoryEmissionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeeCommutingMethod",
                table: "FactoryEmissions",
                newName: "EmployeeCommutingWalkOrCycle");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCommutingCar",
                table: "FactoryEmissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCommutingCarpool",
                table: "FactoryEmissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCommutingPublic",
                table: "FactoryEmissions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeCommutingCar",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "EmployeeCommutingCarpool",
                table: "FactoryEmissions");

            migrationBuilder.DropColumn(
                name: "EmployeeCommutingPublic",
                table: "FactoryEmissions");

            migrationBuilder.RenameColumn(
                name: "EmployeeCommutingWalkOrCycle",
                table: "FactoryEmissions",
                newName: "EmployeeCommutingMethod");
        }
    }
}
