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
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false)
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
                    { 1, "Av. Principal 123", "Agencia San Ramón", -34.9011m, -56.1645m },
                    { 2, "Calle Secundaria 456", "Agencia Montevideo", -34.9050m, -56.1900m }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Discriminator", "Nombre", "Apellido", "Clave", "Correo", "Telefono" },
                values: new object[,]
                {
                    { 1, "Admin", "Juan", "Pérez", "admin123", "juan@gmail.com", "099123456" },
                    { 2, "Admin", "María", "Gómez", "admin123", "maria@gmail.com", "099654321" },
                    { 3, "Cliente", "Carlos", "Rodríguez", "cliente123", "carlitos@gmail.com", "099122555" },
                    { 4, "Cliente", "Fernanda", "López", "cliente123", "fernanda@gmail.com", "099777888" }
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
