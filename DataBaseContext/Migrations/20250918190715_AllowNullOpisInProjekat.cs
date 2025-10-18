using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBaseContext.Migrations
{
    public partial class AllowNullOpisInProjekat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projekti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Naziv = table.Column<string>(type: "TEXT", nullable: false),
                    Opis = table.Column<string>(type: "TEXT", nullable: true),
                    PlaniraniDani = table.Column<int>(type: "INTEGER", nullable: false),
                    DatumPocetka = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DatumZavrsetka = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projekti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zaposleni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ime = table.Column<string>(type: "TEXT", nullable: false),
                    Prezime = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Pozicija = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zaposleni", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RadniPaket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Naziv = table.Column<string>(type: "TEXT", nullable: false),
                    Opis = table.Column<string>(type: "TEXT", nullable: true),
                    PlaniraniDani = table.Column<int>(type: "INTEGER", nullable: false),
                    ProjekatId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RadniPaket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RadniPaket_Projekti_ProjekatId",
                        column: x => x.ProjekatId,
                        principalTable: "Projekti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Timovi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Naziv = table.Column<string>(type: "TEXT", nullable: false),
                    ProjekatId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timovi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Timovi_Projekti_ProjekatId",
                        column: x => x.ProjekatId,
                        principalTable: "Projekti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zadatak",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Naziv = table.Column<string>(type: "TEXT", nullable: false),
                    Opis = table.Column<string>(type: "TEXT", nullable: true),
                    PlaniraniDani = table.Column<int>(type: "INTEGER", nullable: false),
                    RadniPaketId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zadatak", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zadatak_RadniPaket_RadniPaketId",
                        column: x => x.RadniPaketId,
                        principalTable: "RadniPaket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClanoviTimova",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TimId = table.Column<int>(type: "INTEGER", nullable: false),
                    ZaposleniId = table.Column<int>(type: "INTEGER", nullable: false),
                    Uloga = table.Column<string>(type: "TEXT", nullable: true),
                    DatumPridruzivanja = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClanoviTimova", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClanoviTimova_Timovi_TimId",
                        column: x => x.TimId,
                        principalTable: "Timovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClanoviTimova_Zaposleni_ZaposleniId",
                        column: x => x.ZaposleniId,
                        principalTable: "Zaposleni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Aktivnosti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Naziv = table.Column<string>(type: "TEXT", nullable: false),
                    Opis = table.Column<string>(type: "TEXT", nullable: true),
                    PlaniraniSati = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    ZadatakId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aktivnosti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aktivnosti_Zadatak_ZadatakId",
                        column: x => x.ZadatakId,
                        principalTable: "Zadatak",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DodeleZadataka",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ZaposleniId = table.Column<int>(type: "INTEGER", nullable: false),
                    AktivnostId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DodeleZadataka", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DodeleZadataka_Aktivnosti_AktivnostId",
                        column: x => x.AktivnostId,
                        principalTable: "Aktivnosti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DodeleZadataka_Zaposleni_ZaposleniId",
                        column: x => x.ZaposleniId,
                        principalTable: "Zaposleni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aktivnosti_ZadatakId",
                table: "Aktivnosti",
                column: "ZadatakId");

            migrationBuilder.CreateIndex(
                name: "IX_ClanoviTimova_TimId",
                table: "ClanoviTimova",
                column: "TimId");

            migrationBuilder.CreateIndex(
                name: "IX_ClanoviTimova_ZaposleniId",
                table: "ClanoviTimova",
                column: "ZaposleniId");

            migrationBuilder.CreateIndex(
                name: "IX_DodeleZadataka_AktivnostId",
                table: "DodeleZadataka",
                column: "AktivnostId");

            migrationBuilder.CreateIndex(
                name: "IX_DodeleZadataka_ZaposleniId",
                table: "DodeleZadataka",
                column: "ZaposleniId");

            migrationBuilder.CreateIndex(
                name: "IX_RadniPaket_ProjekatId",
                table: "RadniPaket",
                column: "ProjekatId");

            migrationBuilder.CreateIndex(
                name: "IX_Timovi_ProjekatId",
                table: "Timovi",
                column: "ProjekatId");

            migrationBuilder.CreateIndex(
                name: "IX_Zadatak_RadniPaketId",
                table: "Zadatak",
                column: "RadniPaketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClanoviTimova");

            migrationBuilder.DropTable(
                name: "DodeleZadataka");

            migrationBuilder.DropTable(
                name: "Timovi");

            migrationBuilder.DropTable(
                name: "Aktivnosti");

            migrationBuilder.DropTable(
                name: "Zaposleni");

            migrationBuilder.DropTable(
                name: "Zadatak");

            migrationBuilder.DropTable(
                name: "RadniPaket");

            migrationBuilder.DropTable(
                name: "Projekti");
        }
    }
}
