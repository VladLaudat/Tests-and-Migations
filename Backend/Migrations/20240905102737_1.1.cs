using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class _11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsAdmin", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("0ac8362c-d0ed-4bd5-afe5-5e4e928cd402"), "test2@yahoo.com", false, "password2", "Test2" },
                    { new Guid("83522e28-36a3-40f5-b125-38f533b75d3d"), "test1@yahoo.com", true, "password1", "Test1" },
                    { new Guid("eb0c05a5-cfce-4045-ae0a-246d6155d7b2"), "test3@yahoo.com", false, "password3", "Test3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0ac8362c-d0ed-4bd5-afe5-5e4e928cd402"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("83522e28-36a3-40f5-b125-38f533b75d3d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("eb0c05a5-cfce-4045-ae0a-246d6155d7b2"));

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

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
    }
}
