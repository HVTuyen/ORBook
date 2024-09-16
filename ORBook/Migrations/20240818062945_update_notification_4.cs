using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ORBook.Migrations
{
    /// <inheritdoc />
    public partial class update_notification_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Notification",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_BookId",
                table: "Notification",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Book_BookId",
                table: "Notification",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Book_BookId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_BookId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Notification");
        }
    }
}
