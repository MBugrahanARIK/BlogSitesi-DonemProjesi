using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class asdzxasdf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "comments");

            migrationBuilder.AddColumn<byte>(
                name: "status",
                table: "comments",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "comments");

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "comments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
