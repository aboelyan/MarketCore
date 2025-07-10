using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketCore.Migrations
{
    /// <inheritdoc />
    public partial class upInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ParentID",
                table: "ProductCategories",
                column: "ParentID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_ProductCategories_ParentID",
                table: "ProductCategories",
                column: "ParentID",
                principalTable: "ProductCategories",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_ProductCategories_ParentID",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_ParentID",
                table: "ProductCategories");
        }
    }
}
