using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Food_Ordering_Application.Migrations
{
    /// <inheritdoc />
    public partial class FavoriteItemsRestCats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNum",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b9ea5b56-3f0c-4439-a2a9-4004487eaab2", "2", "Customer", "Customer" },
                    { "cd6a49b1-b1e0-48b9-9c7a-41ae2bbf52ed", "1", "Admin", "Admin" },
                    { "e8da1601-e562-46e0-8e66-aaf2a66a399a", "3", "Owner", "Owner" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9ea5b56-3f0c-4439-a2a9-4004487eaab2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd6a49b1-b1e0-48b9-9c7a-41ae2bbf52ed");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8da1601-e562-46e0-8e66-aaf2a66a399a");

            migrationBuilder.AlterColumn<int>(
                name: "PhoneNum",
                table: "Restaurants",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
