using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class adressmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Incidents",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "IncidentDetails",
                newName: "CategoryId");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "IncidentDetails",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_CategoryId",
                table: "Incidents",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_IncidentDetails_CategoryId",
                table: "IncidentDetails",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncidentDetails_Categories_CategoryId",
                table: "IncidentDetails",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_Categories_CategoryId",
                table: "Incidents",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncidentDetails_Categories_CategoryId",
                table: "IncidentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_Categories_CategoryId",
                table: "Incidents");

            migrationBuilder.DropIndex(
                name: "IX_Incidents_CategoryId",
                table: "Incidents");

            migrationBuilder.DropIndex(
                name: "IX_IncidentDetails_CategoryId",
                table: "IncidentDetails");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "IncidentDetails");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Incidents",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "IncidentDetails",
                newName: "Category");
        }
    }
}
