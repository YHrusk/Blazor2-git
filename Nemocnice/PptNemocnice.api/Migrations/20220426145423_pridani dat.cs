using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PptNemocnice.api.Migrations
{
    public partial class pridanidat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Vybavenis",
                columns: new[] { "Id", "BoughtDate", "Name", "Price" },
                values: new object[] { new Guid("39d4018a-ba22-4ab6-b582-0d1c35e79e45"), new DateTime(2020, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "JQW", 999 });

            migrationBuilder.InsertData(
                table: "Vybavenis",
                columns: new[] { "Id", "BoughtDate", "Name", "Price" },
                values: new object[] { new Guid("5a3ed44c-ad9a-4b9d-a1ea-b0deabeb815a"), new DateTime(2018, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "XXX", 555 });

            migrationBuilder.InsertData(
                table: "Vybavenis",
                columns: new[] { "Id", "BoughtDate", "Name", "Price" },
                values: new object[] { new Guid("b88f888d-b16a-4ffc-9bd5-7f6f5cb607b9"), new DateTime(2018, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "AZU", 555 });

            migrationBuilder.InsertData(
                table: "Revizes",
                columns: new[] { "Id", "DateTime", "Name", "VybaveniId" },
                values: new object[] { new Guid("01631c28-10e9-4a04-bcd1-91cbececad23"), new DateTime(2019, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "AAA", new Guid("5a3ed44c-ad9a-4b9d-a1ea-b0deabeb815a") });

            migrationBuilder.InsertData(
                table: "Revizes",
                columns: new[] { "Id", "DateTime", "Name", "VybaveniId" },
                values: new object[] { new Guid("aa7a1c28-10e9-4a04-bcd1-91cbececad23"), new DateTime(2020, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "BBB", new Guid("5a3ed44c-ad9a-4b9d-a1ea-b0deabeb815a") });

            migrationBuilder.InsertData(
                table: "Revizes",
                columns: new[] { "Id", "DateTime", "Name", "VybaveniId" },
                values: new object[] { new Guid("bb0b1c28-10e9-4a04-bcd1-91cbececad23"), new DateTime(1999, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "CCC", new Guid("5a3ed44c-ad9a-4b9d-a1ea-b0deabeb815a") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Revizes",
                keyColumn: "Id",
                keyValue: new Guid("01631c28-10e9-4a04-bcd1-91cbececad23"));

            migrationBuilder.DeleteData(
                table: "Revizes",
                keyColumn: "Id",
                keyValue: new Guid("aa7a1c28-10e9-4a04-bcd1-91cbececad23"));

            migrationBuilder.DeleteData(
                table: "Revizes",
                keyColumn: "Id",
                keyValue: new Guid("bb0b1c28-10e9-4a04-bcd1-91cbececad23"));

            migrationBuilder.DeleteData(
                table: "Vybavenis",
                keyColumn: "Id",
                keyValue: new Guid("39d4018a-ba22-4ab6-b582-0d1c35e79e45"));

            migrationBuilder.DeleteData(
                table: "Vybavenis",
                keyColumn: "Id",
                keyValue: new Guid("b88f888d-b16a-4ffc-9bd5-7f6f5cb607b9"));

            migrationBuilder.DeleteData(
                table: "Vybavenis",
                keyColumn: "Id",
                keyValue: new Guid("5a3ed44c-ad9a-4b9d-a1ea-b0deabeb815a"));
        }
    }
}
