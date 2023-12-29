using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SetNullWhenDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Communes_CommuneId",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CommuneId",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "GrantingDate",
                table: "AwardDiplomas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 29, 17, 21, 54, 750, DateTimeKind.Local).AddTicks(4550),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 29, 0, 27, 30, 789, DateTimeKind.Local).AddTicks(3500));

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Communes_CommuneId",
                table: "Employees",
                column: "CommuneId",
                principalTable: "Communes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Communes_CommuneId",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<int>(
                name: "CommuneId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "GrantingDate",
                table: "AwardDiplomas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 29, 0, 27, 30, 789, DateTimeKind.Local).AddTicks(3500),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 29, 17, 21, 54, 750, DateTimeKind.Local).AddTicks(4550));

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Communes_CommuneId",
                table: "Employees",
                column: "CommuneId",
                principalTable: "Communes",
                principalColumn: "Id");
        }
    }
}
