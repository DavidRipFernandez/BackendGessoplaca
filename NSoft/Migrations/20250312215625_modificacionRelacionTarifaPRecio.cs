using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NSoft.Migrations
{
    /// <inheritdoc />
    public partial class modificacionRelacionTarifaPRecio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreciosTarifas_Marcas_MarcaId",
                table: "PreciosTarifas");

            migrationBuilder.DropIndex(
                name: "IX_PreciosTarifas_MarcaId",
                table: "PreciosTarifas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PreciosTarifas_MarcaId",
                table: "PreciosTarifas",
                column: "MarcaId");

            migrationBuilder.AddForeignKey(
                name: "FK_PreciosTarifas_Marcas_MarcaId",
                table: "PreciosTarifas",
                column: "MarcaId",
                principalTable: "Marcas",
                principalColumn: "MarcaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
