using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class smsentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SmsDetail_SmsMaster_SmsMasterId",
                table: "SmsDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_SmsMaster_Incidents_IncidentId",
                table: "SmsMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SmsMaster",
                table: "SmsMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SmsDetail",
                table: "SmsDetail");

            migrationBuilder.RenameTable(
                name: "SmsMaster",
                newName: "SmsMasters");

            migrationBuilder.RenameTable(
                name: "SmsDetail",
                newName: "SmsDetails");

            migrationBuilder.RenameIndex(
                name: "IX_SmsMaster_IncidentId",
                table: "SmsMasters",
                newName: "IX_SmsMasters_IncidentId");

            migrationBuilder.RenameIndex(
                name: "IX_SmsDetail_SmsMasterId",
                table: "SmsDetails",
                newName: "IX_SmsDetails_SmsMasterId");

            migrationBuilder.AlterColumn<string>(
                name: "MobilePhone",
                table: "SmsDetails",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SmsMasters",
                table: "SmsMasters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SmsDetails",
                table: "SmsDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SmsDetails_SmsMasters_SmsMasterId",
                table: "SmsDetails",
                column: "SmsMasterId",
                principalTable: "SmsMasters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SmsMasters_Incidents_IncidentId",
                table: "SmsMasters",
                column: "IncidentId",
                principalTable: "Incidents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SmsDetails_SmsMasters_SmsMasterId",
                table: "SmsDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SmsMasters_Incidents_IncidentId",
                table: "SmsMasters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SmsMasters",
                table: "SmsMasters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SmsDetails",
                table: "SmsDetails");

            migrationBuilder.RenameTable(
                name: "SmsMasters",
                newName: "SmsMaster");

            migrationBuilder.RenameTable(
                name: "SmsDetails",
                newName: "SmsDetail");

            migrationBuilder.RenameIndex(
                name: "IX_SmsMasters_IncidentId",
                table: "SmsMaster",
                newName: "IX_SmsMaster_IncidentId");

            migrationBuilder.RenameIndex(
                name: "IX_SmsDetails_SmsMasterId",
                table: "SmsDetail",
                newName: "IX_SmsDetail_SmsMasterId");

            migrationBuilder.AlterColumn<string>(
                name: "MobilePhone",
                table: "SmsDetail",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SmsMaster",
                table: "SmsMaster",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SmsDetail",
                table: "SmsDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SmsDetail_SmsMaster_SmsMasterId",
                table: "SmsDetail",
                column: "SmsMasterId",
                principalTable: "SmsMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SmsMaster_Incidents_IncidentId",
                table: "SmsMaster",
                column: "IncidentId",
                principalTable: "Incidents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
