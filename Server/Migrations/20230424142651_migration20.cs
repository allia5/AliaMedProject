using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class migration20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analyses_medicalAnalyses_IdMedicalAnalyse",
                table: "Analyses");

            migrationBuilder.DropForeignKey(
                name: "FK_Radio_radiologies_IdRadiology",
                table: "Radio");

            migrationBuilder.DropForeignKey(
                name: "FK_RadioResult_Radio_IdRadio",
                table: "RadioResult");

            migrationBuilder.DropForeignKey(
                name: "FK_resultAnalyses_Analyses_IdAnalyse",
                table: "resultAnalyses");

            migrationBuilder.DropColumn(
                name: "DateValidation",
                table: "Radio");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Radio");

            migrationBuilder.DropColumn(
                name: "Instruction",
                table: "Radio");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Radio");

            migrationBuilder.DropColumn(
                name: "DateValidation",
                table: "Analyses");

            migrationBuilder.DropColumn(
                name: "Instruction",
                table: "Analyses");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Analyses");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Analyses");

            migrationBuilder.RenameColumn(
                name: "IdAnalyse",
                table: "resultAnalyses",
                newName: "LineAnalyseMedicalsId");

            migrationBuilder.RenameIndex(
                name: "IX_resultAnalyses_IdAnalyse",
                table: "resultAnalyses",
                newName: "IX_resultAnalyses_LineAnalyseMedicalsId");

            migrationBuilder.RenameColumn(
                name: "FileType",
                table: "RadioResult",
                newName: "fileType");

            migrationBuilder.RenameColumn(
                name: "IdRadio",
                table: "RadioResult",
                newName: "IdLineRadio");

            migrationBuilder.RenameIndex(
                name: "IX_RadioResult_IdRadio",
                table: "RadioResult",
                newName: "IX_RadioResult_IdLineRadio");

            migrationBuilder.RenameColumn(
                name: "IdRadiology",
                table: "Radio",
                newName: "RadiologyId");

            migrationBuilder.RenameIndex(
                name: "IX_Radio_IdRadiology",
                table: "Radio",
                newName: "IX_Radio_RadiologyId");

            migrationBuilder.RenameColumn(
                name: "IdMedicalAnalyse",
                table: "Analyses",
                newName: "MedicalAnalyseId");

            migrationBuilder.RenameIndex(
                name: "IX_Analyses_IdMedicalAnalyse",
                table: "Analyses",
                newName: "IX_Analyses_MedicalAnalyseId");

            migrationBuilder.AddColumn<Guid>(
                name: "IdLineAnalyse",
                table: "resultAnalyses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "fileType",
                table: "RadioResult",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "LineAnalyseMedicals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instruction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateValidation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdMedicalAnalyse = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AnalysesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineAnalyseMedicals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LineAnalyseMedicals_Analyses_AnalysesId",
                        column: x => x.AnalysesId,
                        principalTable: "Analyses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LineAnalyseMedicals_medicalAnalyses_IdMedicalAnalyse",
                        column: x => x.IdMedicalAnalyse,
                        principalTable: "medicalAnalyses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LineRadioMedicals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instruction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateValidation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdRadiology = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdRadio = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineRadioMedicals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LineRadioMedicals_Radio_IdRadio",
                        column: x => x.IdRadio,
                        principalTable: "Radio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LineRadioMedicals_radiologies_IdRadiology",
                        column: x => x.IdRadiology,
                        principalTable: "radiologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("03d2395f-a472-4a41-b95f-45828d5f8af4"),
                column: "ConcurrencyStamp",
                value: "f4a4e551-11f7-4951-8372-c7f2096a3cb8");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0916f1e5-ff87-4d4f-89b2-d6dbb922027e"),
                column: "ConcurrencyStamp",
                value: "36017bea-e177-4005-b1a4-5e2c7d0de37a");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0d518584-64a4-424b-b011-7283083394b8"),
                column: "ConcurrencyStamp",
                value: "8679bae6-37d1-4a0b-bac9-6eca5b418ddf");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("14e8987f-77b0-44a9-a641-6c6779b9564c"),
                column: "ConcurrencyStamp",
                value: "3610ae00-7918-41a9-b189-687eb54db909");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("232d07c5-711e-4802-a048-f2f73804ea40"),
                column: "ConcurrencyStamp",
                value: "a47a2f27-52cc-4e11-8529-72acbc305fa0");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("2b102f8f-079c-4ae1-b093-487ba70cf183"),
                column: "ConcurrencyStamp",
                value: "95109d34-4b43-4101-8ff6-00e4672b20b3");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("cf35304b-0241-4b81-8f57-d0dccdccb836"),
                column: "ConcurrencyStamp",
                value: "5690d720-6e33-4051-a919-515eb173fb87");

            migrationBuilder.CreateIndex(
                name: "IX_LineAnalyseMedicals_AnalysesId",
                table: "LineAnalyseMedicals",
                column: "AnalysesId");

            migrationBuilder.CreateIndex(
                name: "IX_LineAnalyseMedicals_IdMedicalAnalyse",
                table: "LineAnalyseMedicals",
                column: "IdMedicalAnalyse");

            migrationBuilder.CreateIndex(
                name: "IX_LineRadioMedicals_IdRadio",
                table: "LineRadioMedicals",
                column: "IdRadio");

            migrationBuilder.CreateIndex(
                name: "IX_LineRadioMedicals_IdRadiology",
                table: "LineRadioMedicals",
                column: "IdRadiology");

            migrationBuilder.AddForeignKey(
                name: "FK_Analyses_medicalAnalyses_MedicalAnalyseId",
                table: "Analyses",
                column: "MedicalAnalyseId",
                principalTable: "medicalAnalyses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Radio_radiologies_RadiologyId",
                table: "Radio",
                column: "RadiologyId",
                principalTable: "radiologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RadioResult_LineRadioMedicals_IdLineRadio",
                table: "RadioResult",
                column: "IdLineRadio",
                principalTable: "LineRadioMedicals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_resultAnalyses_LineAnalyseMedicals_LineAnalyseMedicalsId",
                table: "resultAnalyses",
                column: "LineAnalyseMedicalsId",
                principalTable: "LineAnalyseMedicals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analyses_medicalAnalyses_MedicalAnalyseId",
                table: "Analyses");

            migrationBuilder.DropForeignKey(
                name: "FK_Radio_radiologies_RadiologyId",
                table: "Radio");

            migrationBuilder.DropForeignKey(
                name: "FK_RadioResult_LineRadioMedicals_IdLineRadio",
                table: "RadioResult");

            migrationBuilder.DropForeignKey(
                name: "FK_resultAnalyses_LineAnalyseMedicals_LineAnalyseMedicalsId",
                table: "resultAnalyses");

            migrationBuilder.DropTable(
                name: "LineAnalyseMedicals");

            migrationBuilder.DropTable(
                name: "LineRadioMedicals");

            migrationBuilder.DropColumn(
                name: "IdLineAnalyse",
                table: "resultAnalyses");

            migrationBuilder.RenameColumn(
                name: "LineAnalyseMedicalsId",
                table: "resultAnalyses",
                newName: "IdAnalyse");

            migrationBuilder.RenameIndex(
                name: "IX_resultAnalyses_LineAnalyseMedicalsId",
                table: "resultAnalyses",
                newName: "IX_resultAnalyses_IdAnalyse");

            migrationBuilder.RenameColumn(
                name: "fileType",
                table: "RadioResult",
                newName: "FileType");

            migrationBuilder.RenameColumn(
                name: "IdLineRadio",
                table: "RadioResult",
                newName: "IdRadio");

            migrationBuilder.RenameIndex(
                name: "IX_RadioResult_IdLineRadio",
                table: "RadioResult",
                newName: "IX_RadioResult_IdRadio");

            migrationBuilder.RenameColumn(
                name: "RadiologyId",
                table: "Radio",
                newName: "IdRadiology");

            migrationBuilder.RenameIndex(
                name: "IX_Radio_RadiologyId",
                table: "Radio",
                newName: "IX_Radio_IdRadiology");

            migrationBuilder.RenameColumn(
                name: "MedicalAnalyseId",
                table: "Analyses",
                newName: "IdMedicalAnalyse");

            migrationBuilder.RenameIndex(
                name: "IX_Analyses_MedicalAnalyseId",
                table: "Analyses",
                newName: "IX_Analyses_IdMedicalAnalyse");

            migrationBuilder.AlterColumn<int>(
                name: "FileType",
                table: "RadioResult",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateValidation",
                table: "Radio",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Radio",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Instruction",
                table: "Radio",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Radio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateValidation",
                table: "Analyses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instruction",
                table: "Analyses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Analyses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Analyses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("03d2395f-a472-4a41-b95f-45828d5f8af4"),
                column: "ConcurrencyStamp",
                value: "2183b596-31a7-4960-a440-a82f846d853c");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0916f1e5-ff87-4d4f-89b2-d6dbb922027e"),
                column: "ConcurrencyStamp",
                value: "38905667-9a8a-4ce4-a04a-2be809ff4002");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0d518584-64a4-424b-b011-7283083394b8"),
                column: "ConcurrencyStamp",
                value: "dd36c2dc-aaad-4e64-9ee3-1e461363a9e1");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("14e8987f-77b0-44a9-a641-6c6779b9564c"),
                column: "ConcurrencyStamp",
                value: "2b8dec36-c2af-444e-ab0e-530c50f02e80");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("232d07c5-711e-4802-a048-f2f73804ea40"),
                column: "ConcurrencyStamp",
                value: "7bc01d62-43dd-4957-a8ab-892e5cac8a03");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("2b102f8f-079c-4ae1-b093-487ba70cf183"),
                column: "ConcurrencyStamp",
                value: "02e0071a-0853-4f1b-bc04-bca87c4d7b0f");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("cf35304b-0241-4b81-8f57-d0dccdccb836"),
                column: "ConcurrencyStamp",
                value: "2affa7e8-8cc3-46af-8ca5-faff1f6fd6e3");

            migrationBuilder.AddForeignKey(
                name: "FK_Analyses_medicalAnalyses_IdMedicalAnalyse",
                table: "Analyses",
                column: "IdMedicalAnalyse",
                principalTable: "medicalAnalyses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Radio_radiologies_IdRadiology",
                table: "Radio",
                column: "IdRadiology",
                principalTable: "radiologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RadioResult_Radio_IdRadio",
                table: "RadioResult",
                column: "IdRadio",
                principalTable: "Radio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_resultAnalyses_Analyses_IdAnalyse",
                table: "resultAnalyses",
                column: "IdAnalyse",
                principalTable: "Analyses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
