using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cmentarz.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueGraveLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Graves_Location",
                table: "Graves",
                column: "Location",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Graves_Location",
                table: "Graves");
        }
    }
}
