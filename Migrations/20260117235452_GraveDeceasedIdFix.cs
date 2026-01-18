using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cmentarz.Migrations
{
    /// <inheritdoc />
    public partial class GraveDeceasedIdFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeceasedId",
                table: "Graves",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeceasedId",
                table: "Graves");
        }
    }
}
