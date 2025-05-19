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
                    { 8, "Ruta 85 s/n, Toledo", "Sucursal Toledo – Canelones", -34.7000m, -56.0500m },
                    { 9, "Av. Millan s/n, Salinas", "Sucursal Florida – Florida", -34.7050m, -56.0550m },
                    { 10, "Margarita Martínez de Fariña s/n, San Bautista", "Sucursal San Antonio – Canelones", -33.7000m, -57.0500m }
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
                    { 6, "Admin", "Luis", "Martínez", "admin123", "luis@gmail.com", "099666111" },
                    { 7, "Admin", "Sofía", "Ruiz", "admin123", "sofia@gmail.com", "099222333" },
                    { 8, "Cliente", "Pedro", "Díaz", "cliente123", "pedro@gmail.com", "099333444" },
                    { 9, "Cliente", "Ana", "Fernández", "cliente123", "ana@gmail.com", "099444555" },
                    { 10, "Funcionario", "Martín", "Sosa", "funcionario123", "martin@gmail.com", "099555666" },
                    { 11, "Funcionario", "Laura", "Castro", "funcionario123", "laura@gmail.com", "099888999" },
                    { 12, "Funcionario", "Diego", "Navarro", "funcionario123", "diego@gmail.com", "099999000" }
                });

            migrationBuilder.InsertData(
                table: "Envios",
                columns: new[] { "Id", "ClienteId", "Discriminator", "EmpleadoId", "Estado", "FechaCreacion", "FechaFinalizacion", "LugarRetiroId", "NroTracking", "Peso" },
                values: new object[,]
                {
                    { 1, 3, "Comun", 1, 0, new DateTime(2025, 5, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "TRK0000001", 1.5m },
                    { 2, 4, "Comun", 2, 0, new DateTime(2025, 5, 2, 14, 30, 0, 0, DateTimeKind.Unspecified), null, 2, "TRK0000002", 2.2m },
                    { 3, 3, "Comun", 1, 0, new DateTime(2025, 5, 3, 9, 15, 0, 0, DateTimeKind.Unspecified), null, 1, "TRK0000003", 5.0m },
                    { 4, 4, "Comun", 2, 0, new DateTime(2025, 5, 4, 11, 45, 0, 0, DateTimeKind.Unspecified), null, 3, "TRK0000004", 0.8m }
                });

            migrationBuilder.InsertData(
                table: "Envios",
                columns: new[] { "Id", "NroTracking", "ClienteId", "Discriminator", "EmpleadoId", "Estado", "FechaCreacion", "FechaFinalizacion", "DireccionPostal", "Entregado", "Peso" },
                values: new object[,]
                {
                    { 5, "TRK0000005", 8, "Urgente", 1, 0, new DateTime(2025, 5, 5, 16, 0, 0, 0, DateTimeKind.Unspecified), null, "Calle 1 #100", false, 3.3m },
                    { 6, "TRK0000006", 3, "Urgente", 2, 0, new DateTime(2025, 5, 6, 17, 30, 0, 0, DateTimeKind.Unspecified), null, "Av. 2 #200", false, 10.0m },
                    { 7, "TRK0000007", 8, "Urgente", 1, 0, new DateTime(2025, 5, 7, 18, 45, 0, 0, DateTimeKind.Unspecified), null, "Calle 3 #300", false, 7.7m },
                    { 8, "TRK0000008", 9, "Urgente", 2, 0, new DateTime(2025, 5, 8, 12, 15, 0, 0, DateTimeKind.Unspecified), null, "Av. 4 #400", false, 4.4m }
                });

            migrationBuilder.InsertData(
                table: "Envios",
                columns: new[] { "Id", "ClienteId", "Discriminator", "EmpleadoId", "Estado", "FechaCreacion", "FechaFinalizacion", "LugarRetiroId", "NroTracking", "Peso" },
                values: new object[] { 9, 9, "Comun", 1, 0, new DateTime(2025, 5, 9, 13, 0, 0, 0, DateTimeKind.Unspecified), null, 4, "TRK0000009", 2.0m });

            migrationBuilder.InsertData(
                table: "Envios",
                columns: new[] { "Id", "NroTracking", "ClienteId", "Discriminator", "EmpleadoId", "Estado", "FechaCreacion", "FechaFinalizacion", "DireccionPostal", "Entregado", "Peso" },
                values: new object[] { 10, "TRK0000010", 4, "Urgente", 1, 0, new DateTime(2025, 5, 10, 14, 0, 0, 0, DateTimeKind.Unspecified), null, "Calle 5 #500", false, 6.6m });

            migrationBuilder.InsertData(
                table: "EtapasSeguimiento",
                columns: new[] { "Id", "IdEmpleado", "IdEnvio", "NroTracking", "Comentario", "Fecha" },
                values: new object[,]
                {
                    { 1, 1, 1, "TRK0000001", "Recibido en almacén", new DateTime(2025, 5, 1, 11, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, 1, "TRK0000001", "En tránsito", new DateTime(2025, 5, 1, 13, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, 2, "TRK0000002", "Despachado", new DateTime(2025, 5, 2, 10, 15, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 2, 2, "TRK0000002", "En camino", new DateTime(2025, 5, 2, 15, 45, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, 3, "TRK0000003", "Llegó a destino", new DateTime(2025, 5, 3, 9, 5, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 2, 4, "TRK0000004", "Intento de entrega", new DateTime(2025, 5, 4, 17, 20, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 1, 5, "TRK0000005", "Entregado", new DateTime(2025, 5, 5, 18, 55, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 2, 6, "TRK0000006", "Pendiente retiro", new DateTime(2025, 5, 6, 12, 5, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 1, 7, "TRK0000007", "Reprogramado", new DateTime(2025, 5, 7, 14, 40, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 2, 8, "TRK0000008", "Cancelado por cliente", new DateTime(2025, 5, 8, 16, 25, 0, 0, DateTimeKind.Unspecified) }
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
