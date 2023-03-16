using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniSurveys.Domain.Migrations
{
    /// <inheritdoc />
    public partial class updateAnswerResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Surveys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 16, 15, 13, 0, 979, DateTimeKind.Local).AddTicks(7597),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 3, 14, 17, 1, 51, 196, DateTimeKind.Local).AddTicks(2690));

            migrationBuilder.AlterColumn<int>(
                name: "QuestionResultId",
                table: "AnswerResults",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 18, 15, 13, 0, 978, DateTimeKind.Local).AddTicks(3313), new DateTime(2023, 3, 16, 15, 13, 0, 978, DateTimeKind.Local).AddTicks(3298) });

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 20, 17, 13, 0, 978, DateTimeKind.Local).AddTicks(3322), new DateTime(2023, 3, 18, 17, 13, 0, 978, DateTimeKind.Local).AddTicks(3321) });

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 14, 13, 13, 0, 978, DateTimeKind.Local).AddTicks(3324), new DateTime(2023, 3, 12, 13, 13, 0, 978, DateTimeKind.Local).AddTicks(3323) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Surveys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 14, 17, 1, 51, 196, DateTimeKind.Local).AddTicks(2690),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 3, 16, 15, 13, 0, 979, DateTimeKind.Local).AddTicks(7597));

            migrationBuilder.AlterColumn<int>(
                name: "QuestionResultId",
                table: "AnswerResults",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 16, 17, 1, 51, 193, DateTimeKind.Local).AddTicks(1588), new DateTime(2023, 3, 14, 17, 1, 51, 193, DateTimeKind.Local).AddTicks(1573) });

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 18, 19, 1, 51, 193, DateTimeKind.Local).AddTicks(1599), new DateTime(2023, 3, 16, 19, 1, 51, 193, DateTimeKind.Local).AddTicks(1598) });

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 12, 15, 1, 51, 193, DateTimeKind.Local).AddTicks(1602), new DateTime(2023, 3, 10, 15, 1, 51, 193, DateTimeKind.Local).AddTicks(1601) });
        }
    }
}
