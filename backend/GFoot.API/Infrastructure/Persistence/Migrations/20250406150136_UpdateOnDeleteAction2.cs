using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOnDeleteAction2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnvironmentalAgents_AspNetUsers_ApplicationUserId",
                table: "EnvironmentalAgents");

            migrationBuilder.DropForeignKey(
                name: "FK_FactoryUsers_AspNetUsers_ApplicationUserId",
                table: "FactoryUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_IndividualUsers_AspNetUsers_ApplicationUserId",
                table: "IndividualUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_EnvironmentalAgents_AspNetUsers_ApplicationUserId",
                table: "EnvironmentalAgents",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_FactoryUsers_AspNetUsers_ApplicationUserId",
                table: "FactoryUsers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualUsers_AspNetUsers_ApplicationUserId",
                table: "IndividualUsers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnvironmentalAgents_AspNetUsers_ApplicationUserId",
                table: "EnvironmentalAgents");

            migrationBuilder.DropForeignKey(
                name: "FK_FactoryUsers_AspNetUsers_ApplicationUserId",
                table: "FactoryUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_IndividualUsers_AspNetUsers_ApplicationUserId",
                table: "IndividualUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_EnvironmentalAgents_AspNetUsers_ApplicationUserId",
                table: "EnvironmentalAgents",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FactoryUsers_AspNetUsers_ApplicationUserId",
                table: "FactoryUsers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualUsers_AspNetUsers_ApplicationUserId",
                table: "IndividualUsers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
