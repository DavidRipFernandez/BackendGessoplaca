using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NSoft.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Materiales");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Materiales",
                newName: "MaterialId");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Materiales",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Materiales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoriasMaterialesCategoriaId",
                table: "Materiales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CodigoMaterial",
                table: "Materiales",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CreadoPor",
                table: "Materiales",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Materiales",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Materiales",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModificadoPor",
                table: "Materiales",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SistemaMedicion",
                table: "Materiales",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CategoriasMateriales",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasMateriales", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    MarcaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.MarcaId);
                });

            migrationBuilder.CreateTable(
                name: "Modulos",
                columns: table => new
                {
                    ModuloId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuloCodigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NombreModulo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecurityStampo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulos", x => x.ModuloId);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    ProveedorCifId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DomicilioSocial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.ProveedorCifId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RolId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreRol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RolId);
                });

            migrationBuilder.CreateTable(
                name: "TiposPermiso",
                columns: table => new
                {
                    TipoPermisoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombrePermiso = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposPermiso", x => x.TipoPermisoId);
                });

            migrationBuilder.CreateTable(
                name: "Contactos",
                columns: table => new
                {
                    ContactoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProveedorCifId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProveedoresProveedorCifId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contactos", x => x.ContactoId);
                    table.ForeignKey(
                        name: "FK_Contactos_Proveedores_ProveedoresProveedorCifId",
                        column: x => x.ProveedoresProveedorCifId,
                        principalTable: "Proveedores",
                        principalColumn: "ProveedorCifId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProveedoresMarcas",
                columns: table => new
                {
                    ProveedorCifId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    MarcaId = table.Column<int>(type: "int", nullable: false),
                    ProveedorMarcaId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProveedoresMarcas", x => new { x.ProveedorCifId, x.MarcaId });
                    table.ForeignKey(
                        name: "FK_ProveedoresMarcas_Marcas_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marcas",
                        principalColumn: "MarcaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProveedoresMarcas_Proveedores_ProveedorCifId",
                        column: x => x.ProveedorCifId,
                        principalTable: "Proveedores",
                        principalColumn: "ProveedorCifId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ContraseñaHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "RolId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolesModulos",
                columns: table => new
                {
                    RolId = table.Column<int>(type: "int", nullable: false),
                    ModuloId = table.Column<int>(type: "int", nullable: false),
                    TipoPermisoId = table.Column<int>(type: "int", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesModulos", x => new { x.RolId, x.ModuloId, x.TipoPermisoId });
                    table.ForeignKey(
                        name: "FK_RolesModulos_Modulos_ModuloId",
                        column: x => x.ModuloId,
                        principalTable: "Modulos",
                        principalColumn: "ModuloId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolesModulos_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "RolId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolesModulos_TiposPermiso_TipoPermisoId",
                        column: x => x.TipoPermisoId,
                        principalTable: "TiposPermiso",
                        principalColumn: "TipoPermisoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreciosTarifas",
                columns: table => new
                {
                    PrecioTarifaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    ProveedorMarcaId = table.Column<int>(type: "int", nullable: false),
                    ProveedoresMarcasProveedorCifId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ProveedoresMarcasMarcaId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<int>(type: "int", nullable: true),
                    ModificadoPor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreciosTarifas", x => x.PrecioTarifaId);
                    table.ForeignKey(
                        name: "FK_PreciosTarifas_Materiales_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materiales",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreciosTarifas_ProveedoresMarcas_ProveedoresMarcasProveedorCifId_ProveedoresMarcasMarcaId",
                        columns: x => new { x.ProveedoresMarcasProveedorCifId, x.ProveedoresMarcasMarcaId },
                        principalTable: "ProveedoresMarcas",
                        principalColumns: new[] { "ProveedorCifId", "MarcaId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Materiales_CategoriasMaterialesCategoriaId",
                table: "Materiales",
                column: "CategoriasMaterialesCategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_ProveedoresProveedorCifId",
                table: "Contactos",
                column: "ProveedoresProveedorCifId");

            migrationBuilder.CreateIndex(
                name: "IX_PreciosTarifas_MaterialId",
                table: "PreciosTarifas",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_PreciosTarifas_ProveedoresMarcasProveedorCifId_ProveedoresMarcasMarcaId",
                table: "PreciosTarifas",
                columns: new[] { "ProveedoresMarcasProveedorCifId", "ProveedoresMarcasMarcaId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProveedoresMarcas_MarcaId",
                table: "ProveedoresMarcas",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_RolesModulos_ModuloId",
                table: "RolesModulos",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_RolesModulos_TipoPermisoId",
                table: "RolesModulos",
                column: "TipoPermisoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RolId",
                table: "Usuarios",
                column: "RolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materiales_CategoriasMateriales_CategoriasMaterialesCategoriaId",
                table: "Materiales",
                column: "CategoriasMaterialesCategoriaId",
                principalTable: "CategoriasMateriales",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materiales_CategoriasMateriales_CategoriasMaterialesCategoriaId",
                table: "Materiales");

            migrationBuilder.DropTable(
                name: "CategoriasMateriales");

            migrationBuilder.DropTable(
                name: "Contactos");

            migrationBuilder.DropTable(
                name: "PreciosTarifas");

            migrationBuilder.DropTable(
                name: "RolesModulos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "ProveedoresMarcas");

            migrationBuilder.DropTable(
                name: "Modulos");

            migrationBuilder.DropTable(
                name: "TiposPermiso");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropIndex(
                name: "IX_Materiales_CategoriasMaterialesCategoriaId",
                table: "Materiales");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Materiales");

            migrationBuilder.DropColumn(
                name: "CategoriasMaterialesCategoriaId",
                table: "Materiales");

            migrationBuilder.DropColumn(
                name: "CodigoMaterial",
                table: "Materiales");

            migrationBuilder.DropColumn(
                name: "CreadoPor",
                table: "Materiales");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Materiales");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Materiales");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "Materiales");

            migrationBuilder.DropColumn(
                name: "SistemaMedicion",
                table: "Materiales");

            migrationBuilder.RenameColumn(
                name: "MaterialId",
                table: "Materiales",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Materiales",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<decimal>(
                name: "Precio",
                table: "Materiales",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
