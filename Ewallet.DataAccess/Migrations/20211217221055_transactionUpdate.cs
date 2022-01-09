using Microsoft.EntityFrameworkCore.Migrations;

namespace Ewallet.DataAccess.Migrations
{
    public partial class transactionUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallet_WalletIdId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_WalletIdId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "WalletIdId",
                table: "Transactions");

            migrationBuilder.AddColumn<string>(
                name: "WalletId",
                table: "Transactions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WalletId",
                table: "Transactions",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallet_WalletId",
                table: "Transactions",
                column: "WalletId",
                principalTable: "Wallet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallet_WalletId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_WalletId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Transactions");

            migrationBuilder.AddColumn<string>(
                name: "WalletIdId",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WalletIdId",
                table: "Transactions",
                column: "WalletIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallet_WalletIdId",
                table: "Transactions",
                column: "WalletIdId",
                principalTable: "Wallet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
