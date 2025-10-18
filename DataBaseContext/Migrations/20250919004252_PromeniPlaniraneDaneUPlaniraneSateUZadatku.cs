using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBaseContext.Migrations
{
    public partial class PromeniPlaniraneDaneUPlaniraneSateUZadatku : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlaniraniDani",
                table: "Zadaci",
                newName: "PlaniraniSati");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlaniraniSati",
                table: "Zadaci",
                newName: "PlaniraniDani");
        }
    }
}
