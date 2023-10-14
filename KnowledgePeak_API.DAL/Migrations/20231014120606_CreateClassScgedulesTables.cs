using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowledgePeak_API.DAL.Migrations
{
    public partial class CreateClassScgedulesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    ClassTimeId = table.Column<int>(type: "int", nullable: false),
                    TutorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    ScheduleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassSchedules_AspNetUsers_TutorId",
                        column: x => x.TutorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClassSchedules_ClassTimes_ClassTimeId",
                        column: x => x.ClassTimeId,
                        principalTable: "ClassTimes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClassSchedules_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClassSchedules_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClassSchedules_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedules_ClassTimeId",
                table: "ClassSchedules",
                column: "ClassTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedules_GroupId",
                table: "ClassSchedules",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedules_LessonId",
                table: "ClassSchedules",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedules_RoomId",
                table: "ClassSchedules",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedules_TutorId",
                table: "ClassSchedules",
                column: "TutorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassSchedules");
        }
    }
}
