using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class migration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_resultAnalyses_LineAnalyseMedicals_LineAnalyseMedicalsId",
                table: "resultAnalyses");

            migrationBuilder.DropIndex(
                name: "IX_resultAnalyses_LineAnalyseMedicalsId",
                table: "resultAnalyses");

            migrationBuilder.DropColumn(
                name: "LineAnalyseMedicalsId",
                table: "resultAnalyses");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("03d2395f-a472-4a41-b95f-45828d5f8af4"),
                column: "ConcurrencyStamp",
                value: "b8893ce6-de81-438a-9361-c265440515eb");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0916f1e5-ff87-4d4f-89b2-d6dbb922027e"),
                column: "ConcurrencyStamp",
                value: "d75836bb-8fb1-4b46-8052-faf124da73f1");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0d518584-64a4-424b-b011-7283083394b8"),
                column: "ConcurrencyStamp",
                value: "758871bc-9177-4cb3-9c0a-f0ac6f14406f");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("14e8987f-77b0-44a9-a641-6c6779b9564c"),
                column: "ConcurrencyStamp",
                value: "c0b9dd27-587d-4199-a533-70256d29bd35");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("232d07c5-711e-4802-a048-f2f73804ea40"),
                column: "ConcurrencyStamp",
                value: "cb84329d-9272-42f7-823e-51697307fe2d");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("2b102f8f-079c-4ae1-b093-487ba70cf183"),
                column: "ConcurrencyStamp",
                value: "584a74d6-3d54-4adf-8df6-6d653094c6f2");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("cf35304b-0241-4b81-8f57-d0dccdccb836"),
                column: "ConcurrencyStamp",
                value: "bba7194d-4d76-4159-9190-25a37cd26e72");

            migrationBuilder.CreateIndex(
                name: "IX_resultAnalyses_IdLineAnalyse",
                table: "resultAnalyses",
                column: "IdLineAnalyse");

            migrationBuilder.AddForeignKey(
                name: "FK_resultAnalyses_LineAnalyseMedicals_IdLineAnalyse",
                table: "resultAnalyses",
                column: "IdLineAnalyse",
                principalTable: "LineAnalyseMedicals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_resultAnalyses_LineAnalyseMedicals_IdLineAnalyse",
                table: "resultAnalyses");

            migrationBuilder.DropIndex(
                name: "IX_resultAnalyses_IdLineAnalyse",
                table: "resultAnalyses");

            migrationBuilder.AddColumn<Guid>(
                name: "LineAnalyseMedicalsId",
                table: "resultAnalyses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("03d2395f-a472-4a41-b95f-45828d5f8af4"),
                column: "ConcurrencyStamp",
                value: "177b8f89-b213-4b40-96f2-aa8f2df7b5d7");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0916f1e5-ff87-4d4f-89b2-d6dbb922027e"),
                column: "ConcurrencyStamp",
                value: "ecd4222b-620d-4d48-bcc1-d7535675d0bb");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0d518584-64a4-424b-b011-7283083394b8"),
                column: "ConcurrencyStamp",
                value: "d1a288cd-7ef6-40b9-b5c3-d0c0705a2b11");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("14e8987f-77b0-44a9-a641-6c6779b9564c"),
                column: "ConcurrencyStamp",
                value: "cd2ad34d-1762-4757-908d-971c8409ea37");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("232d07c5-711e-4802-a048-f2f73804ea40"),
                column: "ConcurrencyStamp",
                value: "3a587c17-99f5-4747-b3ed-eb504a5464d2");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("2b102f8f-079c-4ae1-b093-487ba70cf183"),
                column: "ConcurrencyStamp",
                value: "554e56d1-01c5-4817-9ca4-c84a1b16a217");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("cf35304b-0241-4b81-8f57-d0dccdccb836"),
                column: "ConcurrencyStamp",
                value: "1117356e-10af-4a1e-bd43-55ab56465ed4");

            migrationBuilder.CreateIndex(
                name: "IX_resultAnalyses_LineAnalyseMedicalsId",
                table: "resultAnalyses",
                column: "LineAnalyseMedicalsId");

            migrationBuilder.AddForeignKey(
                name: "FK_resultAnalyses_LineAnalyseMedicals_LineAnalyseMedicalsId",
                table: "resultAnalyses",
                column: "LineAnalyseMedicalsId",
                principalTable: "LineAnalyseMedicals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
