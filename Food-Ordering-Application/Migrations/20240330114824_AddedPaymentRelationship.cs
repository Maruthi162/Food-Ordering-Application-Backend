using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Food_Ordering_Application.Migrations
{
    /// <inheritdoc />
    public partial class AddedPaymentRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "05910d79-7f4b-446e-94c3-973a363e747d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f0fc39e-953f-4aee-b304-0c81dc0e19d5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a43b6f5a-25a9-432b-bcb0-ba37ed1a4123");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4e2c1b2b-c2bc-429f-9ea4-27f6f6256abd", "3", "Owner", "Owner" },
                    { "8e1a0b49-c44b-4c6c-843a-ab87b8bef074", "2", "Customer", "Customer" },
                    { "d9d6ead7-8adf-40e1-9196-1c0ee488c9b5", "1", "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e2c1b2b-c2bc-429f-9ea4-27f6f6256abd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8e1a0b49-c44b-4c6c-843a-ab87b8bef074");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9d6ead7-8adf-40e1-9196-1c0ee488c9b5");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "05910d79-7f4b-446e-94c3-973a363e747d", "3", "Owner", "Owner" },
                    { "9f0fc39e-953f-4aee-b304-0c81dc0e19d5", "1", "Admin", "Admin" },
                    { "a43b6f5a-25a9-432b-bcb0-ba37ed1a4123", "2", "Customer", "Customer" }
                });
        }
    }
}
