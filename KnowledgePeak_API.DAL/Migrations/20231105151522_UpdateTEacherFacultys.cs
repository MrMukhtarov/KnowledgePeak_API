using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowledgePeak_API.DAL.Migrations
{
    public partial class UpdateTEacherFacultys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeachersFacultys_Faculties_FacultyId",
                table: "TeachersFacultys");

            migrationBuilder.AlterColumn<int>(
                name: "FacultyId",
                table: "TeachersFacultys",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersFacultys_Faculties_FacultyId",
                table: "TeachersFacultys",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeachersFacultys_Faculties_FacultyId",
                table: "TeachersFacultys");

            migrationBuilder.AlterColumn<int>(
                name: "FacultyId",
                table: "TeachersFacultys",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersFacultys_Faculties_FacultyId",
                table: "TeachersFacultys",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
