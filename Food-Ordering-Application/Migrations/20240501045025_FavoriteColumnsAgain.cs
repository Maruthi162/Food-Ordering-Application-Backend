using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Food_Ordering_Application.Migrations
{
    /// <inheritdoc />
    public partial class FavoriteColumnsAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "Restaurants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4fdd8725-9f75-45ab-9e84-4f26c5cdabcf", "1", "Admin", "Admin" },
                    { "927498c3-d311-48aa-b2e4-016d90764208", "2", "Customer", "Customer" },
                    { "f0a78449-d7ac-4c6a-8267-8892e3bfce9b", "3", "Owner", "Owner" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4fdd8725-9f75-45ab-9e84-4f26c5cdabcf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "927498c3-d311-48aa-b2e4-016d90764208");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f0a78449-d7ac-4c6a-8267-8892e3bfce9b");

            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "Categories");

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
    }
}
