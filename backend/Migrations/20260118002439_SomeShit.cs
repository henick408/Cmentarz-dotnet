using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cmentarz.Migrations
{
    /// <inheritdoc />
    public partial class SomeShit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deceaseds_Graves_GraveId",
                table: "Deceaseds");

            migrationBuilder.DropIndex(
                name: "IX_Deceaseds_GraveId",
                table: "Deceaseds");

            migrationBuilder.DropColumn(
                name: "GraveId",
                table: "Deceaseds");

            migrationBuilder.CreateIndex(
                name: "IX_Graves_DeceasedId",
                table: "Graves",
                column: "DeceasedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Graves_Deceaseds_DeceasedId",
                table: "Graves",
                column: "DeceasedId",
                principalTable: "Deceaseds",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Graves_Deceaseds_DeceasedId",
                table: "Graves");

            migrationBuilder.DropIndex(
                name: "IX_Graves_DeceasedId",
                table: "Graves");

            migrationBuilder.AddColumn<int>(
                name: "GraveId",
                table: "Deceaseds",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Deceaseds_GraveId",
                table: "Deceaseds",
                column: "GraveId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Deceaseds_Graves_GraveId",
                table: "Deceaseds",
                column: "GraveId",
                principalTable: "Graves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
