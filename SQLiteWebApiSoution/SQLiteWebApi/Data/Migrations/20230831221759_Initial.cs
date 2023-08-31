using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SQLiteWebApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "CreatedDate", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { "17a95541-1e3d-4dc9-bea9-729da6dd67a3", "2023-08-31 17:00:00", "brey@email.com", "Ben", "Ray" },
                    { "204a8fbd-443b-42e0-9985-79164a78a85f", "2023-08-31 17:00:00", "sfox@email.com", "Sue", "Fox" },
                    { "7f5cf754-966d-4094-89d7-759daab61074", "2023-08-31 17:00:00", "jsun@email.com", "Joe", "Sun" },
                    { "9214c650-818b-4066-bd9c-ae3f4669c2b7", "2023-08-31 17:00:00", "afay@email.com", "Ann", "Fay" },
                    { "e9e16361-afd8-4f48-ad45-9cb6ad51fb60", "2023-08-31 17:00:00", "tmax@email.com", "Tom", "Max" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUser");
        }
    }
}
