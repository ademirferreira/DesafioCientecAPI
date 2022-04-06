using Microsoft.EntityFrameworkCore.Migrations;

namespace DesafioCientec.Data.Migrations
{
    public partial class RemoveUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Fundacoes_Documento",
                table: "Fundacoes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Fundacoes_Documento",
                table: "Fundacoes",
                column: "Documento",
                unique: true);
        }
    }
}
