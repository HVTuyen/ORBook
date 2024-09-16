using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ORBook.Migrations
{
    /// <inheritdoc />
    public partial class update_notification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "Notification");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Notification",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
