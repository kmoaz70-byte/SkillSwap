using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddingSkillManagementFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "UserSkills",
                newName: "SkillType");

            migrationBuilder.AddColumn<int>(
                name: "ProficiencyLevel",
                table: "UserSkills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Category",
                table: "Skills",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ApprovalStatus",
                table: "Skills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SuggestedByUserId",
                table: "Skills",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_SuggestedByUserId",
                table: "Skills",
                column: "SuggestedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_AspNetUsers_SuggestedByUserId",
                table: "Skills",
                column: "SuggestedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_AspNetUsers_SuggestedByUserId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_SuggestedByUserId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "ProficiencyLevel",
                table: "UserSkills");

            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "SuggestedByUserId",
                table: "Skills");

            migrationBuilder.RenameColumn(
                name: "SkillType",
                table: "UserSkills",
                newName: "Type");

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Skills",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
