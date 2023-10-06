using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowledgePeak_API.DAL.Migrations
{
    public partial class CreateTeacherAndTeacherFacultysAndTeacherLessonsAndTeacherSpecialitiesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Teacher_Description",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Teacher_EndDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Teacher_IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Teacher_Salary",
                table: "AspNetUsers",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Teacher_StartDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "DATEADD(hour, 4, GETUTCDATE())");

            migrationBuilder.AddColumn<int>(
                name: "Teacher_Status",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TeachersFacultys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachersFacultys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeachersFacultys_AspNetUsers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeachersFacultys_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeachersLessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachersLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeachersLessons_AspNetUsers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeachersLessons_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherSpecialities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SpecialityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSpecialities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherSpecialities_AspNetUsers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherSpecialities_Specialities_SpecialityId",
                        column: x => x.SpecialityId,
                        principalTable: "Specialities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeachersFacultys_FacultyId",
                table: "TeachersFacultys",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachersFacultys_TeacherId",
                table: "TeachersFacultys",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachersLessons_LessonId",
                table: "TeachersLessons",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachersLessons_TeacherId",
                table: "TeachersLessons",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSpecialities_SpecialityId",
                table: "TeacherSpecialities",
                column: "SpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSpecialities_TeacherId",
                table: "TeacherSpecialities",
                column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeachersFacultys");

            migrationBuilder.DropTable(
                name: "TeachersLessons");

            migrationBuilder.DropTable(
                name: "TeacherSpecialities");

            migrationBuilder.DropColumn(
                name: "Teacher_Description",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Teacher_EndDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Teacher_IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Teacher_Salary",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Teacher_StartDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Teacher_Status",
                table: "AspNetUsers");
        }
    }
}
