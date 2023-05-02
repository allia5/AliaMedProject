using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class migration14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_adviceMedicals_users_ReceiverUserId",
                table: "adviceMedicals");

            migrationBuilder.DropForeignKey(
                name: "FK_adviceMedicals_users_TransmitterUserId",
                table: "adviceMedicals");

            migrationBuilder.DropIndex(
                name: "IX_adviceMedicals_ReceiverUserId",
                table: "adviceMedicals");

            migrationBuilder.DropColumn(
                name: "ReceiverUserId",
                table: "adviceMedicals");

            migrationBuilder.RenameColumn(
                name: "TransmitterUserId",
                table: "adviceMedicals",
                newName: "transmitterUserId");

            migrationBuilder.RenameIndex(
                name: "IX_adviceMedicals_TransmitterUserId",
                table: "adviceMedicals",
                newName: "IX_adviceMedicals_transmitterUserId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_adviceMedicals_users_transmitterUserId",
                table: "adviceMedicals",
                column: "transmitterUserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_adviceMedicals_users_transmitterUserId",
                table: "adviceMedicals");

            migrationBuilder.RenameColumn(
                name: "transmitterUserId",
                table: "adviceMedicals",
                newName: "TransmitterUserId");

            migrationBuilder.RenameIndex(
                name: "IX_adviceMedicals_transmitterUserId",
                table: "adviceMedicals",
                newName: "IX_adviceMedicals_TransmitterUserId");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverUserId",
                table: "adviceMedicals",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("03d2395f-a472-4a41-b95f-45828d5f8af4"),
                column: "ConcurrencyStamp",
                value: "16d3050d-8938-4506-9589-c9848fc4b083");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0916f1e5-ff87-4d4f-89b2-d6dbb922027e"),
                column: "ConcurrencyStamp",
                value: "c536dd1d-43a6-48d7-a517-8539bfc5748d");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("0d518584-64a4-424b-b011-7283083394b8"),
                column: "ConcurrencyStamp",
                value: "dc7a81f6-9ef9-4811-ae77-7e43d4feffb1");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("14e8987f-77b0-44a9-a641-6c6779b9564c"),
                column: "ConcurrencyStamp",
                value: "fd6a6c0c-1016-4469-9702-de0034b09cea");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("232d07c5-711e-4802-a048-f2f73804ea40"),
                column: "ConcurrencyStamp",
                value: "12235338-71cb-42d8-9ca3-cb9d7b16e194");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("2b102f8f-079c-4ae1-b093-487ba70cf183"),
                column: "ConcurrencyStamp",
                value: "2dfe2147-07c6-4524-8eb4-196cf95f010b");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("cf35304b-0241-4b81-8f57-d0dccdccb836"),
                column: "ConcurrencyStamp",
                value: "59ac619c-d5d9-457a-a1d2-cf3f9d576bf3");

            migrationBuilder.CreateIndex(
                name: "IX_adviceMedicals_ReceiverUserId",
                table: "adviceMedicals",
                column: "ReceiverUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_adviceMedicals_users_ReceiverUserId",
                table: "adviceMedicals",
                column: "ReceiverUserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_adviceMedicals_users_TransmitterUserId",
                table: "adviceMedicals",
                column: "TransmitterUserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
