using Microsoft.EntityFrameworkCore.Migrations;

namespace April20.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Police",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vrsta = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Velicina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Police", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VideoKlubovi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoKlubovi", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DVDS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naslov = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PoliceID = table.Column<int>(type: "int", nullable: true),
                    KluboviID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DVDS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DVDS_Police_PoliceID",
                        column: x => x.PoliceID,
                        principalTable: "Police",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DVDS_VideoKlubovi_KluboviID",
                        column: x => x.KluboviID,
                        principalTable: "VideoKlubovi",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DVDS_KluboviID",
                table: "DVDS",
                column: "KluboviID");

            migrationBuilder.CreateIndex(
                name: "IX_DVDS_PoliceID",
                table: "DVDS",
                column: "PoliceID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DVDS");

            migrationBuilder.DropTable(
                name: "Police");

            migrationBuilder.DropTable(
                name: "VideoKlubovi");
        }
    }
}
