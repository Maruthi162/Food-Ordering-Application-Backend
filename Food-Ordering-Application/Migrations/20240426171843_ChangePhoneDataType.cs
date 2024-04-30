using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Food_Ordering_Application.Migrations
{
    /// <inheritdoc />
    public partial class ChangePhoneDataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "493048dd-071e-4fb2-9b46-b3bdd03242fc", "2", "Customer", "Customer" },
                    { "7aa9a0fa-bedc-4c58-8bb2-5992698ec695", "3", "Owner", "Owner" },
                    { "b5888635-170c-49d3-887f-f59a18ad943c", "1", "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "493048dd-071e-4fb2-9b46-b3bdd03242fc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7aa9a0fa-bedc-4c58-8bb2-5992698ec695");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5888635-170c-49d3-887f-f59a18ad943c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1b088809-ab49-496d-b7de-074a1d3fb508", "1", "Admin", "Admin" },
                    { "3860c18c-38a2-4a64-a1d7-bf51f07cad29", "3", "Owner", "Owner" },
                    { "b6d0ee8e-0857-4ba0-80b0-aa576965cdbd", "2", "Customer", "Customer" }
                });
        }
    }
}
