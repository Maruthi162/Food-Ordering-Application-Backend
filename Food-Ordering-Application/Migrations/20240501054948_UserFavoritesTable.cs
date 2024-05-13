using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Food_Ordering_Application.Migrations
{
    /// <inheritdoc />
    public partial class UserFavoritesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "UserFavoriteMenuItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    UserFavoriteMenuItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavoriteMenuItems", x => new { x.Id, x.MenuItemId });
                    table.ForeignKey(
                        name: "FK_UserFavoriteMenuItems_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavoriteMenuItems_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "MenuItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFavoriteRestaurants",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    userFavoriteRestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavoriteRestaurants", x => new { x.Id, x.RestaurantId });
                    table.ForeignKey(
                        name: "FK_UserFavoriteRestaurants_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavoriteRestaurants_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1a412c5e-9c2f-4c2b-84f9-3eed2c48e305", "2", "Customer", "Customer" },
                    { "6e648f43-6f7c-4029-9872-029f49c36f3b", "1", "Admin", "Admin" },
                    { "df7dd7a1-5e68-4ac6-b53c-3925709a8704", "3", "Owner", "Owner" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteMenuItems_MenuItemId",
                table: "UserFavoriteMenuItems",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteRestaurants_RestaurantId",
                table: "UserFavoriteRestaurants",
                column: "RestaurantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFavoriteMenuItems");

            migrationBuilder.DropTable(
                name: "UserFavoriteRestaurants");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a412c5e-9c2f-4c2b-84f9-3eed2c48e305");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e648f43-6f7c-4029-9872-029f49c36f3b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df7dd7a1-5e68-4ac6-b53c-3925709a8704");

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
    }
}
