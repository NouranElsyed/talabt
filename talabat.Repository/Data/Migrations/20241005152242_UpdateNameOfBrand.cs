using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace talabat.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNameOfBrand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BrandsId",
                table: "Products",
                newName: "BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BrandsId",
                table: "Products",
                newName: "IX_Products_BrandId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "Products",
                newName: "BrandsId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                newName: "IX_Products_BrandsId");
        }
    }
}
