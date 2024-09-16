using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ORBook.Migrations
{
    /// <inheritdoc />
    public partial class update_volumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Volumn_Book_BookId",
                table: "Volumn");

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "Volumn",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Volumn_Book_BookId",
                table: "Volumn",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Volumn_Book_BookId",
                table: "Volumn");

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "Volumn",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Volumn_Book_BookId",
                table: "Volumn",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id");
        }
    }
}
