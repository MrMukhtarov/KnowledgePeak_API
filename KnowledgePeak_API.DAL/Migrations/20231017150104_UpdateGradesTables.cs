using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowledgePeak_API.DAL.Migrations
{
    public partial class UpdateGradesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_LessonId",
                table: "Grades",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Lessons_LessonId",
                table: "Grades",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Lessons_LessonId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_LessonId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Grades");
        }
    }
}
