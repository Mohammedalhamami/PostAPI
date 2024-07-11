using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Posts.API.Migrations
{
    /// <inheritdoc />
    public partial class updateDateTimeField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Posts");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimePosted",
                table: "Posts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "get_current_datetime_utc()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimePosted",
                table: "Posts");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "Posts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "DateTime", "Publisher" },
                values: new object[,]
                {
                    { 1, "Hello, I This is not acceptable.", new DateTime(2024, 7, 11, 5, 44, 50, 749, DateTimeKind.Utc).AddTicks(7506), "Tom" },
                    { 2, "Hey, tell your borthoer to bring the umbrella with him.", new DateTime(2024, 7, 11, 5, 44, 50, 749, DateTimeKind.Utc).AddTicks(7550), "Sam" }
                });
        }
    }
}
