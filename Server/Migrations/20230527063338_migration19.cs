using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class migration19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "city",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Adrar" },
                    { 2, "Chlef" },
                    { 3, "Laghouat" },
                    { 4, "Oum El Bouaghi" },
                    { 5, "Batna" },
                    { 6, "Béjaïa" },
                    { 7, "Biskra" },
                    { 8, "Béchar" },
                    { 9, "Blida" },
                    { 10, "Bouira" },
                    { 11, "Tamanrasset" },
                    { 12, "Tébessa" },
                    { 13, "Tlemcen" },
                    { 14, "Tiaret" },
                    { 15, "Tizi Ouzou" },
                    { 16, "Alger" },
                    { 17, "Djelfa" },
                    { 18, "Jijel" },
                    { 19, "Sétif" },
                    { 20, "Saïda" },
                    { 21, "Skikda" },
                    { 22, "Sidi Bel Abbès" },
                    { 23, "Annaba" },
                    { 24, "Guelma" },
                    { 25, "Constantine" },
                    { 26, "Médéa" },
                    { 27, "Mostaganem" },
                    { 28, "M'Sila" },
                    { 29, "Mascara" },
                    { 30, "Ouargla" },
                    { 31, "Oran" },
                    { 32, "El Bayadh" },
                    { 33, "Illizi" },
                    { 34, "Bordj Bou Arréridj" },
                    { 35, "Boumerdès" },
                    { 36, "El Tarf" },
                    { 37, "Tindouf" },
                    { 38, "Tissemsilt" },
                    { 39, "El Oued" },
                    { 40, "Khenchela" },
                    { 41, "Souk Ahras" },
                    { 42, "Tipaza" },
                    { 43, "Mila" },
                    { 44, "Aïn Defla" },
                    { 45, "Naâma" },
                    { 46, "Aïn Témouchent" },
                    { 47, "Ghardaïa" },
                    { 48, "Relizane" }
                });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("03d2395f-a472-4a41-b95f-45828d5f8af4"),
                column: "ConcurrencyStamp",
                value: "06eb7324-a83b-48e0-ae95-f66ff140d666");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0916f1e5-ff87-4d4f-89b2-d6dbb922027e"),
                column: "ConcurrencyStamp",
                value: "d81ee3cf-93c4-426c-8b92-b5896eb675e5");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0d518584-64a4-424b-b011-7283083394b8"),
                column: "ConcurrencyStamp",
                value: "ad39d2d9-1a41-44c8-9c6d-2d203ddba145");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("14e8987f-77b0-44a9-a641-6c6779b9564c"),
                column: "ConcurrencyStamp",
                value: "313646b1-f8fb-4836-9153-d8bac5cffb89");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("232d07c5-711e-4802-a048-f2f73804ea40"),
                column: "ConcurrencyStamp",
                value: "6f820d9e-027e-4a77-8e05-e49632aee4e3");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("2b102f8f-079c-4ae1-b093-487ba70cf183"),
                column: "ConcurrencyStamp",
                value: "c5ff8f33-a3f6-42d7-b4b3-7fd4f8e190b2");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("cf35304b-0241-4b81-8f57-d0dccdccb836"),
                column: "ConcurrencyStamp",
                value: "934fd86b-f690-45b2-bd08-a72db619468c");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "city",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("03d2395f-a472-4a41-b95f-45828d5f8af4"),
                column: "ConcurrencyStamp",
                value: "3bdbfbca-780f-4669-bc5c-0365ffb1861b");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0916f1e5-ff87-4d4f-89b2-d6dbb922027e"),
                column: "ConcurrencyStamp",
                value: "198ab4b4-565b-45c8-bd6f-ab962418cc9d");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0d518584-64a4-424b-b011-7283083394b8"),
                column: "ConcurrencyStamp",
                value: "c8828f1f-57e1-4e96-a976-f89276b27a50");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("14e8987f-77b0-44a9-a641-6c6779b9564c"),
                column: "ConcurrencyStamp",
                value: "35765c70-86c7-427b-bd50-ec3bce041522");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("232d07c5-711e-4802-a048-f2f73804ea40"),
                column: "ConcurrencyStamp",
                value: "fcf6c271-be09-4ded-8b79-85e9a234d1e3");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("2b102f8f-079c-4ae1-b093-487ba70cf183"),
                column: "ConcurrencyStamp",
                value: "4aa38939-bc3e-4890-8a56-07895fc88d38");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("cf35304b-0241-4b81-8f57-d0dccdccb836"),
                column: "ConcurrencyStamp",
                value: "fec3fc07-bc10-4a90-8507-47ae594d3853");
        }
    }
}
