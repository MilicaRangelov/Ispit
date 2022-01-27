using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Septembar21.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drzava",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drzava", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Rezervacija",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumOd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumDo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kapacitet = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacija", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Mesto",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DrzavaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesto", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Mesto_Drzava_DrzavaID",
                        column: x => x.DrzavaID,
                        principalTable: "Drzava",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SmestajniObjekat",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MestoID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmestajniObjekat", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SmestajniObjekat_Mesto_MestoID",
                        column: x => x.MestoID,
                        principalTable: "Mesto",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Soba",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrSobe = table.Column<int>(type: "int", nullable: false),
                    SmestajniObjekatID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Soba", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Soba_SmestajniObjekat_SmestajniObjekatID",
                        column: x => x.SmestajniObjekatID,
                        principalTable: "SmestajniObjekat",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RezervacijaSoba",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Zauzeto = table.Column<bool>(type: "bit", nullable: false),
                    SobaID = table.Column<int>(type: "int", nullable: true),
                    RezervacijaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezervacijaSoba", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RezervacijaSoba_Rezervacija_RezervacijaID",
                        column: x => x.RezervacijaID,
                        principalTable: "Rezervacija",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RezervacijaSoba_Soba_SobaID",
                        column: x => x.SobaID,
                        principalTable: "Soba",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mesto_DrzavaID",
                table: "Mesto",
                column: "DrzavaID");

            migrationBuilder.CreateIndex(
                name: "IX_RezervacijaSoba_RezervacijaID",
                table: "RezervacijaSoba",
                column: "RezervacijaID");

            migrationBuilder.CreateIndex(
                name: "IX_RezervacijaSoba_SobaID",
                table: "RezervacijaSoba",
                column: "SobaID");

            migrationBuilder.CreateIndex(
                name: "IX_SmestajniObjekat_MestoID",
                table: "SmestajniObjekat",
                column: "MestoID");

            migrationBuilder.CreateIndex(
                name: "IX_Soba_SmestajniObjekatID",
                table: "Soba",
                column: "SmestajniObjekatID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RezervacijaSoba");

            migrationBuilder.DropTable(
                name: "Rezervacija");

            migrationBuilder.DropTable(
                name: "Soba");

            migrationBuilder.DropTable(
                name: "SmestajniObjekat");

            migrationBuilder.DropTable(
                name: "Mesto");

            migrationBuilder.DropTable(
                name: "Drzava");
        }
    }
}
