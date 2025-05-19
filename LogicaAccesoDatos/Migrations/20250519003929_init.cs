using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DireccionPostal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitud = table.Column<decimal>(type: "decimal(9,6)", precision: 9, scale: 6, nullable: false),
                    Longitud = table.Column<decimal>(type: "decimal(9,6)", precision: 9, scale: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Auditorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdResponsable = table.Column<int>(type: "int", nullable: false),
                    IdEmpleado = table.Column<int>(type: "int", nullable: false),
                    Accion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Envios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NroTracking = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    Peso = table.Column<decimal>(type: "decimal(10,3)", precision: 10, scale: 3, nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinalizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LugarRetiroId = table.Column<int>(type: "int", nullable: true),
                    DireccionPostal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Entregado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Envios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Envios_Agencias_LugarRetiroId",
                        column: x => x.LugarRetiroId,
                        principalTable: "Agencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Envios_Usuarios_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Envios_Usuarios_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EtapasSeguimiento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEnvio = table.Column<int>(type: "int", nullable: false),
                    NroTracking = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdEmpleado = table.Column<int>(type: "int", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtapasSeguimiento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EtapasSeguimiento_Envios_IdEnvio",
                        column: x => x.IdEnvio,
                        principalTable: "Envios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EtapasSeguimiento_Usuarios_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Agencias",
                columns: new[] { "Id", "DireccionPostal", "Nombre", "Latitud", "Longitud" },
                values: new object[,]
                {
                    { 1, "Buenos Aires 451, Ciudad Vieja", "Palacio de Correos – Montevideo", -34.9052m, -56.2036m },
                    { 2, "Bulevar Artigas 1825, Terminal Tres Cruces", "Sucursal Tres Cruces – Montevideo", -34.8945m, -56.1748m },
                    { 3, "Av. Ing. Luis Giannattasio Km 21.500", "Sucursal Lagomar – Canelones", -34.8220m, -55.9960m },
                    { 4, "Ruta 8 Km 23.800", "Sucursal Barros Blancos – Canelones", -34.7320m, -56.0000m },
                    { 5, "Ruta Interbalnearia Km 62.800", "Sucursal San Luis – Canelones", -34.7500m, -55.6000m },
                    { 6, "Av. Julieta s/n, Salinas", "Sucursal Salinas – Canelones", -34.7700m, -55.8300m },
                    { 7, "Margarita Martínez de Fariña s/n, San Bautista", "Sucursal San Bautista – Canelones", -34.4500m, -55.9500m },
                    { 8, "Ruta 85 s/n, Toledo", "Sucursal Toledo – Canelones", -34.7000m, -56.0500m }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Discriminator", "Nombre", "Apellido", "Clave", "Correo", "Telefono" },
                values: new object[,]
                {
                    { 1, "Admin", "Juan", "Pérez", "admin123", "juan@gmail.com", "099123456" },
                    { 2, "Admin", "María", "Gómez", "admin123", "maria@gmail.com", "099654321" },
                    { 3, "Cliente", "Carlos", "Rodríguez", "cliente123", "carlitos@gmail.com", "099122555" },
                    { 4, "Cliente", "Fernanda", "López", "cliente123", "fernanda@gmail.com", "099777888" },
                    { 5, "Admin", "Gerente", "Apellido", "Gerente2025.", "administrador@gmail.com", "099100200" },
                    { 6, "Cliente", "Luis", "Martínez", "cliente123", "luis@gmail.com", "099666111" },
                    { 7, "Cliente", "Sofía", "Ruiz", "cliente123", "sofia@gmail.com", "099222333" },
                    { 8, "Cliente", "Pedro", "Díaz", "cliente123", "pedro@gmail.com", "099333444" },
                    { 9, "Cliente", "Ana", "Fernández", "cliente123", "ana@gmail.com", "099444555" },
                    { 10, "Cliente", "Martín", "Sosa", "cliente123", "martin@gmail.com", "099555666" },
                    { 11, "Cliente", "Laura", "Castro", "cliente123", "laura@gmail.com", "099888999" },
                    { 12, "Cliente", "Diego", "Navarro", "cliente123", "diego@gmail.com", "099999000" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Envios_ClienteId",
                table: "Envios",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Envios_EmpleadoId",
                table: "Envios",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Envios_LugarRetiroId",
                table: "Envios",
                column: "LugarRetiroId");

            migrationBuilder.CreateIndex(
                name: "IX_EtapasSeguimiento_IdEmpleado",
                table: "EtapasSeguimiento",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_EtapasSeguimiento_IdEnvio",
                table: "EtapasSeguimiento",
                column: "IdEnvio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auditorias");

            migrationBuilder.DropTable(
                name: "EtapasSeguimiento");

            migrationBuilder.DropTable(
                name: "Envios");

            migrationBuilder.DropTable(
                name: "Agencias");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
