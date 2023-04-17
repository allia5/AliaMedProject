using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class migration10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analyses_Pharmacists_IdPharmacist",
                table: "Analyses");

            migrationBuilder.DropForeignKey(
                name: "FK_Analyses_medicalAnalyses_MedicalAnalyseId",
                table: "Analyses");

            migrationBuilder.DropIndex(
                name: "IX_Analyses_IdPharmacist",
                table: "Analyses");

            migrationBuilder.DropColumn(
                name: "IdPharmacist",
                table: "Analyses");

            migrationBuilder.RenameColumn(
                name: "MedicalAnalyseId",
                table: "Analyses",
                newName: "IdMedicalAnalyse");

            migrationBuilder.RenameIndex(
                name: "IX_Analyses_MedicalAnalyseId",
                table: "Analyses",
                newName: "IX_Analyses_IdMedicalAnalyse");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdRadiology",
                table: "Radio",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Instruction",
                table: "Analyses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("03d2395f-a472-4a41-b95f-45828d5f8af4"),
                column: "ConcurrencyStamp",
                value: "e6a06bca-6750-45a0-8942-37178852092e");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0916f1e5-ff87-4d4f-89b2-d6dbb922027e"),
                column: "ConcurrencyStamp",
                value: "0e32073a-513a-4f54-8cee-9cba2ae0d8b8");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0d518584-64a4-424b-b011-7283083394b8"),
                column: "ConcurrencyStamp",
                value: "7ae74917-33d5-428f-9a5e-9fdb53da5e68");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("14e8987f-77b0-44a9-a641-6c6779b9564c"),
                column: "ConcurrencyStamp",
                value: "29c71144-ec09-4e93-b069-94e4038d9f1b");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("232d07c5-711e-4802-a048-f2f73804ea40"),
                column: "ConcurrencyStamp",
                value: "eb9de68c-d529-4edc-a1fc-9f69aa1d9260");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("2b102f8f-079c-4ae1-b093-487ba70cf183"),
                column: "ConcurrencyStamp",
                value: "860d5307-77d1-47e5-9ad4-8546c89578f7");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("cf35304b-0241-4b81-8f57-d0dccdccb836"),
                column: "ConcurrencyStamp",
                value: "1688c3f9-66d1-4a01-b71c-ab3a0cd76051");

            migrationBuilder.AddForeignKey(
                name: "FK_Analyses_medicalAnalyses_IdMedicalAnalyse",
                table: "Analyses",
                column: "IdMedicalAnalyse",
                principalTable: "medicalAnalyses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analyses_medicalAnalyses_IdMedicalAnalyse",
                table: "Analyses");

            migrationBuilder.RenameColumn(
                name: "IdMedicalAnalyse",
                table: "Analyses",
                newName: "MedicalAnalyseId");

            migrationBuilder.RenameIndex(
                name: "IX_Analyses_IdMedicalAnalyse",
                table: "Analyses",
                newName: "IX_Analyses_MedicalAnalyseId");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdRadiology",
                table: "Radio",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Instruction",
                table: "Analyses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IdPharmacist",
                table: "Analyses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("03d2395f-a472-4a41-b95f-45828d5f8af4"),
                column: "ConcurrencyStamp",
                value: "666f3118-5aa9-436e-80e0-116f2d4daba5");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0916f1e5-ff87-4d4f-89b2-d6dbb922027e"),
                column: "ConcurrencyStamp",
                value: "48ad8719-358d-4838-9db8-9914f46d6c35");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0d518584-64a4-424b-b011-7283083394b8"),
                column: "ConcurrencyStamp",
                value: "9d35fab1-b81b-4093-9698-eaef5c0651fb");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("14e8987f-77b0-44a9-a641-6c6779b9564c"),
                column: "ConcurrencyStamp",
                value: "881951aa-8767-45e0-aa5f-005835c50cf3");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("232d07c5-711e-4802-a048-f2f73804ea40"),
                column: "ConcurrencyStamp",
                value: "ac488795-def8-4620-91aa-6ef45a6ba8b6");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("2b102f8f-079c-4ae1-b093-487ba70cf183"),
                column: "ConcurrencyStamp",
                value: "d341cafc-71da-4c9e-ab2f-622f451844ac");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("cf35304b-0241-4b81-8f57-d0dccdccb836"),
                column: "ConcurrencyStamp",
                value: "d695f45e-537b-4307-a145-fec3a977ce76");

            migrationBuilder.CreateIndex(
                name: "IX_Analyses_IdPharmacist",
                table: "Analyses",
                column: "IdPharmacist");

            migrationBuilder.AddForeignKey(
                name: "FK_Analyses_Pharmacists_IdPharmacist",
                table: "Analyses",
                column: "IdPharmacist",
                principalTable: "Pharmacists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Analyses_medicalAnalyses_MedicalAnalyseId",
                table: "Analyses",
                column: "MedicalAnalyseId",
                principalTable: "medicalAnalyses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
