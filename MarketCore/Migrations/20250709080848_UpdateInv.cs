using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketCore.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetails_InvoiceHeaders_InoiceID",
                table: "InvoiceDetails");

            migrationBuilder.RenameColumn(
                name: "InoiceID",
                table: "InvoiceDetails",
                newName: "InvoiceID");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceDetails_InoiceID",
                table: "InvoiceDetails",
                newName: "IX_InvoiceDetails_InvoiceID");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "CustomersAndVendors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_InvoiceHeaders_InvoiceID",
                table: "InvoiceDetails",
                column: "InvoiceID",
                principalTable: "InvoiceHeaders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetails_InvoiceHeaders_InvoiceID",
                table: "InvoiceDetails");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "CustomersAndVendors");

            migrationBuilder.RenameColumn(
                name: "InvoiceID",
                table: "InvoiceDetails",
                newName: "InoiceID");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceDetails_InvoiceID",
                table: "InvoiceDetails",
                newName: "IX_InvoiceDetails_InoiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_InvoiceHeaders_InoiceID",
                table: "InvoiceDetails",
                column: "InoiceID",
                principalTable: "InvoiceHeaders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
