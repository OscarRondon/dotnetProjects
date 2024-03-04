using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerceServer.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProductandProductVariantpropertyname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Visble",
                table: "ProductVariants",
                newName: "Visible");

            migrationBuilder.RenameColumn(
                name: "Visble",
                table: "Products",
                newName: "Visible");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Visible",
                table: "ProductVariants",
                newName: "Visble");

            migrationBuilder.RenameColumn(
                name: "Visible",
                table: "Products",
                newName: "Visble");
        }
    }
}
