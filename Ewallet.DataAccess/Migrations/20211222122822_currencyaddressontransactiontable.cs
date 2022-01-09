using Microsoft.EntityFrameworkCore.Migrations;

namespace Ewallet.DataAccess.Migrations
{
    public partial class currencyaddressontransactiontable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CurrencyShortCode",
                table: "Transactions",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WalletCurrencyId",
                table: "Transactions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WalletCurrencyId",
                table: "Transactions",
                column: "WalletCurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_WalletCurrency_WalletCurrencyId",
                table: "Transactions",
                column: "WalletCurrencyId",
                principalTable: "WalletCurrency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_WalletCurrency_WalletCurrencyId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_WalletCurrencyId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "WalletCurrencyId",
                table: "Transactions");

            migrationBuilder.AlterColumn<string>(
                name: "CurrencyShortCode",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
