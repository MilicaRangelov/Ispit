using Microsoft.EntityFrameworkCore.Migrations;

namespace April20.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Naslov",
                table: "DVDS");

            migrationBuilder.AddColumn<int>(
                name: "Broj",
                table: "DVDS",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Broj",
                table: "DVDS");

            migrationBuilder.AddColumn<string>(
                name: "Naslov",
                table: "DVDS",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
