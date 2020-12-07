using Microsoft.EntityFrameworkCore.Migrations;

namespace PortariaInteligente.Migrations
{
    public partial class VisitanteConfirmado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Confirmado",
                table: "Convites",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confirmado",
                table: "Convites");
        }
    }
}
