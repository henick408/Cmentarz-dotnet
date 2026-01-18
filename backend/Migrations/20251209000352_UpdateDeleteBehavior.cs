using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cmentarz.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deceaseds_Graves_GraveId",
                table: "Deceaseds");

            migrationBuilder.AddForeignKey(
                name: "FK_Deceaseds_Graves_GraveId",
                table: "Deceaseds",
                column: "GraveId",
                principalTable: "Graves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deceaseds_Graves_GraveId",
                table: "Deceaseds");

            migrationBuilder.AddForeignKey(
                name: "FK_Deceaseds_Graves_GraveId",
                table: "Deceaseds",
                column: "GraveId",
                principalTable: "Graves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
