using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class added_WishListInfrastructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "ShowPrice",
                table: "Catalog",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateTable(
                name: "Wishers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WisherId = table.Column<string>(maxLength: 40, nullable: false),
                    WishName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WishItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WishId = table.Column<string>(nullable: true),
                    WishDate = table.Column<DateTimeOffset>(nullable: false),
                    NotifyCasePriceChanges = table.Column<bool>(nullable: false),
                    NotifyWhenAvailable = table.Column<bool>(nullable: false),
                    NotifyChoice = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    CatalogItemId = table.Column<int>(nullable: false),
                    WishListId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishItems_Wishers_WishListId",
                        column: x => x.WishListId,
                        principalTable: "Wishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WishItems_WishListId",
                table: "WishItems",
                column: "WishListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WishItems");

            migrationBuilder.DropTable(
                name: "Wishers");

            migrationBuilder.AlterColumn<bool>(
                name: "ShowPrice",
                table: "Catalog",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: true);
        }
    }
}
