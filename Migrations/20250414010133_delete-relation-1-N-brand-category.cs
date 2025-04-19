using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bikestore2.Migrations
{
    /// <inheritdoc />
    public partial class deleterelation1Nbrandcategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Brands_BrandId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_BrandId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Categories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_BrandId",
                table: "Categories",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Brands_BrandId",
                table: "Categories",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
