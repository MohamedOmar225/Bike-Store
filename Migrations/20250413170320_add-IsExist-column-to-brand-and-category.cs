using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bikestore2.Migrations
{
    /// <inheritdoc />
    public partial class addIsExistcolumntobrandandcategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsExsit",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsExist",
                table: "Brands",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExsit",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IsExist",
                table: "Brands");
        }
    }
}
