using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateFactoryEntities2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IndustryDescription",
                table: "FactoryUsers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "FactoryUsers");

            migrationBuilder.AlterColumn<decimal>(
                name: "CarbonEmission",
                table: "FactoryEmissions",
                type: "decimal(12,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,4)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<decimal>(
                name: "CarbonEmission",
                table: "FactoryEmissions",
                type: "decimal(12,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,4)");
        }
    }
}
