using Microsoft.EntityFrameworkCore.Migrations;

namespace ContasPagar.Migrations
{
    public partial class InsertRegras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "Regras",
            columns: new[] { "Id", "DiasMinimoAtraso", "MultaPercentual", "JurosDiarioPercentual" },
            values: new object[,]
            {
                { "B749AB76-B17F-488D-928D-8A9025C4BC79", 1, 2, 0.1 },
                { "EF3D9B51-B237-4EB7-9D0E-40DF9DD303F9", 4, 3, 0.2 },
                { "06CEB4E8-B821-45C2-B007-9DDB2C95AAF8", 6, 5, 0.3 }
        });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
            table: "Regras",
            keyColumn: "Id",
            keyValue: new object[,]
            {
                { "B749AB76-B17F-488D-928D-8A9025C4BC79"},
                { "EF3D9B51-B237-4EB7-9D0E-40DF9DD303F9"},
                { "06CEB4E8-B821-45C2-B007-9DDB2C95AAF8"}
        });
        }
    }
}
