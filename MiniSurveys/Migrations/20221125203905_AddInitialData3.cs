using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniSurveys.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialData3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Surveys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 26, 1, 39, 4, 796, DateTimeKind.Local).AddTicks(5163),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 26, 1, 37, 19, 53, DateTimeKind.Local).AddTicks(4800));

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2022, 11, 28, 1, 39, 4, 794, DateTimeKind.Local).AddTicks(6107), new DateTime(2022, 11, 26, 1, 39, 4, 794, DateTimeKind.Local).AddTicks(6058) });

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2022, 11, 30, 3, 39, 4, 794, DateTimeKind.Local).AddTicks(6127), new DateTime(2022, 11, 28, 3, 39, 4, 794, DateTimeKind.Local).AddTicks(6126) });

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndTime", "StartTime", "Title" },
                values: new object[] { new DateTime(2022, 11, 23, 23, 39, 4, 794, DateTimeKind.Local).AddTicks(6130), new DateTime(2022, 11, 21, 23, 39, 4, 794, DateTimeKind.Local).AddTicks(6129), "Тест 0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Surveys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 26, 1, 37, 19, 53, DateTimeKind.Local).AddTicks(4800),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 26, 1, 39, 4, 796, DateTimeKind.Local).AddTicks(5163));

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2022, 11, 28, 1, 37, 19, 52, DateTimeKind.Local).AddTicks(5698), new DateTime(2022, 11, 26, 1, 37, 19, 52, DateTimeKind.Local).AddTicks(5684) });

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2022, 11, 30, 3, 37, 19, 52, DateTimeKind.Local).AddTicks(5713), new DateTime(2022, 11, 28, 3, 37, 19, 52, DateTimeKind.Local).AddTicks(5712) });

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndTime", "StartTime", "Title" },
                values: new object[] { new DateTime(2022, 11, 23, 23, 37, 19, 52, DateTimeKind.Local).AddTicks(5716), new DateTime(2022, 11, 21, 23, 37, 19, 52, DateTimeKind.Local).AddTicks(5715), "Тест 2" });
        }
    }
}
