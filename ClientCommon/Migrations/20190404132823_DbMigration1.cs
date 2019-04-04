using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientCommon.Migrations
{
    public partial class DbMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswerAmount",
                table: "Tests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IncorrectAnswerAmount",
                table: "Tests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswerAmount",
                table: "TaskInstances",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IncorrectAnswerAmount",
                table: "TaskInstances",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectAnswerAmount",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "IncorrectAnswerAmount",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "CorrectAnswerAmount",
                table: "TaskInstances");

            migrationBuilder.DropColumn(
                name: "IncorrectAnswerAmount",
                table: "TaskInstances");
        }
    }
}
