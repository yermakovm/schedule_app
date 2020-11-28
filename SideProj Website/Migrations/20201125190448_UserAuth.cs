using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScheduleApp.Migrations
{
    public partial class UserAuth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShiftTypeId",
                table: "Shifts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsTrainee",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ShiftType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DurationHours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_ShiftTypeId",
                table: "Shifts",
                column: "ShiftTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_ShiftType_ShiftTypeId",
                table: "Shifts",
                column: "ShiftTypeId",
                principalTable: "ShiftType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_ShiftType_ShiftTypeId",
                table: "Shifts");

            migrationBuilder.DropTable(
                name: "ShiftType");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_ShiftTypeId",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "ShiftTypeId",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "IsTrainee",
                table: "Employees");
        }
    }
}
