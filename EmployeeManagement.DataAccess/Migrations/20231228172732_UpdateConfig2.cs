using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConfig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Communes_Districts_DistrictId",
                table: "Communes");

            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Provinces_ProvinceId",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Communes_CommuneId",
                table: "Employees");

            migrationBuilder.AlterColumn<DateTime>(
                name: "GrantingDate",
                table: "AwardDiplomas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 29, 0, 27, 30, 789, DateTimeKind.Local).AddTicks(3500),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 29, 0, 12, 50, 410, DateTimeKind.Local).AddTicks(2753));

            migrationBuilder.AddForeignKey(
                name: "FK_Communes_Districts_DistrictId",
                table: "Communes",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Provinces_ProvinceId",
                table: "Districts",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Communes_CommuneId",
                table: "Employees",
                column: "CommuneId",
                principalTable: "Communes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Communes_Districts_DistrictId",
                table: "Communes");

            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Provinces_ProvinceId",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Communes_CommuneId",
                table: "Employees");

            migrationBuilder.AlterColumn<DateTime>(
                name: "GrantingDate",
                table: "AwardDiplomas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 29, 0, 12, 50, 410, DateTimeKind.Local).AddTicks(2753),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 29, 0, 27, 30, 789, DateTimeKind.Local).AddTicks(3500));

            migrationBuilder.AddForeignKey(
                name: "FK_Communes_Districts_DistrictId",
                table: "Communes",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Provinces_ProvinceId",
                table: "Districts",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Communes_CommuneId",
                table: "Employees",
                column: "CommuneId",
                principalTable: "Communes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
