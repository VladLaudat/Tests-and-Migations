using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class _11EmailAdressUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("323f8a3f-509d-455b-9ab7-b6279e571ef2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("520a3d1d-e370-4bd9-b8cc-8f3892a11738"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8789e8a4-2bce-4114-b6b7-ceb3408404d9"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsAdmin", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("27bff935-3583-4120-ae10-a43be1210ea9"), "test3@yahoo.com", false, "password3", "Test3" },
                    { new Guid("a57cc43d-1d79-496f-a1ea-3eb0e553a396"), "test1@yahoo.com", true, "password1", "Test1" },
                    { new Guid("c1c911ba-8b15-449f-ae46-b873a27129a2"), "test2@yahoo.com", false, "password2", "Test2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("27bff935-3583-4120-ae10-a43be1210ea9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a57cc43d-1d79-496f-a1ea-3eb0e553a396"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c1c911ba-8b15-449f-ae46-b873a27129a2"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsAdmin", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("323f8a3f-509d-455b-9ab7-b6279e571ef2"), "test2@yahoo.com", false, "password2", "Test2" },
                    { new Guid("520a3d1d-e370-4bd9-b8cc-8f3892a11738"), "test1@yahoo.com", true, "password1", "Test1" },
                    { new Guid("8789e8a4-2bce-4114-b6b7-ceb3408404d9"), "test3@yahoo.com", false, "password3", "Test3" }
                });
        }
    }
}
