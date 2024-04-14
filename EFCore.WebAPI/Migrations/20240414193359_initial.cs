using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Batalha",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DtInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtFim = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batalha", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Heroi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heroi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Arma",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeroiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arma_Heroi_HeroiId",
                        column: x => x.HeroiId,
                        principalTable: "Heroi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HeroiBatalha",
                columns: table => new
                {
                    HeroiId = table.Column<int>(type: "int", nullable: false),
                    BatalhaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroiBatalha", x => new { x.BatalhaId, x.HeroiId });
                    table.ForeignKey(
                        name: "FK_HeroiBatalha_Batalha_BatalhaId",
                        column: x => x.BatalhaId,
                        principalTable: "Batalha",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeroiBatalha_Heroi_HeroiId",
                        column: x => x.HeroiId,
                        principalTable: "Heroi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentidadeSecreta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeReal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeroiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentidadeSecreta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentidadeSecreta_Heroi_HeroiId",
                        column: x => x.HeroiId,
                        principalTable: "Heroi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arma_HeroiId",
                table: "Arma",
                column: "HeroiId");

            migrationBuilder.CreateIndex(
                name: "IX_HeroiBatalha_HeroiId",
                table: "HeroiBatalha",
                column: "HeroiId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentidadeSecreta_HeroiId",
                table: "IdentidadeSecreta",
                column: "HeroiId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arma");

            migrationBuilder.DropTable(
                name: "HeroiBatalha");

            migrationBuilder.DropTable(
                name: "IdentidadeSecreta");

            migrationBuilder.DropTable(
                name: "Batalha");

            migrationBuilder.DropTable(
                name: "Heroi");
        }
    }
}
