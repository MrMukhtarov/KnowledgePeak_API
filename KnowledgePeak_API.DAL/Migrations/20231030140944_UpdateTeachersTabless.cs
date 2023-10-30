using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowledgePeak_API.DAL.Migrations
{
    public partial class UpdateTeachersTabless : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeachersFacultys_AspNetUsers_TeacherId",
                table: "TeachersFacultys");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachersLessons_AspNetUsers_TeacherId",
                table: "TeachersLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSpecialities_AspNetUsers_TeacherId",
                table: "TeacherSpecialities");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "TeacherSpecialities",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "TeachersLessons",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "TeachersFacultys",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersFacultys_AspNetUsers_TeacherId",
                table: "TeachersFacultys",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersLessons_AspNetUsers_TeacherId",
                table: "TeachersLessons",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSpecialities_AspNetUsers_TeacherId",
                table: "TeacherSpecialities",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeachersFacultys_AspNetUsers_TeacherId",
                table: "TeachersFacultys");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachersLessons_AspNetUsers_TeacherId",
                table: "TeachersLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSpecialities_AspNetUsers_TeacherId",
                table: "TeacherSpecialities");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "TeacherSpecialities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "TeachersLessons",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "TeachersFacultys",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersFacultys_AspNetUsers_TeacherId",
                table: "TeachersFacultys",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersLessons_AspNetUsers_TeacherId",
                table: "TeachersLessons",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSpecialities_AspNetUsers_TeacherId",
                table: "TeacherSpecialities",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
