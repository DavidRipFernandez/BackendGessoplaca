using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NSoft.Migrations
{
    /// <inheritdoc />
    public partial class Migracion09032025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProveedorMarcaId",
                table: "ProveedoresMarcas");

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "TiposPermiso",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "RolesModulos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "ProveedoresMarcas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Proveedores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "PreciosTarifas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Modulos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Materiales",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Marcas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Contactos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "CategoriasMateriales",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "TiposPermiso");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "RolesModulos");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "ProveedoresMarcas");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Proveedores");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "PreciosTarifas");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Modulos");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Materiales");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Marcas");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Contactos");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "CategoriasMateriales");

            migrationBuilder.AddColumn<int>(
                name: "ProveedorMarcaId",
                table: "ProveedoresMarcas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
