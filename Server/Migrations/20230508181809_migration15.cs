using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class migration15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SpecialisteAnalyse_NameMedicalAnalyse",
                table: "SpecialisteAnalyse");

            migrationBuilder.DropIndex(
                name: "IX_Pharmacists_PharmacistName",
                table: "Pharmacists");

            migrationBuilder.DropColumn(
                name: "Adress",
                table: "SpecialisteAnalyse");

            migrationBuilder.DropColumn(
                name: "NameMedicalAnalyse",
                table: "SpecialisteAnalyse");

            migrationBuilder.DropColumn(
                name: "AdressPharmacist",
                table: "Pharmacists");

            migrationBuilder.DropColumn(
                name: "PharmacistName",
                table: "Pharmacists");

            migrationBuilder.AddColumn<Guid>(
                name: "MedicalAnalyseClinicId",
                table: "SpecialisteAnalyse",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PharmacyId",
                table: "Pharmacists",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "MedicalAnalysisClinic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicalAnalysisName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdressMedicalAnalysis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Services = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailMedicalAnalysis = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalAnalysisClinic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PharmacistName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdressPharmacist = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Services = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailPharmacy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacy", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("03d2395f-a472-4a41-b95f-45828d5f8af4"),
                column: "ConcurrencyStamp",
                value: "79cc8cd8-7099-41da-b1e8-89f3ec69c43c");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0916f1e5-ff87-4d4f-89b2-d6dbb922027e"),
                column: "ConcurrencyStamp",
                value: "a9f71c37-2b5d-472b-9574-996f481bbeeb");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0d518584-64a4-424b-b011-7283083394b8"),
                column: "ConcurrencyStamp",
                value: "2be2228a-a8f2-4ea8-918e-26ad9c836cf0");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("14e8987f-77b0-44a9-a641-6c6779b9564c"),
                column: "ConcurrencyStamp",
                value: "76550c54-63a8-44e4-8209-7f8f4d5e6449");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("232d07c5-711e-4802-a048-f2f73804ea40"),
                column: "ConcurrencyStamp",
                value: "c5271819-5f3d-4c18-a323-27f3cedd6410");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("2b102f8f-079c-4ae1-b093-487ba70cf183"),
                column: "ConcurrencyStamp",
                value: "d37bf4cb-f5cc-40d7-bfa7-8592c126f7e8");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("cf35304b-0241-4b81-8f57-d0dccdccb836"),
                column: "ConcurrencyStamp",
                value: "bd439bb4-aa2d-47b4-a1af-e0179fcf10ce");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialisteAnalyse_MedicalAnalyseClinicId",
                table: "SpecialisteAnalyse",
                column: "MedicalAnalyseClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacists_PharmacyId",
                table: "Pharmacists",
                column: "PharmacyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pharmacists_Pharmacy_PharmacyId",
                table: "Pharmacists",
                column: "PharmacyId",
                principalTable: "Pharmacy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialisteAnalyse_MedicalAnalysisClinic_MedicalAnalyseClinicId",
                table: "SpecialisteAnalyse",
                column: "MedicalAnalyseClinicId",
                principalTable: "MedicalAnalysisClinic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pharmacists_Pharmacy_PharmacyId",
                table: "Pharmacists");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialisteAnalyse_MedicalAnalysisClinic_MedicalAnalyseClinicId",
                table: "SpecialisteAnalyse");

            migrationBuilder.DropTable(
                name: "MedicalAnalysisClinic");

            migrationBuilder.DropTable(
                name: "Pharmacy");

            migrationBuilder.DropIndex(
                name: "IX_SpecialisteAnalyse_MedicalAnalyseClinicId",
                table: "SpecialisteAnalyse");

            migrationBuilder.DropIndex(
                name: "IX_Pharmacists_PharmacyId",
                table: "Pharmacists");

            migrationBuilder.DropColumn(
                name: "MedicalAnalyseClinicId",
                table: "SpecialisteAnalyse");

            migrationBuilder.DropColumn(
                name: "PharmacyId",
                table: "Pharmacists");

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "SpecialisteAnalyse",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameMedicalAnalyse",
                table: "SpecialisteAnalyse",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AdressPharmacist",
                table: "Pharmacists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PharmacistName",
                table: "Pharmacists",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("03d2395f-a472-4a41-b95f-45828d5f8af4"),
                column: "ConcurrencyStamp",
                value: "b2659eff-f2fc-47cc-bf0a-786bfddeb3ed");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0916f1e5-ff87-4d4f-89b2-d6dbb922027e"),
                column: "ConcurrencyStamp",
                value: "65081d2f-f0ce-429a-9744-e8644a53a7d3");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0d518584-64a4-424b-b011-7283083394b8"),
                column: "ConcurrencyStamp",
                value: "5e00c78d-ceed-4fd8-98db-2167c1bf416c");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("14e8987f-77b0-44a9-a641-6c6779b9564c"),
                column: "ConcurrencyStamp",
                value: "20ab1a08-cb59-40fa-a77b-18b3d161e43e");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("232d07c5-711e-4802-a048-f2f73804ea40"),
                column: "ConcurrencyStamp",
                value: "5172b527-d448-4ed6-95fb-11b6e6771ea8");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("2b102f8f-079c-4ae1-b093-487ba70cf183"),
                column: "ConcurrencyStamp",
                value: "363654da-847e-4e97-9d2f-482d58d0a697");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("cf35304b-0241-4b81-8f57-d0dccdccb836"),
                column: "ConcurrencyStamp",
                value: "5a2b6166-2daa-4f74-8cda-e53a1a2adad6");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialisteAnalyse_NameMedicalAnalyse",
                table: "SpecialisteAnalyse",
                column: "NameMedicalAnalyse");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacists_PharmacistName",
                table: "Pharmacists",
                column: "PharmacistName");
        }
    }
}
