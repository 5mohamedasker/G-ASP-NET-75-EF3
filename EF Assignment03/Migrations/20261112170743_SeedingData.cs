using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EF_Assignment03.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "ParentId" },
                values: new object[] { 10, "Software development", "Programming", null });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "Id", "Bio", "Email", "FullName", "JoinedDate" },
                values: new object[,]
                {
                    { 10, "Good Instructor", "ahmed@gmail.com", "Ahmed", new DateOnly(2020, 3, 3) },
                    { 20, "Good Mentor", "Amr@gmail.com", "Amr", new DateOnly(2025, 4, 4) }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "DateOfBirth", "Email", "FullName" },
                values: new object[,]
                {
                    { 10, new DateOnly(2002, 4, 6), "mohamed@gmail.com", "Mohamed" },
                    { 20, new DateOnly(1999, 9, 2), "asker@gmail.com", "Asker" },
                    { 30, new DateOnly(2012, 11, 12), "abdullah@gmail.com", "Abdullah" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "ParentId" },
                values: new object[,]
                {
                    { 20, null, "Backend", 10 },
                    { 30, null, "DataBase", 20 }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "Description", "IsPublished", "Level", "Price", "PublishedDate", "PublsherId", "Title" },
                values: new object[,]
                {
                    { 10, 20, "Fundamentals", true, 1, 999m, new DateTime(2026, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "C#" },
                    { 20, 30, "Fundamentals", true, 2, 1500m, new DateTime(2026, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "EF Core" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
