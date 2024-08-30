using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class _10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("19869db0-0b54-41d9-bc76-7bb2ad7e7c82"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8b3856a5-247c-4265-b1e3-237322b9e757"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("df7883fa-d1b4-46e4-9c88-2a1b648e992c"));

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsAdmin", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("2510519a-07c4-4ac1-a5e8-13a13bacd9a8"), false, "password2", "Test2" },
                    { new Guid("c6cac9c5-2d86-4a02-9894-968d564bd5ad"), true, "password1", "Test1" },
                    { new Guid("ea8838c5-fd63-474d-a10f-e9c8ce9ed88b"), false, "password3", "Test3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2510519a-07c4-4ac1-a5e8-13a13bacd9a8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c6cac9c5-2d86-4a02-9894-968d564bd5ad"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ea8838c5-fd63-474d-a10f-e9c8ce9ed88b"));

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("19869db0-0b54-41d9-bc76-7bb2ad7e7c82"), "password3", "Test3" },
                    { new Guid("8b3856a5-247c-4265-b1e3-237322b9e757"), "password2", "Test2" },
                    { new Guid("df7883fa-d1b4-46e4-9c88-2a1b648e992c"), "password1", "Test1" }
                });
        }
    }
}
