using Microsoft.EntityFrameworkCore.Migrations;

namespace PortariaInteligente.Migrations
{
    public partial class StringConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VisitanteID1",
                table: "Visitantes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visitantes_VisitanteID1",
                table: "Visitantes",
                column: "VisitanteID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitantes_Visitantes_VisitanteID1",
                table: "Visitantes",
                column: "VisitanteID1",
                principalTable: "Visitantes",
                principalColumn: "VisitanteID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitantes_Visitantes_VisitanteID1",
                table: "Visitantes");

            migrationBuilder.DropIndex(
                name: "IX_Visitantes_VisitanteID1",
                table: "Visitantes");

            migrationBuilder.DropColumn(
                name: "VisitanteID1",
                table: "Visitantes");
        }
    }
}
