using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bikestore2.Migrations
{
    /// <inheritdoc />
    public partial class addProductStorewithrelationManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductStore",
                columns: table => new
                {
                    Productsproductid = table.Column<int>(name: "Productsproduct_id", type: "int", nullable: false),
                    Storesstoreid = table.Column<int>(name: "Storesstore_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStore", x => new { x.Productsproductid, x.Storesstoreid });
                    table.ForeignKey(
                        name: "FK_ProductStore_Products_Productsproduct_id",
                        column: x => x.Productsproductid,
                        principalTable: "Products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductStore_Stores_Storesstore_id",
                        column: x => x.Storesstoreid,
                        principalTable: "Stores",
                        principalColumn: "store_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductStores",
                columns: table => new
                {
                    productid = table.Column<int>(name: "product_id", type: "int", nullable: false),
                    storeid = table.Column<int>(name: "store_id", type: "int", nullable: false),
                    Quanttity = table.Column<int>(type: "INT", nullable: false),
                    Productsproductid = table.Column<int>(name: "Productsproduct_id", type: "int", nullable: false),
                    Storesstoreid = table.Column<int>(name: "Storesstore_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStores", x => new { x.productid, x.storeid });
                    table.ForeignKey(
                        name: "FK_ProductStores_Products_Productsproduct_id",
                        column: x => x.Productsproductid,
                        principalTable: "Products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductStores_Stores_Storesstore_id",
                        column: x => x.Storesstoreid,
                        principalTable: "Stores",
                        principalColumn: "store_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductStore_Storesstore_id",
                table: "ProductStore",
                column: "Storesstore_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStores_Productsproduct_id",
                table: "ProductStores",
                column: "Productsproduct_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStores_Storesstore_id",
                table: "ProductStores",
                column: "Storesstore_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductStore");

            migrationBuilder.DropTable(
                name: "ProductStores");
        }
    }
}
