using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniSurveys.Domain.Migrations
{
    /// <inheritdoc />
    public partial class initialAvatarUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HrefAvatar",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HrefAvatar",
                table: "AspNetUsers");
        }
    }
}
