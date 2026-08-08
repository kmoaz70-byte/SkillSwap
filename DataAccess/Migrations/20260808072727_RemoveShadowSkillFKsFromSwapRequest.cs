using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveShadowSkillFKsFromSwapRequest : Migration
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

            migrationBuilder.DropIndex(
                name: "IX_SwapRequests_OfferedSkillId",
                table: "SwapRequests");

            migrationBuilder.DropIndex(
                name: "IX_SwapRequests_RequestedSkillId",
                table: "SwapRequests");

            migrationBuilder.DropColumn(
                name: "OfferedSkillId",
                table: "SwapRequests");

            migrationBuilder.DropColumn(
                name: "RequestedSkillId",
                table: "SwapRequests");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "SwapRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "SwapRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SwapRequests_ApplicationUserId",
                table: "SwapRequests",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SwapRequests_ApplicationUserId1",
                table: "SwapRequests",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SwapRequests_AspNetUsers_ApplicationUserId",
                table: "SwapRequests",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SwapRequests_AspNetUsers_ApplicationUserId1",
                table: "SwapRequests",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SwapRequests_AspNetUsers_ApplicationUserId",
                table: "SwapRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_SwapRequests_AspNetUsers_ApplicationUserId1",
                table: "SwapRequests");

            migrationBuilder.DropIndex(
                name: "IX_SwapRequests_ApplicationUserId",
                table: "SwapRequests");

            migrationBuilder.DropIndex(
                name: "IX_SwapRequests_ApplicationUserId1",
                table: "SwapRequests");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "SwapRequests");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "SwapRequests");

            migrationBuilder.AddColumn<int>(
                name: "OfferedSkillId",
                table: "SwapRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequestedSkillId",
                table: "SwapRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SwapRequests_OfferedSkillId",
                table: "SwapRequests",
                column: "OfferedSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_SwapRequests_RequestedSkillId",
                table: "SwapRequests",
                column: "RequestedSkillId");

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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
