using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateFactoryEntities3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IndividualUsers_AspNetUsers_ApplicationUserId",
                table: "IndividualUsers");

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
                name: "FK_IndividualUsers_AspNetUsers_ApplicationUserId",
                table: "IndividualUsers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "IndividualUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IndividualUsers_ApplicationUserId1",
                table: "IndividualUsers",
                column: "ApplicationUserId1",
                unique: true,
                filter: "[ApplicationUserId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualUsers_AspNetUsers_ApplicationUserId",
                table: "IndividualUsers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualUsers_AspNetUsers_ApplicationUserId1",
                table: "IndividualUsers",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
