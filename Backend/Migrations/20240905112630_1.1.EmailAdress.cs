using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class _11EmailAdress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("0ac8362c-d0ed-4bd5-afe5-5e4e928cd402"), "test2@yahoo.com", false, "password2", "Test2" },
                    { new Guid("83522e28-36a3-40f5-b125-38f533b75d3d"), "test1@yahoo.com", true, "password1", "Test1" },
                    { new Guid("eb0c05a5-cfce-4045-ae0a-246d6155d7b2"), "test3@yahoo.com", false, "password3", "Test3" }
                });
        }
    }
}
