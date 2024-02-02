using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerceServer.Migrations
{
    /// <inheritdoc />
    public partial class Add_CartItems_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarItems",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarItems", x => new { x.UserId, x.ProductId, x.ProductTypeId });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarItems");
        }
    }
}
