using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Phase9CreatedpropertiesinSwapRequestcs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SwapRequests_Skills_OfferedSkillId",
                table: "SwapRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_SwapRequests_Skills_RequestedSkillId",
                table: "SwapRequests");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "SwapRequests",
                newName: "RespondedAt");

            migrationBuilder.AddColumn<int>(
                name: "ReceiverSkillId",
                table: "SwapRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SenderSkillId",
                table: "SwapRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SwapRequests_ReceiverSkillId",
                table: "SwapRequests",
                column: "ReceiverSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_SwapRequests_SenderSkillId",
                table: "SwapRequests",
                column: "SenderSkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_SwapRequests_Skills_OfferedSkillId",
                table: "SwapRequests",
                column: "OfferedSkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SwapRequests_Skills_RequestedSkillId",
                table: "SwapRequests",
                column: "RequestedSkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_SwapRequests_UserSkills_ReceiverSkillId",
                table: "SwapRequests",
                column: "ReceiverSkillId",
                principalTable: "UserSkills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SwapRequests_UserSkills_SenderSkillId",
                table: "SwapRequests",
                column: "SenderSkillId",
                principalTable: "UserSkills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SwapRequests_Skills_OfferedSkillId",
                table: "SwapRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_SwapRequests_Skills_RequestedSkillId",
                table: "SwapRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_SwapRequests_UserSkills_ReceiverSkillId",
                table: "SwapRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_SwapRequests_UserSkills_SenderSkillId",
                table: "SwapRequests");

            migrationBuilder.DropIndex(
                name: "IX_SwapRequests_ReceiverSkillId",
                table: "SwapRequests");

            migrationBuilder.DropIndex(
                name: "IX_SwapRequests_SenderSkillId",
                table: "SwapRequests");

            migrationBuilder.DropColumn(
                name: "ReceiverSkillId",
                table: "SwapRequests");

            migrationBuilder.DropColumn(
                name: "SenderSkillId",
                table: "SwapRequests");

            migrationBuilder.RenameColumn(
                name: "RespondedAt",
                table: "SwapRequests",
                newName: "UpdatedAt");

            migrationBuilder.AddForeignKey(
                name: "FK_SwapRequests_Skills_OfferedSkillId",
                table: "SwapRequests",
                column: "OfferedSkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SwapRequests_Skills_RequestedSkillId",
                table: "SwapRequests",
                column: "RequestedSkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
