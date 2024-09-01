using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Food_Ordering_Application.Migrations
{
    /// <inheritdoc />
    public partial class removingaddressrelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Addresses_AddressId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_AddressId",
                table: "Restaurants");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44971c87-e3e0-402f-ac34-9584cdc87e9d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53527bf1-8a9e-4a8c-b5c5-32589a538dea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "549907ab-9aee-4c06-974a-ed62f560f1d4");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
            name: "RestaurantId",
            table: "Addresses");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "baf00dcb-5c36-4f7e-933a-b907e1fc9570", "2", "Customer", "Customer" },
                    { "d19d5dc7-7986-46b6-be13-d4c67f65380c", "3", "Owner", "Owner" },
                    { "d9308177-1748-4f27-a363-638d5d017091", "1", "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "baf00dcb-5c36-4f7e-933a-b907e1fc9570");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d19d5dc7-7986-46b6-be13-d4c67f65380c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9308177-1748-4f27-a363-638d5d017091");

            migrationBuilder.AddColumn<int>(
            name: "RestaurantId",
            table: "Addresses",
            type: "int",
            nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Restaurants",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "44971c87-e3e0-402f-ac34-9584cdc87e9d", "3", "Owner", "Owner" },
                    { "53527bf1-8a9e-4a8c-b5c5-32589a538dea", "2", "Customer", "Customer" },
                    { "549907ab-9aee-4c06-974a-ed62f560f1d4", "1", "Admin", "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_AddressId",
                table: "Restaurants",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Addresses_AddressId",
                table: "Restaurants",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId");
        }
    }
}
