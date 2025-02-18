using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NSoft.Migrations
{
    /// <inheritdoc />
    public partial class CorreccionCampoEnModulos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecurityStampo",
                table: "Modulos",
                newName: "SecurityStamp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecurityStamp",
                table: "Modulos",
                newName: "SecurityStampo");
        }
    }
}
