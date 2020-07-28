using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SearchDataApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QueryResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Keyword = table.Column<string>(nullable: false),
                    SearchEngineId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    EnteredDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueryResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SearchEngines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchEngines", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "SearchEngines",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Google" });

            migrationBuilder.InsertData(
                table: "SearchEngines",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Bing" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QueryResults");

            migrationBuilder.DropTable(
                name: "SearchEngines");
        }
    }
}
