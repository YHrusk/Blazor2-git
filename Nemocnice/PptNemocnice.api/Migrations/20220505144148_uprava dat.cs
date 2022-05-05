using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PptNemocnice.api.Migrations
{
    public partial class upravadat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Revizes",
                columns: new[] { "Id", "DateTime", "Name", "VybaveniId" },
                values: new object[] { new Guid("cc9b1c28-10e9-4a04-bcd1-91cbececad23"), new DateTime(2018, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "DDD", new Guid("b88f888d-b16a-4ffc-9bd5-7f6f5cb607b9") });

            migrationBuilder.InsertData(
                table: "Ukons",
                columns: new[] { "Id", "DateTime", "Info", "Name", "VybaveniId" },
                values: new object[] { new Guid("ab501c28-10e9-4a04-bcd1-91cbececad23"), new DateTime(2016, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cras id dolor", "VVV", new Guid("b88f888d-b16a-4ffc-9bd5-7f6f5cb607b9") });

            migrationBuilder.InsertData(
                table: "Ukons",
                columns: new[] { "Id", "DateTime", "Info", "Name", "VybaveniId" },
                values: new object[] { new Guid("cb661c28-10e9-4a04-bcd1-91cbececad23"), new DateTime(2016, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mauris dictum", "OOO", new Guid("b88f888d-b16a-4ffc-9bd5-7f6f5cb607b9") });

            migrationBuilder.InsertData(
                table: "Ukons",
                columns: new[] { "Id", "DateTime", "Info", "Name", "VybaveniId" },
                values: new object[] { new Guid("cc771c28-10e9-4a04-bcd1-91cbececad23"), new DateTime(2019, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aliquam ornare", "HHH", new Guid("5a3ed44c-ad9a-4b9d-a1ea-b0deabeb815a") });

            migrationBuilder.InsertData(
                table: "Ukons",
                columns: new[] { "Id", "DateTime", "Info", "Name", "VybaveniId" },
                values: new object[] { new Guid("dd881c28-10e9-4a04-bcd1-91cbececad23"), new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Neque porro", "ZZZ", new Guid("5a3ed44c-ad9a-4b9d-a1ea-b0deabeb815a") });

            migrationBuilder.InsertData(
                table: "Ukons",
                columns: new[] { "Id", "DateTime", "Info", "Name", "VybaveniId" },
                values: new object[] { new Guid("ee991c28-10e9-4a04-bcd1-91cbececad23"), new DateTime(2016, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Duis risus", "MMM", new Guid("5a3ed44c-ad9a-4b9d-a1ea-b0deabeb815a") });

            migrationBuilder.InsertData(
                table: "Ukons",
                columns: new[] { "Id", "DateTime", "Info", "Name", "VybaveniId" },
                values: new object[] { new Guid("ff001c28-10e9-4a04-bcd1-91cbececad23"), new DateTime(2022, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fusce suscipit", "III", new Guid("5a3ed44c-ad9a-4b9d-a1ea-b0deabeb815a") });

            migrationBuilder.UpdateData(
                table: "Vybavenis",
                keyColumn: "Id",
                keyValue: new Guid("b88f888d-b16a-4ffc-9bd5-7f6f5cb607b9"),
                columns: new[] { "BoughtDate", "Price" },
                values: new object[] { new DateTime(2000, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 10888 });

            migrationBuilder.CreateIndex(
                name: "IX_Ukons_VybaveniId",
                table: "Ukons",
                column: "VybaveniId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ukons_Vybavenis_VybaveniId",
                table: "Ukons",
                column: "VybaveniId",
                principalTable: "Vybavenis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ukons_Vybavenis_VybaveniId",
                table: "Ukons");

            migrationBuilder.DropIndex(
                name: "IX_Ukons_VybaveniId",
                table: "Ukons");

            migrationBuilder.DeleteData(
                table: "Revizes",
                keyColumn: "Id",
                keyValue: new Guid("cc9b1c28-10e9-4a04-bcd1-91cbececad23"));

            migrationBuilder.DeleteData(
                table: "Ukons",
                keyColumn: "Id",
                keyValue: new Guid("ab501c28-10e9-4a04-bcd1-91cbececad23"));

            migrationBuilder.DeleteData(
                table: "Ukons",
                keyColumn: "Id",
                keyValue: new Guid("cb661c28-10e9-4a04-bcd1-91cbececad23"));

            migrationBuilder.DeleteData(
                table: "Ukons",
                keyColumn: "Id",
                keyValue: new Guid("cc771c28-10e9-4a04-bcd1-91cbececad23"));

            migrationBuilder.DeleteData(
                table: "Ukons",
                keyColumn: "Id",
                keyValue: new Guid("dd881c28-10e9-4a04-bcd1-91cbececad23"));

            migrationBuilder.DeleteData(
                table: "Ukons",
                keyColumn: "Id",
                keyValue: new Guid("ee991c28-10e9-4a04-bcd1-91cbececad23"));

            migrationBuilder.DeleteData(
                table: "Ukons",
                keyColumn: "Id",
                keyValue: new Guid("ff001c28-10e9-4a04-bcd1-91cbececad23"));

            migrationBuilder.UpdateData(
                table: "Vybavenis",
                keyColumn: "Id",
                keyValue: new Guid("b88f888d-b16a-4ffc-9bd5-7f6f5cb607b9"),
                columns: new[] { "BoughtDate", "Price" },
                values: new object[] { new DateTime(2018, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 555 });
        }
    }
}
