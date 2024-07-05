using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class asdasdfg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_keyWords_contents_Contentid",
                table: "keyWords");

            migrationBuilder.DropIndex(
                name: "IX_keyWords_Contentid",
                table: "keyWords");

            migrationBuilder.DropColumn(
                name: "Contentid",
                table: "keyWords");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Contentid",
                table: "keyWords",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_keyWords_Contentid",
                table: "keyWords",
                column: "Contentid");

            migrationBuilder.AddForeignKey(
                name: "FK_keyWords_contents_Contentid",
                table: "keyWords",
                column: "Contentid",
                principalTable: "contents",
                principalColumn: "id");
        }
    }
}
