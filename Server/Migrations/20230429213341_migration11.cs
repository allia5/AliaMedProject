using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class migration11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analyses_SpecialisteAnalyse_SpecialisteAnalyseId",
                table: "Analyses");

            migrationBuilder.DropIndex(
                name: "IX_Analyses_SpecialisteAnalyseId",
                table: "Analyses");

            migrationBuilder.DropColumn(
                name: "SpecialisteAnalyseId",
                table: "Analyses");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("03d2395f-a472-4a41-b95f-45828d5f8af4"),
                column: "ConcurrencyStamp",
                value: "56fa5951-8120-472c-81e4-5f1d7df6ef36");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0916f1e5-ff87-4d4f-89b2-d6dbb922027e"),
                column: "ConcurrencyStamp",
                value: "ccbd0986-c1e2-452f-a937-37ba402681a0");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0d518584-64a4-424b-b011-7283083394b8"),
                column: "ConcurrencyStamp",
                value: "13734020-8169-4367-b2bc-a83d859d89f7");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("14e8987f-77b0-44a9-a641-6c6779b9564c"),
                column: "ConcurrencyStamp",
                value: "75b883ee-35f5-4346-abfd-b6cc157a773b");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("232d07c5-711e-4802-a048-f2f73804ea40"),
                column: "ConcurrencyStamp",
                value: "ed8f57ef-d42c-4783-968d-5e054975880a");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("2b102f8f-079c-4ae1-b093-487ba70cf183"),
                column: "ConcurrencyStamp",
                value: "d2f2f35b-cd39-4131-8ade-5ff4bd1c4d34");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("cf35304b-0241-4b81-8f57-d0dccdccb836"),
                column: "ConcurrencyStamp",
                value: "bd212bf8-848d-4b0d-a7e5-0d491b47016a");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SpecialisteAnalyseId",
                table: "Analyses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("03d2395f-a472-4a41-b95f-45828d5f8af4"),
                column: "ConcurrencyStamp",
                value: "98622b6b-5276-435a-8660-00078f72745b");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0916f1e5-ff87-4d4f-89b2-d6dbb922027e"),
                column: "ConcurrencyStamp",
                value: "bd9bced3-6c16-4442-975d-ef4d8928cef2");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0d518584-64a4-424b-b011-7283083394b8"),
                column: "ConcurrencyStamp",
                value: "109f36fb-04ca-4e8d-97af-d73593111b8b");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("14e8987f-77b0-44a9-a641-6c6779b9564c"),
                column: "ConcurrencyStamp",
                value: "43fc11f8-72ba-421f-8ad3-3e2eaac76809");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("232d07c5-711e-4802-a048-f2f73804ea40"),
                column: "ConcurrencyStamp",
                value: "67a1576f-548c-4feb-8f6d-77592472e833");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("2b102f8f-079c-4ae1-b093-487ba70cf183"),
                column: "ConcurrencyStamp",
                value: "8265a4ab-12cc-4f90-9889-943b77de8903");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("cf35304b-0241-4b81-8f57-d0dccdccb836"),
                column: "ConcurrencyStamp",
                value: "528368c9-ae11-4838-9880-b54fe240c9f8");

            migrationBuilder.CreateIndex(
                name: "IX_Analyses_SpecialisteAnalyseId",
                table: "Analyses",
                column: "SpecialisteAnalyseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Analyses_SpecialisteAnalyse_SpecialisteAnalyseId",
                table: "Analyses",
                column: "SpecialisteAnalyseId",
                principalTable: "SpecialisteAnalyse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
