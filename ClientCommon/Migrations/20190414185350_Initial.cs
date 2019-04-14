using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientCommon.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    TestId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Header = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    CorrectAnswerAmount = table.Column<int>(nullable: false),
                    IncorrectAnswerAmount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.TestId);
                });

            migrationBuilder.CreateTable(
                name: "UITypes",
                columns: table => new
                {
                    UITypeId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UITypes", x => x.UITypeId);
                });

            migrationBuilder.CreateTable(
                name: "TaskInstances",
                columns: table => new
                {
                    TaskInstanceId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SeqNo = table.Column<int>(nullable: false),
                    TestId = table.Column<int>(nullable: false),
                    TaskId = table.Column<int>(nullable: false),
                    CorrectAnswerAmount = table.Column<int>(nullable: false),
                    IncorrectAnswerAmount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskInstances", x => x.TaskInstanceId);
                    table.ForeignKey(
                        name: "FK_TaskInstances_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskInstances_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "TestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskItems",
                columns: table => new
                {
                    TaskItemId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SeqNo = table.Column<int>(nullable: false),
                    ValueInt = table.Column<int>(nullable: true),
                    ValueString = table.Column<string>(nullable: true),
                    LangItemId = table.Column<int>(nullable: false),
                    TaskId = table.Column<int>(nullable: true),
                    TaskInstanceId = table.Column<int>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    UITypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskItems", x => x.TaskItemId);
                    table.ForeignKey(
                        name: "FK_TaskItems_TaskItems_ParentId",
                        column: x => x.ParentId,
                        principalTable: "TaskItems",
                        principalColumn: "TaskItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskItems_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskItems_TaskInstances_TaskInstanceId",
                        column: x => x.TaskInstanceId,
                        principalTable: "TaskInstances",
                        principalColumn: "TaskInstanceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskItems_UITypes_UITypeId",
                        column: x => x.UITypeId,
                        principalTable: "UITypes",
                        principalColumn: "UITypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskInstances_TaskId",
                table: "TaskInstances",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskInstances_TestId",
                table: "TaskInstances",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_ParentId",
                table: "TaskItems",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_TaskId",
                table: "TaskItems",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_TaskInstanceId",
                table: "TaskItems",
                column: "TaskInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_UITypeId",
                table: "TaskItems",
                column: "UITypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskItems");

            migrationBuilder.DropTable(
                name: "TaskInstances");

            migrationBuilder.DropTable(
                name: "UITypes");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Tests");
        }
    }
}
