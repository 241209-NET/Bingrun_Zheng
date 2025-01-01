using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrary.API.Migrations
{
    /// <inheritdoc />
    public partial class BookTableChange_Author : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Books",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Books",
                table: "Authors");
        }
    }
}
