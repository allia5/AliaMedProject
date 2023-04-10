using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cabinetMedicals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameCabinet = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    image = table.Column<byte[]>(type: "varbinary(100)", maxLength: 100, nullable: true),
                    MapAdress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    numberPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    statusService = table.Column<int>(type: "int", nullable: false),
                    JobTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Services = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cabinetMedicals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "chronicDiseases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameChronicDiseases = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chronicDiseases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "specialites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameSpecialite = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specialites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sexe = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateCreateAccount = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateExpireRefreshToken = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberRRPS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsuranceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusDoctor = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "medicalAnalyses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameMedicalAnalyse = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AuthenticationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicalAnalyses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_medicalAnalyses_users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PharmacistName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AuthenticationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdressPharmacist = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    idUser = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pharmacists_users_idUser",
                        column: x => x.idUser,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Secretarys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCabinetMedical = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secretarys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Secretarys_cabinetMedicals_IdCabinetMedical",
                        column: x => x.IdCabinetMedical,
                        principalTable: "cabinetMedicals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Secretarys_users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "userRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userRoles_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_userRoles_users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCabinet = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdDoctor = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_admins_Doctors_IdDoctor",
                        column: x => x.IdDoctor,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_admins_cabinetMedicals_IdCabinet",
                        column: x => x.IdCabinet,
                        principalTable: "cabinetMedicals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "fileMedicals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicalIdentification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    firstname = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sexe = table.Column<int>(type: "int", nullable: false),
                    DateOfCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IdDoctor = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fileMedicals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fileMedicals_Doctors_IdDoctor",
                        column: x => x.IdDoctor,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_fileMedicals_users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "medicalPlannings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentCount = table.Column<int>(type: "int", nullable: false),
                    AppointmentAdress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdDoctor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCabinet = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicalPlannings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_medicalPlannings_Doctors_IdDoctor",
                        column: x => x.IdDoctor,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_medicalPlannings_cabinetMedicals_IdCabinet",
                        column: x => x.IdCabinet,
                        principalTable: "cabinetMedicals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_medicalPlannings_users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "radiologies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Services = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Materiel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdDoctor = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_radiologies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_radiologies_Doctors_IdDoctor",
                        column: x => x.IdDoctor,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "specialtiesDoctors",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDoctor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecialitesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specialtiesDoctors", x => x.id);
                    table.ForeignKey(
                        name: "FK_specialtiesDoctors_Doctors_IdDoctor",
                        column: x => x.IdDoctor,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_specialtiesDoctors_specialites_SpecialitesId",
                        column: x => x.SpecialitesId,
                        principalTable: "specialites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkDoctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReadyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeOfConsultation = table.Column<int>(type: "int", nullable: false),
                    statusServcie = table.Column<int>(type: "int", nullable: false),
                    statusReservation = table.Column<int>(type: "int", nullable: false),
                    StatusWork = table.Column<int>(type: "int", nullable: false),
                    DateInvitation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NbPatientAvailble = table.Column<int>(type: "int", nullable: false),
                    IdDoctor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCabinet = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkDoctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkDoctors_Doctors_IdDoctor",
                        column: x => x.IdDoctor,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkDoctors_cabinetMedicals_IdCabinet",
                        column: x => x.IdCabinet,
                        principalTable: "cabinetMedicals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FileChronicDiseases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdFile = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdChronicDisease = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileChronicDiseases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileChronicDiseases_chronicDiseases_IdChronicDisease",
                        column: x => x.IdChronicDisease,
                        principalTable: "chronicDiseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FileChronicDiseases_fileMedicals_IdFile",
                        column: x => x.IdFile,
                        principalTable: "fileMedicals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalOrdres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateValidation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdDoctor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdFileMedical = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSecritary = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCabinetMedical = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalOrdres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalOrdres_Doctors_IdDoctor",
                        column: x => x.IdDoctor,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalOrdres_Secretarys_IdSecritary",
                        column: x => x.IdSecritary,
                        principalTable: "Secretarys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalOrdres_cabinetMedicals_IdCabinetMedical",
                        column: x => x.IdCabinetMedical,
                        principalTable: "cabinetMedicals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalOrdres_fileMedicals_IdFileMedical",
                        column: x => x.IdFileMedical,
                        principalTable: "fileMedicals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Analyses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QrCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instruction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IdPharmacist = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdOrdreMedical = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicalAnalyseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analyses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Analyses_MedicalOrdres_IdOrdreMedical",
                        column: x => x.IdOrdreMedical,
                        principalTable: "MedicalOrdres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Analyses_Pharmacists_IdPharmacist",
                        column: x => x.IdPharmacist,
                        principalTable: "Pharmacists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Analyses_medicalAnalyses_MedicalAnalyseId",
                        column: x => x.MedicalAnalyseId,
                        principalTable: "medicalAnalyses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "prescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    qrCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    instruction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdMedicalOrdre = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_prescriptions_MedicalOrdres_IdMedicalOrdre",
                        column: x => x.IdMedicalOrdre,
                        principalTable: "MedicalOrdres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Radio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QrCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instruction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateValidation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdRadiology = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdOrdreMedical = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Radio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Radio_MedicalOrdres_IdOrdreMedical",
                        column: x => x.IdOrdreMedical,
                        principalTable: "MedicalOrdres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Radio_radiologies_IdRadiology",
                        column: x => x.IdRadiology,
                        principalTable: "radiologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "resultAnalyses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferenceFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileType = table.Column<int>(type: "int", nullable: false),
                    IdAnalyse = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resultAnalyses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_resultAnalyses_Analyses_IdAnalyse",
                        column: x => x.IdAnalyse,
                        principalTable: "Analyses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicamentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dosage = table.Column<int>(type: "int", nullable: false),
                    StatusPrescriptionLine = table.Column<int>(type: "int", nullable: false),
                    DateValidation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdPrescription = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdPharmacist = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrescriptionLines_Pharmacists_IdPharmacist",
                        column: x => x.IdPharmacist,
                        principalTable: "Pharmacists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrescriptionLines_prescriptions_IdPrescription",
                        column: x => x.IdPrescription,
                        principalTable: "prescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RadioResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferenceFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileType = table.Column<int>(type: "int", nullable: false),
                    IdRadio = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RadioResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RadioResult_Radio_IdRadio",
                        column: x => x.IdRadio,
                        principalTable: "Radio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "cabinetMedicals",
                columns: new[] { "Id", "Adress", "JobTime", "MapAdress", "NameCabinet", "Services", "image", "numberPhone", "statusService" },
                values: new object[] { new Guid("cf35304b-7896-4b81-8f57-d0dccdccb836"), "rue 112 -Kores-Biskra", "8h->16h --dimanche a jeudi", "", "Cabinet Medical El Balsem", "chirurgie-Coudre blessé-Médecin Général", null, "0541253658", 0 });

            migrationBuilder.InsertData(
                table: "chronicDiseases",
                columns: new[] { "Id", "NameChronicDiseases" },
                values: new object[,]
                {
                    { 1, "DIABÈTE" },
                    { 2, "HYPERTENSION-ARTÉRIELLE" },
                    { 3, "MALADIES-RESPIRATOIRES" },
                    { 4, "MALADIES RÉNALES" },
                    { 5, "MALADIES ARTICULAIRES" },
                    { 6, "MALADIES DU FOIE" },
                    { 7, "MALADIES NEUROLOGIQUES" },
                    { 8, "CANCER" },
                    { 9, "MALADIES-CARDIOVASCULAIRES" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("03d2395f-a472-4a41-b95f-45828d5f8af4"), "f2ffc49d-468d-4138-914d-c207fb2a840c", "RADIOLOGUE", null },
                    { new Guid("0916f1e5-ff87-4d4f-89b2-d6dbb922027e"), "471b24fe-2258-44a6-90da-ddaca9024a69", "PHARMACIEN", null },
                    { new Guid("0d518584-64a4-424b-b011-7283083394b8"), "3360e595-a431-453a-a057-1d951aadd1f8", "SECRITAIRE", null },
                    { new Guid("14e8987f-77b0-44a9-a641-6c6779b9564c"), "9979c289-ce9d-4c54-befe-9527355b11f6", "MEDECIN", null },
                    { new Guid("232d07c5-711e-4802-a048-f2f73804ea40"), "6cdcb83e-8520-4232-97ea-0920343ec437", "ANALYSE", null },
                    { new Guid("2b102f8f-079c-4ae1-b093-487ba70cf183"), "1a9ad5bc-bcdb-42be-a9f5-c75b325b6066", "PATIENT", null },
                    { new Guid("cf35304b-0241-4b81-8f57-d0dccdccb836"), "0d95d517-b6d2-4b71-831c-cd16497959a8", "ADMIN", null }
                });

            migrationBuilder.InsertData(
                table: "specialites",
                columns: new[] { "Id", "NameSpecialite" },
                values: new object[,]
                {
                    { 1, "ANESTHÉSIOLOGIE" },
                    { 2, "CARDIOLOGIE" },
                    { 3, "CHIRURGIE" },
                    { 4, "DERMATOLOGIE" },
                    { 5, "ENDOCRINOLOGIE" },
                    { 6, "GASTRO ENTÉROLOGIE" },
                    { 7, "GYNÉCOLOGIE" },
                    { 8, "HÉMATOLOGIE" },
                    { 9, "INFECTIOLOGIE" },
                    { 10, "MÉDECINE DU TRAVAIL" },
                    { 11, "MÉDECINE INTERNE" },
                    { 12, "NÉPHROLOGIE" },
                    { 13, "NEUROLOGIE" },
                    { 14, "OBSTÉTRIQUE" },
                    { 15, "ONCOLOGIE" },
                    { 16, "OPHTALMOLOGIE" },
                    { 17, "ORTHOPÉDIE" },
                    { 18, "OTO-RHINO-LARYNGOLOGIE" },
                    { 19, "PÉDIATRIE" },
                    { 20, "PNEUMOLOGIE" },
                    { 21, "PSYCHIATRIE" },
                    { 22, "RADIOLOGIE" },
                    { 23, "RHUMATOLOGIE" },
                    { 24, "URGENTISTE" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_admins_IdCabinet",
                table: "admins",
                column: "IdCabinet");

            migrationBuilder.CreateIndex(
                name: "IX_admins_IdDoctor",
                table: "admins",
                column: "IdDoctor");

            migrationBuilder.CreateIndex(
                name: "IX_Analyses_IdOrdreMedical",
                table: "Analyses",
                column: "IdOrdreMedical");

            migrationBuilder.CreateIndex(
                name: "IX_Analyses_IdPharmacist",
                table: "Analyses",
                column: "IdPharmacist");

            migrationBuilder.CreateIndex(
                name: "IX_Analyses_MedicalAnalyseId",
                table: "Analyses",
                column: "MedicalAnalyseId");

            migrationBuilder.CreateIndex(
                name: "IX_cabinetMedicals_NameCabinet",
                table: "cabinetMedicals",
                column: "NameCabinet");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_UserId",
                table: "Doctors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FileChronicDiseases_IdChronicDisease",
                table: "FileChronicDiseases",
                column: "IdChronicDisease");

            migrationBuilder.CreateIndex(
                name: "IX_FileChronicDiseases_IdFile",
                table: "FileChronicDiseases",
                column: "IdFile");

            migrationBuilder.CreateIndex(
                name: "IX_fileMedicals_firstname_Lastname",
                table: "fileMedicals",
                columns: new[] { "firstname", "Lastname" });

            migrationBuilder.CreateIndex(
                name: "IX_fileMedicals_IdDoctor",
                table: "fileMedicals",
                column: "IdDoctor");

            migrationBuilder.CreateIndex(
                name: "IX_fileMedicals_IdUser",
                table: "fileMedicals",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_medicalAnalyses_IdUser",
                table: "medicalAnalyses",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_medicalAnalyses_NameMedicalAnalyse",
                table: "medicalAnalyses",
                column: "NameMedicalAnalyse");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalOrdres_IdCabinetMedical",
                table: "MedicalOrdres",
                column: "IdCabinetMedical");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalOrdres_IdDoctor",
                table: "MedicalOrdres",
                column: "IdDoctor");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalOrdres_IdFileMedical",
                table: "MedicalOrdres",
                column: "IdFileMedical");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalOrdres_IdSecritary",
                table: "MedicalOrdres",
                column: "IdSecritary");

            migrationBuilder.CreateIndex(
                name: "IX_medicalPlannings_IdCabinet",
                table: "medicalPlannings",
                column: "IdCabinet");

            migrationBuilder.CreateIndex(
                name: "IX_medicalPlannings_IdDoctor",
                table: "medicalPlannings",
                column: "IdDoctor");

            migrationBuilder.CreateIndex(
                name: "IX_medicalPlannings_IdUser",
                table: "medicalPlannings",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacists_idUser",
                table: "Pharmacists",
                column: "idUser");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacists_PharmacistName",
                table: "Pharmacists",
                column: "PharmacistName");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionLines_IdPharmacist",
                table: "PrescriptionLines",
                column: "IdPharmacist");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionLines_IdPrescription",
                table: "PrescriptionLines",
                column: "IdPrescription");

            migrationBuilder.CreateIndex(
                name: "IX_prescriptions_IdMedicalOrdre",
                table: "prescriptions",
                column: "IdMedicalOrdre");

            migrationBuilder.CreateIndex(
                name: "IX_Radio_IdOrdreMedical",
                table: "Radio",
                column: "IdOrdreMedical");

            migrationBuilder.CreateIndex(
                name: "IX_Radio_IdRadiology",
                table: "Radio",
                column: "IdRadiology");

            migrationBuilder.CreateIndex(
                name: "IX_radiologies_IdDoctor",
                table: "radiologies",
                column: "IdDoctor");

            migrationBuilder.CreateIndex(
                name: "IX_RadioResult_IdRadio",
                table: "RadioResult",
                column: "IdRadio");

            migrationBuilder.CreateIndex(
                name: "IX_resultAnalyses_IdAnalyse",
                table: "resultAnalyses",
                column: "IdAnalyse");

            migrationBuilder.CreateIndex(
                name: "IX_Secretarys_IdCabinetMedical",
                table: "Secretarys",
                column: "IdCabinetMedical");

            migrationBuilder.CreateIndex(
                name: "IX_Secretarys_IdUser",
                table: "Secretarys",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_specialtiesDoctors_IdDoctor",
                table: "specialtiesDoctors",
                column: "IdDoctor");

            migrationBuilder.CreateIndex(
                name: "IX_specialtiesDoctors_SpecialitesId",
                table: "specialtiesDoctors",
                column: "SpecialitesId");

            migrationBuilder.CreateIndex(
                name: "IX_userRoles_IdUser",
                table: "userRoles",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_userRoles_RoleId",
                table: "userRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                table: "users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkDoctors_IdCabinet",
                table: "WorkDoctors",
                column: "IdCabinet");

            migrationBuilder.CreateIndex(
                name: "IX_WorkDoctors_IdDoctor",
                table: "WorkDoctors",
                column: "IdDoctor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "FileChronicDiseases");

            migrationBuilder.DropTable(
                name: "medicalPlannings");

            migrationBuilder.DropTable(
                name: "PrescriptionLines");

            migrationBuilder.DropTable(
                name: "RadioResult");

            migrationBuilder.DropTable(
                name: "resultAnalyses");

            migrationBuilder.DropTable(
                name: "specialtiesDoctors");

            migrationBuilder.DropTable(
                name: "userRoles");

            migrationBuilder.DropTable(
                name: "WorkDoctors");

            migrationBuilder.DropTable(
                name: "chronicDiseases");

            migrationBuilder.DropTable(
                name: "prescriptions");

            migrationBuilder.DropTable(
                name: "Radio");

            migrationBuilder.DropTable(
                name: "Analyses");

            migrationBuilder.DropTable(
                name: "specialites");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "radiologies");

            migrationBuilder.DropTable(
                name: "MedicalOrdres");

            migrationBuilder.DropTable(
                name: "Pharmacists");

            migrationBuilder.DropTable(
                name: "medicalAnalyses");

            migrationBuilder.DropTable(
                name: "Secretarys");

            migrationBuilder.DropTable(
                name: "fileMedicals");

            migrationBuilder.DropTable(
                name: "cabinetMedicals");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
