using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PptNemocnice.api.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vybavenis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    BoughtDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastRevisionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Price = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vybavenis", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vybavenis");
        }
    }
}
