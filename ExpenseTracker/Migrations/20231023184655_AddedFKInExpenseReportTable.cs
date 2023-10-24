using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddedFKInExpenseReportTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "ExpenseCategoryId",
                table: "ExpenseReport",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseReport_ExpenseCategoryId",
                table: "ExpenseReport",
                column: "ExpenseCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseReport_ExpenseCategory_ExpenseCategoryId",
                table: "ExpenseReport",
                column: "ExpenseCategoryId",
                principalTable: "ExpenseCategory",
                principalColumn: "ExpenseCategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseReport_ExpenseCategory_ExpenseCategoryId",
                table: "ExpenseReport");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseReport_ExpenseCategoryId",
                table: "ExpenseReport");

            migrationBuilder.DropColumn(
                name: "ExpenseCategoryId",
                table: "ExpenseReport");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "ExpenseReport",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
