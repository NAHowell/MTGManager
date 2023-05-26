using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTGCollectionManager.Data.Migrations
{
    public partial class ownermigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "WishListDb",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Decks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "WishListDb");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Cards");
        }
    }
}
