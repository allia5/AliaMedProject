using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class migration7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferenceFile",
                table: "resultAnalyses");

            migrationBuilder.DropColumn(
                name: "ReferenceFile",
                table: "RadioResult");

            migrationBuilder.AddColumn<byte[]>(
                name: "AnalyseResult",
                table: "resultAnalyses",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "FileResult",
                table: "RadioResult",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("03d2395f-a472-4a41-b95f-45828d5f8af4"),
                column: "ConcurrencyStamp",
                value: "eedb5587-9e07-4abf-a803-0821d0178ee4");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0916f1e5-ff87-4d4f-89b2-d6dbb922027e"),
                column: "ConcurrencyStamp",
                value: "677bdd5d-7975-4a83-a1b1-fc4287bd4676");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0d518584-64a4-424b-b011-7283083394b8"),
                column: "ConcurrencyStamp",
                value: "0527474f-d3ab-48d4-92bd-2bffc09cd4e0");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("14e8987f-77b0-44a9-a641-6c6779b9564c"),
                column: "ConcurrencyStamp",
                value: "02e22899-20bb-4061-9bde-117ee5d7d83d");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("232d07c5-711e-4802-a048-f2f73804ea40"),
                column: "ConcurrencyStamp",
                value: "edb85d49-f712-4f2e-90c7-61e14c07ac86");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("2b102f8f-079c-4ae1-b093-487ba70cf183"),
                column: "ConcurrencyStamp",
                value: "7ec55533-cbaa-4091-9092-b1fd05f9f19e");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("cf35304b-0241-4b81-8f57-d0dccdccb836"),
                column: "ConcurrencyStamp",
                value: "beae2073-a725-45a0-b201-02da2d02c2ae");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnalyseResult",
                table: "resultAnalyses");

            migrationBuilder.DropColumn(
                name: "FileResult",
                table: "RadioResult");

            migrationBuilder.AddColumn<string>(
                name: "ReferenceFile",
                table: "resultAnalyses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReferenceFile",
                table: "RadioResult",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("03d2395f-a472-4a41-b95f-45828d5f8af4"),
                column: "ConcurrencyStamp",
                value: "ff9e6775-75ad-4b1e-a2dd-c1b0d0efb0d6");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0916f1e5-ff87-4d4f-89b2-d6dbb922027e"),
                column: "ConcurrencyStamp",
                value: "6e3c83ad-19e2-41cc-aae7-461498203719");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0d518584-64a4-424b-b011-7283083394b8"),
                column: "ConcurrencyStamp",
                value: "fd8c69d9-5f62-49a9-a247-f9c9d456e7d5");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("14e8987f-77b0-44a9-a641-6c6779b9564c"),
                column: "ConcurrencyStamp",
                value: "f898735b-066b-4c21-b4bb-672a156b2ad9");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("232d07c5-711e-4802-a048-f2f73804ea40"),
                column: "ConcurrencyStamp",
                value: "4cd17eba-fe5b-450a-bb9b-bcc59ea271f1");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("2b102f8f-079c-4ae1-b093-487ba70cf183"),
                column: "ConcurrencyStamp",
                value: "ac890648-3d8d-42f2-9d89-0e6afaf9d5b5");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("cf35304b-0241-4b81-8f57-d0dccdccb836"),
                column: "ConcurrencyStamp",
                value: "ffee5ae5-af0e-4627-bf6d-c95d2224a52f");
        }
    }
}
