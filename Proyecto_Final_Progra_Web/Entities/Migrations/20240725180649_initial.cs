using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cartas",
                columns: table => new
                {
                    carta_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    puntos_ataque = table.Column<int>(type: "int", nullable: true),
                    puntos_defensa = table.Column<int>(type: "int", nullable: true),
                    creado_en = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cartas__D8704F7BE1CB01CC", x => x.carta_id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    usuario_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_usuario = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    contrasena_hash = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    rol = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    creado_en = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__usuarios__2ED7D2AFA4D7C62B", x => x.usuario_id);
                });

            migrationBuilder.CreateTable(
                name: "mazos",
                columns: table => new
                {
                    mazo_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuario_id = table.Column<int>(type: "int", nullable: true),
                    nombre_mazo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    creado_en = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__mazos__80DC12C788FBDCDD", x => x.mazo_id);
                    table.ForeignKey(
                        name: "FK__mazos__usuario_i__3E52440B",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "usuario_id");
                });

            migrationBuilder.CreateTable(
                name: "cartas_en_mazo",
                columns: table => new
                {
                    mazo_id = table.Column<int>(type: "int", nullable: false),
                    carta_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cartas_e__2D5B1630D4735FA7", x => new { x.mazo_id, x.carta_id });
                    table.ForeignKey(
                        name: "FK__cartas_en__carta__4316F928",
                        column: x => x.carta_id,
                        principalTable: "cartas",
                        principalColumn: "carta_id");
                    table.ForeignKey(
                        name: "FK__cartas_en__mazo___4222D4EF",
                        column: x => x.mazo_id,
                        principalTable: "mazos",
                        principalColumn: "mazo_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_cartas_en_mazo_carta_id",
                table: "cartas_en_mazo",
                column: "carta_id");

            migrationBuilder.CreateIndex(
                name: "IX_mazos_usuario_id",
                table: "mazos",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "UQ__usuarios__D4D22D74D19F414C",
                table: "usuarios",
                column: "nombre_usuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cartas_en_mazo");

            migrationBuilder.DropTable(
                name: "cartas");

            migrationBuilder.DropTable(
                name: "mazos");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
