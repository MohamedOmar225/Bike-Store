using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bikestore2.Migrations
{
    /// <inheritdoc />
    public partial class BikeStore2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    brandid = table.Column<int>(name: "brand_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    brandname = table.Column<string>(name: "brand_name", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.brandid);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    cateid = table.Column<int>(name: "cate_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    catename = table.Column<string>(name: "cate_name", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.cateid);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    storeid = table.Column<int>(name: "store_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    storename = table.Column<string>(name: "store_name", type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    street = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.storeid);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    productid = table.Column<int>(name: "product_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productname = table.Column<string>(name: "product_name", type: "nvarchar(max)", nullable: false),
                    modelyear = table.Column<string>(name: "model_year", type: "nvarchar(max)", nullable: true),
                    listprice = table.Column<decimal>(name: "list_price", type: "decimal(18,2)", nullable: true),
                    brandid = table.Column<int>(name: "brand_id", type: "int", nullable: false),
                    cateid = table.Column<int>(name: "cate_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.productid);
                    table.ForeignKey(
                        name: "FK_Products_Brands_brand_id",
                        column: x => x.brandid,
                        principalTable: "Brands",
                        principalColumn: "brand_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Categories_cate_id",
                        column: x => x.cateid,
                        principalTable: "Categories",
                        principalColumn: "cate_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Empid = table.Column<int>(name: "Emp_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Empname = table.Column<string>(name: "Emp_name", type: "nvarchar(max)", nullable: false),
                    EmpEmail = table.Column<string>(name: "Emp_Email", type: "nvarchar(max)", nullable: true),
                    Empphone = table.Column<string>(name: "Emp_phone", type: "nvarchar(max)", nullable: true),
                    Empsalary = table.Column<decimal>(name: "Emp_salary", type: "decimal(18,2)", nullable: true),
                    Storeid = table.Column<int>(name: "Store_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Empid);
                    table.ForeignKey(
                        name: "FK_Employees_Stores_Store_id",
                        column: x => x.Storeid,
                        principalTable: "Stores",
                        principalColumn: "store_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Store_id",
                table: "Employees",
                column: "Store_id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_brand_id",
                table: "Products",
                column: "brand_id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_cate_id",
                table: "Products",
                column: "cate_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
