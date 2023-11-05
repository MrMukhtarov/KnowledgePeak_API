using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowledgePeak_API.DAL.Migrations
{
    public partial class UpdateSpeciality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeachersLessons_Lessons_LessonId",
                table: "TeachersLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSpecialities_Specialities_SpecialityId",
                table: "TeacherSpecialities");

            migrationBuilder.AlterColumn<int>(
                name: "SpecialityId",
                table: "TeacherSpecialities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LessonId",
                table: "TeachersLessons",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersLessons_Lessons_LessonId",
                table: "TeachersLessons",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSpecialities_Specialities_SpecialityId",
                table: "TeacherSpecialities",
                column: "SpecialityId",
                principalTable: "Specialities",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeachersLessons_Lessons_LessonId",
                table: "TeachersLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSpecialities_Specialities_SpecialityId",
                table: "TeacherSpecialities");

            migrationBuilder.AlterColumn<int>(
                name: "SpecialityId",
                table: "TeacherSpecialities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LessonId",
                table: "TeachersLessons",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersLessons_Lessons_LessonId",
                table: "TeachersLessons",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSpecialities_Specialities_SpecialityId",
                table: "TeacherSpecialities",
                column: "SpecialityId",
                principalTable: "Specialities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
