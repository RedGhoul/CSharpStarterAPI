using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class FixedSpellingErrorSentEmailRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SentEmailRecords_Recipent",
                table: "SentEmailRecords");

            migrationBuilder.DropColumn(
                name: "Recipent",
                table: "SentEmailRecords");

            migrationBuilder.AddColumn<string>(
                name: "Recipient",
                table: "SentEmailRecords",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SentEmailRecords_Recipient",
                table: "SentEmailRecords",
                column: "Recipient");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SentEmailRecords_Recipient",
                table: "SentEmailRecords");

            migrationBuilder.DropColumn(
                name: "Recipient",
                table: "SentEmailRecords");

            migrationBuilder.AddColumn<string>(
                name: "Recipent",
                table: "SentEmailRecords",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SentEmailRecords_Recipent",
                table: "SentEmailRecords",
                column: "Recipent");
        }
    }
}
