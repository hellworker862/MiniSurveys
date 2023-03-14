using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniSurveys.Domain.Migrations
{
    /// <inheritdoc />
    public partial class updateResult2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Surveys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 14, 17, 1, 51, 196, DateTimeKind.Local).AddTicks(2690),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 3, 14, 16, 47, 1, 256, DateTimeKind.Local).AddTicks(9352));

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

            migrationBuilder.CreateIndex(
                name: "IX_AnswerResults_QuestionResultId",
                table: "AnswerResults",
                column: "QuestionResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerResults_QuestionResults_QuestionResultId",
                table: "AnswerResults",
                column: "QuestionResultId",
                principalTable: "QuestionResults",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerResults_QuestionResults_QuestionResultId",
                table: "AnswerResults");

            migrationBuilder.DropIndex(
                name: "IX_AnswerResults_QuestionResultId",
                table: "AnswerResults");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Surveys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 14, 16, 47, 1, 256, DateTimeKind.Local).AddTicks(9352),
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
                values: new object[] { new DateTime(2023, 3, 16, 16, 47, 1, 256, DateTimeKind.Local).AddTicks(797), new DateTime(2023, 3, 14, 16, 47, 1, 256, DateTimeKind.Local).AddTicks(785) });

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 18, 18, 47, 1, 256, DateTimeKind.Local).AddTicks(805), new DateTime(2023, 3, 16, 18, 47, 1, 256, DateTimeKind.Local).AddTicks(804) });

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 12, 14, 47, 1, 256, DateTimeKind.Local).AddTicks(807), new DateTime(2023, 3, 10, 14, 47, 1, 256, DateTimeKind.Local).AddTicks(806) });
        }
    }
}
