using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace talabat.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class modefiynameofDescriptioninDeliveryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Describtion",
                table: "DeliveryMethods",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "DeliveryMethods",
                newName: "Describtion");
        }
    }
}
