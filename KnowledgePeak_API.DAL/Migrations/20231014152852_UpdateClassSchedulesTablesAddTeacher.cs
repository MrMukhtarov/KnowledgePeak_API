using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowledgePeak_API.DAL.Migrations
{
    public partial class UpdateClassSchedulesTablesAddTeacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TeacherId",
                table: "ClassSchedules",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedules_TeacherId",
                table: "ClassSchedules",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSchedules_AspNetUsers_TeacherId",
                table: "ClassSchedules",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSchedules_AspNetUsers_TeacherId",
                table: "ClassSchedules");

            migrationBuilder.DropIndex(
                name: "IX_ClassSchedules_TeacherId",
                table: "ClassSchedules");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "ClassSchedules");
        }
    }
}
