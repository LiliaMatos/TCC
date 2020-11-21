using Microsoft.EntityFrameworkCore.Migrations;

namespace PortariaInteligente.Migrations
{
    public partial class cor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarroCor",
                table: "Visitantes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarroCor",
                table: "Visitantes");
        }
    }
}
