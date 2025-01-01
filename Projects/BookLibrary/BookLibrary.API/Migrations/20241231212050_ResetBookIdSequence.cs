using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrary.API.Migrations
{
    /// <inheritdoc />
    public partial class ResetBookIdSequence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DBCC CHECKIDENT ('Books', RESEED, 0)");

            migrationBuilder.Sql("DBCC CHECKIDENT ('Authors', RESEED, 0)");

            migrationBuilder.Sql("DBCC CHECKIDENT ('Borrowings', RESEED, 0)");

            migrationBuilder.Sql("DBCC CHECKIDENT ('Users', RESEED, 0)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
