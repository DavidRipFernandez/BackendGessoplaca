using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NSoft.Migrations
{
    /// <inheritdoc />
    public partial class modificacionrelaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materiales_CategoriasMateriales_CategoriasMaterialesCategoriaId",
                table: "Materiales");

            migrationBuilder.DropForeignKey(
                name: "FK_PreciosTarifas_ProveedoresMarcas_ProveedoresMarcasProveedorCifId_ProveedoresMarcasMarcaId",
                table: "PreciosTarifas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PreciosTarifas",
                table: "PreciosTarifas");

            migrationBuilder.DropIndex(
                name: "IX_PreciosTarifas_MaterialId",
                table: "PreciosTarifas");

            migrationBuilder.DropIndex(
                name: "IX_Materiales_CategoriasMaterialesCategoriaId",
                table: "Materiales");

            migrationBuilder.DropColumn(
                name: "PrecioTarifaId",
                table: "PreciosTarifas");

            migrationBuilder.DropColumn(
                name: "ProveedorMarcaId",
                table: "PreciosTarifas");

            migrationBuilder.DropColumn(
                name: "CategoriasMaterialesCategoriaId",
                table: "Materiales");

            migrationBuilder.RenameColumn(
                name: "ProveedoresMarcasProveedorCifId",
                table: "PreciosTarifas",
                newName: "ProveedorCifId");

            migrationBuilder.RenameColumn(
                name: "ProveedoresMarcasMarcaId",
                table: "PreciosTarifas",
                newName: "MarcaId");

            migrationBuilder.RenameIndex(
                name: "IX_PreciosTarifas_ProveedoresMarcasProveedorCifId_ProveedoresMarcasMarcaId",
                table: "PreciosTarifas",
                newName: "IX_PreciosTarifas_ProveedorCifId_MarcaId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "TiposPermiso",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "RolesModulos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "ProveedoresMarcas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MarcaId",
                table: "ProveedoresMarcas",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "ProveedorCifId",
                table: "ProveedoresMarcas",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "Proveedores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "PreciosTarifas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Estado",
                table: "PreciosTarifas",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "Modulos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SistemaMedicion",
                table: "Materiales",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "Materiales",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "Marcas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Marcas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "Contactos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "CategoriasMateriales",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "CategoriasMateriales",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PreciosTarifas",
                table: "PreciosTarifas",
                columns: new[] { "MaterialId", "ProveedorCifId", "MarcaId" });

            migrationBuilder.CreateIndex(
                name: "IX_PreciosTarifas_MarcaId",
                table: "PreciosTarifas",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Materiales_CategoriaId",
                table: "Materiales",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materiales_CategoriasMateriales_CategoriaId",
                table: "Materiales",
                column: "CategoriaId",
                principalTable: "CategoriasMateriales",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PreciosTarifas_Marcas_MarcaId",
                table: "PreciosTarifas",
                column: "MarcaId",
                principalTable: "Marcas",
                principalColumn: "MarcaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PreciosTarifas_ProveedoresMarcas_ProveedorCifId_MarcaId",
                table: "PreciosTarifas",
                columns: new[] { "ProveedorCifId", "MarcaId" },
                principalTable: "ProveedoresMarcas",
                principalColumns: new[] { "ProveedorCifId", "MarcaId" },
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materiales_CategoriasMateriales_CategoriaId",
                table: "Materiales");

            migrationBuilder.DropForeignKey(
                name: "FK_PreciosTarifas_Marcas_MarcaId",
                table: "PreciosTarifas");

            migrationBuilder.DropForeignKey(
                name: "FK_PreciosTarifas_ProveedoresMarcas_ProveedorCifId_MarcaId",
                table: "PreciosTarifas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PreciosTarifas",
                table: "PreciosTarifas");

            migrationBuilder.DropIndex(
                name: "IX_PreciosTarifas_MarcaId",
                table: "PreciosTarifas");

            migrationBuilder.DropIndex(
                name: "IX_Materiales_CategoriaId",
                table: "Materiales");

            migrationBuilder.RenameColumn(
                name: "MarcaId",
                table: "PreciosTarifas",
                newName: "ProveedoresMarcasMarcaId");

            migrationBuilder.RenameColumn(
                name: "ProveedorCifId",
                table: "PreciosTarifas",
                newName: "ProveedoresMarcasProveedorCifId");

            migrationBuilder.RenameIndex(
                name: "IX_PreciosTarifas_ProveedorCifId_MarcaId",
                table: "PreciosTarifas",
                newName: "IX_PreciosTarifas_ProveedoresMarcasProveedorCifId_ProveedoresMarcasMarcaId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "TiposPermiso",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "RolesModulos",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "Roles",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "ProveedoresMarcas",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "MarcaId",
                table: "ProveedoresMarcas",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "ProveedorCifId",
                table: "ProveedoresMarcas",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "Proveedores",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "PreciosTarifas",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<bool>(
                name: "Estado",
                table: "PreciosTarifas",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AddColumn<int>(
                name: "PrecioTarifaId",
                table: "PreciosTarifas",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ProveedorMarcaId",
                table: "PreciosTarifas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "Modulos",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "SistemaMedicion",
                table: "Materiales",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "Materiales",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "CategoriasMaterialesCategoriaId",
                table: "Materiales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "Marcas",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Marcas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "Contactos",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "CategoriasMateriales",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "CategoriasMateriales",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PreciosTarifas",
                table: "PreciosTarifas",
                column: "PrecioTarifaId");

            migrationBuilder.CreateIndex(
                name: "IX_PreciosTarifas_MaterialId",
                table: "PreciosTarifas",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Materiales_CategoriasMaterialesCategoriaId",
                table: "Materiales",
                column: "CategoriasMaterialesCategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materiales_CategoriasMateriales_CategoriasMaterialesCategoriaId",
                table: "Materiales",
                column: "CategoriasMaterialesCategoriaId",
                principalTable: "CategoriasMateriales",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PreciosTarifas_ProveedoresMarcas_ProveedoresMarcasProveedorCifId_ProveedoresMarcasMarcaId",
                table: "PreciosTarifas",
                columns: new[] { "ProveedoresMarcasProveedorCifId", "ProveedoresMarcasMarcaId" },
                principalTable: "ProveedoresMarcas",
                principalColumns: new[] { "ProveedorCifId", "MarcaId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
