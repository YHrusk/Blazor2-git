using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PptNemocnice.api.Migrations
{
    public partial class pridanikodupojistoven : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Kod",
                table: "Ukons",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Ukons",
                keyColumn: "Id",
                keyValue: new Guid("ab501c28-10e9-4a04-bcd1-91cbececad23"),
                column: "Kod",
                value: "205");

            migrationBuilder.UpdateData(
                table: "Ukons",
                keyColumn: "Id",
                keyValue: new Guid("cb661c28-10e9-4a04-bcd1-91cbececad23"),
                column: "Kod",
                value: "209");

            migrationBuilder.UpdateData(
                table: "Ukons",
                keyColumn: "Id",
                keyValue: new Guid("cc771c28-10e9-4a04-bcd1-91cbececad23"),
                column: "Kod",
                value: "211");

            migrationBuilder.UpdateData(
                table: "Ukons",
                keyColumn: "Id",
                keyValue: new Guid("dd881c28-10e9-4a04-bcd1-91cbececad23"),
                column: "Kod",
                value: "111");

            migrationBuilder.UpdateData(
                table: "Ukons",
                keyColumn: "Id",
                keyValue: new Guid("ee991c28-10e9-4a04-bcd1-91cbececad23"),
                column: "Kod",
                value: "213");

            migrationBuilder.UpdateData(
                table: "Ukons",
                keyColumn: "Id",
                keyValue: new Guid("ff001c28-10e9-4a04-bcd1-91cbececad23"),
                column: "Kod",
                value: "201");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kod",
                table: "Ukons");
        }
    }
}
