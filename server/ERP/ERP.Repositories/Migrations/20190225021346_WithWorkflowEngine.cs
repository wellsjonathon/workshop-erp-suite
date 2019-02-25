using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Repositories.Migrations
{
    public partial class WithWorkflowEngine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransitionActions_Transitions_TransitionId",
                table: "TransitionActions");

            migrationBuilder.DropForeignKey(
                name: "FK_TransitionActions_Actions_WorkflowActionId",
                table: "TransitionActions");

            migrationBuilder.DropIndex(
                name: "IX_TransitionActions_TransitionId",
                table: "TransitionActions");

            migrationBuilder.DropIndex(
                name: "IX_TransitionActions_WorkflowActionId",
                table: "TransitionActions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TransitionActions_TransitionId",
                table: "TransitionActions",
                column: "TransitionId");

            migrationBuilder.CreateIndex(
                name: "IX_TransitionActions_WorkflowActionId",
                table: "TransitionActions",
                column: "WorkflowActionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransitionActions_Transitions_TransitionId",
                table: "TransitionActions",
                column: "TransitionId",
                principalTable: "Transitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransitionActions_Actions_WorkflowActionId",
                table: "TransitionActions",
                column: "WorkflowActionId",
                principalTable: "Actions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
