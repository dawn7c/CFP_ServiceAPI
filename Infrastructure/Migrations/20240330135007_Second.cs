using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Author_BidIdId",
                table: "Bids");

            migrationBuilder.RenameColumn(
                name: "BidIdId",
                table: "Bids",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_BidIdId",
                table: "Bids",
                newName: "IX_Bids_AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Author_AuthorId",
                table: "Bids",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Author_AuthorId",
                table: "Bids");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Bids",
                newName: "BidIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_AuthorId",
                table: "Bids",
                newName: "IX_Bids_BidIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Author_BidIdId",
                table: "Bids",
                column: "BidIdId",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
