using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class _11UsernameEmailUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("532e6206-8f6c-4698-96c8-8d1d05696f0b"), "test3@yahoo.com", false, "password3", "Test3" },
                    { new Guid("ce1b5797-5960-49f5-a4d5-5b24b899de37"), "test1@yahoo.com", true, "password1", "Test1" },
                    { new Guid("daf2d4e3-a84c-4324-8a6a-6fd70a631007"), "test2@yahoo.com", false, "password2", "Test2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName_Email",
                table: "Users",
                columns: new[] { "UserName", "Email" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_UserName_Email",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("532e6206-8f6c-4698-96c8-8d1d05696f0b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ce1b5797-5960-49f5-a4d5-5b24b899de37"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("daf2d4e3-a84c-4324-8a6a-6fd70a631007"));

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
    }
}
