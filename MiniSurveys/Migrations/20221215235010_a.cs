using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniSurveys.Domain.Migrations
{
    /// <inheritdoc />
    public partial class a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Surveys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 16, 4, 50, 9, 482, DateTimeKind.Local).AddTicks(7272),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 2, 14, 33, 22, 310, DateTimeKind.Local).AddTicks(7989));

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTimeEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SurveyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2022, 12, 18, 4, 50, 9, 480, DateTimeKind.Local).AddTicks(84), new DateTime(2022, 12, 16, 4, 50, 9, 480, DateTimeKind.Local).AddTicks(70) });

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2022, 12, 20, 6, 50, 9, 480, DateTimeKind.Local).AddTicks(102), new DateTime(2022, 12, 18, 6, 50, 9, 480, DateTimeKind.Local).AddTicks(100) });

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2022, 12, 14, 2, 50, 9, 480, DateTimeKind.Local).AddTicks(104), new DateTime(2022, 12, 12, 2, 50, 9, 480, DateTimeKind.Local).AddTicks(103) });

            migrationBuilder.CreateIndex(
                name: "IX_Results_SurveyId",
                table: "Results",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_UserId",
                table: "Results",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Surveys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 2, 14, 33, 22, 310, DateTimeKind.Local).AddTicks(7989),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 12, 16, 4, 50, 9, 482, DateTimeKind.Local).AddTicks(7272));

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2022, 12, 4, 14, 33, 22, 310, DateTimeKind.Local).AddTicks(1642), new DateTime(2022, 12, 2, 14, 33, 22, 310, DateTimeKind.Local).AddTicks(1629) });

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2022, 12, 6, 16, 33, 22, 310, DateTimeKind.Local).AddTicks(1650), new DateTime(2022, 12, 4, 16, 33, 22, 310, DateTimeKind.Local).AddTicks(1649) });

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2022, 11, 30, 12, 33, 22, 310, DateTimeKind.Local).AddTicks(1652), new DateTime(2022, 11, 28, 12, 33, 22, 310, DateTimeKind.Local).AddTicks(1651) });
        }
    }
}
