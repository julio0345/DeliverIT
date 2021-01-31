using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContasPagar.Migrations
{
    public partial class VersaoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContaPagar",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    ValorOriginal = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "datetime", nullable: false),
                    DiasAtraso = table.Column<int>(type: "int", nullable: false),
                    ValorCorrigido = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaPagar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regras",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DiasMinimoAtraso = table.Column<int>(nullable: false),
                    MultaPercentual = table.Column<decimal>(type: "numeric(6,3)", nullable: false),
                    JurosDiarioPercentual = table.Column<decimal>(type: "numeric(6,3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regras", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContaPagar");

            migrationBuilder.DropTable(
                name: "Regras");
        }
    }
}
