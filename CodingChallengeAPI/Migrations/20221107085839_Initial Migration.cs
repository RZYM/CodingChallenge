using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingChallengeAPI.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountName = table.Column<string>(type: "TEXT", nullable: false),
                    IBAN = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "TransferTransactions",
                columns: table => new
                {
                    TransactionsId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TransferAmount = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TransferFrom = table.Column<long>(type: "INTEGER", nullable: true),
                    TransferTo = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferTransactions", x => x.TransactionsId);
                });

            migrationBuilder.CreateTable(
                name: "Deposit",
                columns: table => new
                {
                    DepositId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Amount = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    FeeAmount = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AccountId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposit", x => x.DepositId);
                    table.ForeignKey(
                        name: "FK_Deposit_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId");
                });

            migrationBuilder.CreateTable(
                name: "Outstanding",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AccountId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outstanding", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Outstanding_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deposit_AccountId",
                table: "Deposit",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Outstanding_AccountId",
                table: "Outstanding",
                column: "AccountId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deposit");

            migrationBuilder.DropTable(
                name: "Outstanding");

            migrationBuilder.DropTable(
                name: "TransferTransactions");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
