using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class removedAndAddedNewColsToSentEmailRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SentEmailRecord",
                table: "SentEmailRecord");

            migrationBuilder.DropColumn(
                name: "Attachments",
                table: "SentEmailRecord");

            migrationBuilder.DropColumn(
                name: "BCC",
                table: "SentEmailRecord");

            migrationBuilder.DropColumn(
                name: "Body",
                table: "SentEmailRecord");

            migrationBuilder.DropColumn(
                name: "CC",
                table: "SentEmailRecord");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "SentEmailRecord");

            migrationBuilder.RenameTable(
                name: "SentEmailRecord",
                newName: "SentEmailRecords");

            migrationBuilder.RenameIndex(
                name: "IX_SentEmailRecord_Recipent",
                table: "SentEmailRecords",
                newName: "IX_SentEmailRecords_Recipent");

            migrationBuilder.RenameIndex(
                name: "IX_SentEmailRecord_CreatedAt",
                table: "SentEmailRecords",
                newName: "IX_SentEmailRecords_CreatedAt");

            migrationBuilder.AddColumn<string>(
                name: "BodyHTML",
                table: "SentEmailRecords",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BodyPlainText",
                table: "SentEmailRecords",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "SentEmailRecords",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SentEmailRecords",
                table: "SentEmailRecords",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SentEmailRecords",
                table: "SentEmailRecords");

            migrationBuilder.DropColumn(
                name: "BodyHTML",
                table: "SentEmailRecords");

            migrationBuilder.DropColumn(
                name: "BodyPlainText",
                table: "SentEmailRecords");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "SentEmailRecords");

            migrationBuilder.RenameTable(
                name: "SentEmailRecords",
                newName: "SentEmailRecord");

            migrationBuilder.RenameIndex(
                name: "IX_SentEmailRecords_Recipent",
                table: "SentEmailRecord",
                newName: "IX_SentEmailRecord_Recipent");

            migrationBuilder.RenameIndex(
                name: "IX_SentEmailRecords_CreatedAt",
                table: "SentEmailRecord",
                newName: "IX_SentEmailRecord_CreatedAt");

            migrationBuilder.AddColumn<string>(
                name: "Attachments",
                table: "SentEmailRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BCC",
                table: "SentEmailRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "SentEmailRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CC",
                table: "SentEmailRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "SentEmailRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SentEmailRecord",
                table: "SentEmailRecord",
                column: "Id");
        }
    }
}
