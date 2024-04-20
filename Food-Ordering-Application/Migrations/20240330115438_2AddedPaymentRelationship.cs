using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Food_Ordering_Application.Migrations
{
    /// <inheritdoc />
    public partial class _2AddedPaymentRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1b088809-ab49-496d-b7de-074a1d3fb508", "1", "Admin", "Admin" },
                    { "3860c18c-38a2-4a64-a1d7-bf51f07cad29", "3", "Owner", "Owner" },
                    { "b6d0ee8e-0857-4ba0-80b0-aa576965cdbd", "2", "Customer", "Customer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentId",
                table: "Orders",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Payments_PaymentId",
                table: "Orders",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "PaymentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Payments_PaymentId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PaymentId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1b088809-ab49-496d-b7de-074a1d3fb508");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3860c18c-38a2-4a64-a1d7-bf51f07cad29");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b6d0ee8e-0857-4ba0-80b0-aa576965cdbd");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Orders");

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
    }
}
