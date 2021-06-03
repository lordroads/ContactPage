using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactPage.Migrations
{
    public partial class InitialCreateReborn3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignedEmployee",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhereIs",
                table: "Items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedEmployee",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "WhereIs",
                table: "Items");
        }
    }
}
