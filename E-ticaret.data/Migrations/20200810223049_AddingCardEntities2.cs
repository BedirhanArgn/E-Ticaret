using Microsoft.EntityFrameworkCore.Migrations;

namespace E_ticaret.data.Migrations
{
    public partial class AddingCardEntities2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardItems_Products_ProductId1",
                table: "CardItems");

            migrationBuilder.DropIndex(
                name: "IX_CardItems_ProductId1",
                table: "CardItems");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "CardItems");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "CardItems",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardItems_ProductId",
                table: "CardItems",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardItems_Products_ProductId",
                table: "CardItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardItems_Products_ProductId",
                table: "CardItems");

            migrationBuilder.DropIndex(
                name: "IX_CardItems_ProductId",
                table: "CardItems");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "CardItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "CardItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardItems_ProductId1",
                table: "CardItems",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CardItems_Products_ProductId1",
                table: "CardItems",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
