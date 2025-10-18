using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBaseContext.Migrations
{
    public partial class PromenaPlaniraniDaniUPlaniraniSati : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aktivnosti_Zadatak_ZadatakId",
                table: "Aktivnosti");

            migrationBuilder.DropForeignKey(
                name: "FK_RadniPaket_Projekti_ProjekatId",
                table: "RadniPaket");

            migrationBuilder.DropForeignKey(
                name: "FK_Zadatak_RadniPaket_RadniPaketId",
                table: "Zadatak");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Zadatak",
                table: "Zadatak");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RadniPaket",
                table: "RadniPaket");

            migrationBuilder.RenameTable(
                name: "Zadatak",
                newName: "Zadaci");

            migrationBuilder.RenameTable(
                name: "RadniPaket",
                newName: "RadniPaketi");

            migrationBuilder.RenameColumn(
                name: "PlaniraniDani",
                table: "Projekti",
                newName: "PlaniraniSati");

            migrationBuilder.RenameIndex(
                name: "IX_Zadatak_RadniPaketId",
                table: "Zadaci",
                newName: "IX_Zadaci_RadniPaketId");

            migrationBuilder.RenameColumn(
                name: "PlaniraniDani",
                table: "RadniPaketi",
                newName: "PlaniraniSati");

            migrationBuilder.RenameIndex(
                name: "IX_RadniPaket_ProjekatId",
                table: "RadniPaketi",
                newName: "IX_RadniPaketi_ProjekatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Zadaci",
                table: "Zadaci",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RadniPaketi",
                table: "RadniPaketi",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Aktivnosti_Zadaci_ZadatakId",
                table: "Aktivnosti",
                column: "ZadatakId",
                principalTable: "Zadaci",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RadniPaketi_Projekti_ProjekatId",
                table: "RadniPaketi",
                column: "ProjekatId",
                principalTable: "Projekti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Zadaci_RadniPaketi_RadniPaketId",
                table: "Zadaci",
                column: "RadniPaketId",
                principalTable: "RadniPaketi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aktivnosti_Zadaci_ZadatakId",
                table: "Aktivnosti");

            migrationBuilder.DropForeignKey(
                name: "FK_RadniPaketi_Projekti_ProjekatId",
                table: "RadniPaketi");

            migrationBuilder.DropForeignKey(
                name: "FK_Zadaci_RadniPaketi_RadniPaketId",
                table: "Zadaci");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Zadaci",
                table: "Zadaci");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RadniPaketi",
                table: "RadniPaketi");

            migrationBuilder.RenameTable(
                name: "Zadaci",
                newName: "Zadatak");

            migrationBuilder.RenameTable(
                name: "RadniPaketi",
                newName: "RadniPaket");

            migrationBuilder.RenameColumn(
                name: "PlaniraniSati",
                table: "Projekti",
                newName: "PlaniraniDani");

            migrationBuilder.RenameIndex(
                name: "IX_Zadaci_RadniPaketId",
                table: "Zadatak",
                newName: "IX_Zadatak_RadniPaketId");

            migrationBuilder.RenameColumn(
                name: "PlaniraniSati",
                table: "RadniPaket",
                newName: "PlaniraniDani");

            migrationBuilder.RenameIndex(
                name: "IX_RadniPaketi_ProjekatId",
                table: "RadniPaket",
                newName: "IX_RadniPaket_ProjekatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Zadatak",
                table: "Zadatak",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RadniPaket",
                table: "RadniPaket",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Aktivnosti_Zadatak_ZadatakId",
                table: "Aktivnosti",
                column: "ZadatakId",
                principalTable: "Zadatak",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RadniPaket_Projekti_ProjekatId",
                table: "RadniPaket",
                column: "ProjekatId",
                principalTable: "Projekti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Zadatak_RadniPaket_RadniPaketId",
                table: "Zadatak",
                column: "RadniPaketId",
                principalTable: "RadniPaket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
