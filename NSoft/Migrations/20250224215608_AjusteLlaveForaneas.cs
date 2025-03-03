using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NSoft.Migrations
{
    /// <inheritdoc />
    public partial class AjusteLlaveForaneas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contactos_Proveedores_ProveedoresProveedorCifId",
                table: "Contactos");

            migrationBuilder.DropIndex(
                name: "IX_Contactos_ProveedoresProveedorCifId",
                table: "Contactos");

            migrationBuilder.DropColumn(
                name: "ProveedoresProveedorCifId",
                table: "Contactos");

            migrationBuilder.AlterColumn<string>(
                name: "ProveedorCifId",
                table: "Contactos",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Contactos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Correo",
                table: "Contactos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_ProveedorCifId",
                table: "Contactos",
                column: "ProveedorCifId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contactos_Proveedores_ProveedorCifId",
                table: "Contactos",
                column: "ProveedorCifId",
                principalTable: "Proveedores",
                principalColumn: "ProveedorCifId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contactos_Proveedores_ProveedorCifId",
                table: "Contactos");

            migrationBuilder.DropIndex(
                name: "IX_Contactos_ProveedorCifId",
                table: "Contactos");

            migrationBuilder.AlterColumn<string>(
                name: "ProveedorCifId",
                table: "Contactos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Contactos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Correo",
                table: "Contactos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProveedoresProveedorCifId",
                table: "Contactos",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_ProveedoresProveedorCifId",
                table: "Contactos",
                column: "ProveedoresProveedorCifId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contactos_Proveedores_ProveedoresProveedorCifId",
                table: "Contactos",
                column: "ProveedoresProveedorCifId",
                principalTable: "Proveedores",
                principalColumn: "ProveedorCifId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
