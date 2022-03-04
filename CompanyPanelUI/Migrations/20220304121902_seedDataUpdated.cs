using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyPanelUI.Migrations
{
    public partial class seedDataUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "6d9fe8d0-4791-4625-aa41-01f3ef4d165e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "0fb79537-cc35-4982-9b32-a59217185254");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "269751b8-4095-4c70-8eab-32ae83d940a5");

            migrationBuilder.UpdateData(
                table: "CustomUser",
                keyColumn: "Id",
                keyValue: "22e40406-8a9d-2d82-912c-5d6a640ee696",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "15142b86-2dd6-4e0a-8731-0af709f5c26b", "AQAAAAEAACcQAAAAEBnB8oXphFdmCsywKjHsM1T0Rqoy+MUE/X6BTKXc92U7kCDqn3k8JwfkAyO3GjGfuA==", "G4UWDNIBHRMGKMISDT73JLS7P3EBZMRV" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "041de6a1-8a8c-4fec-877f-bb0b34026188");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "3fee7373-5a1e-4679-a048-0b0a3f30c4c4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "ea7136c8-fadd-4b35-bc07-6c25b904ac91");

            migrationBuilder.UpdateData(
                table: "CustomUser",
                keyColumn: "Id",
                keyValue: "22e40406-8a9d-2d82-912c-5d6a640ee696",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "773338ee-7657-421d-b718-e919e7633ede", "AQAAAAEAACcQAAAAEBaA5EMstOiPliZR3Whk+8FaW5S25TK7r1dN1fjdCLwLNLfAfBSixQKhDMiYadQeOQ==", "O4JPUURPA3NEWQ5IR2RPDHEO7WLZYBWV" });
        }
    }
}
