using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Swapee.Server.Data.Migrations
{
    public partial class AddedDefaultDataAndUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateUpdated", "Name", "UpdatedBy" },
                values: new object[] { 1, "System", new DateTime(2023, 2, 10, 13, 19, 37, 335, DateTimeKind.Local).AddTicks(814), new DateTime(2023, 2, 10, 13, 19, 37, 337, DateTimeKind.Local).AddTicks(4574), "Books", "System" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateUpdated", "Name", "UpdatedBy" },
                values: new object[] { 2, "System", new DateTime(2023, 2, 10, 13, 19, 37, 337, DateTimeKind.Local).AddTicks(5623), new DateTime(2023, 2, 10, 13, 19, 37, 337, DateTimeKind.Local).AddTicks(5629), "Stationary", "System" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
