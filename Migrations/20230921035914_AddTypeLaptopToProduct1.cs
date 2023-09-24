using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LapTopShop.Migrations
{
    /// <inheritdoc />
    public partial class AddTypeLaptopToProduct1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TypeLaptop",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeLaptop",
                table: "Products");
        }
    }
}
