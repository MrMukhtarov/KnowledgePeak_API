using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowledgePeak_API.DAL.Migrations
{
    public partial class CreateStudentHistoryTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GradeId = table.Column<int>(type: "int", nullable: false),
                    Studentid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentHistories_AspNetUsers_Studentid",
                        column: x => x.Studentid,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentHistories_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentHistories_GradeId",
                table: "StudentHistories",
                column: "GradeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentHistories_Studentid",
                table: "StudentHistories",
                column: "Studentid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentHistories");
        }
    }
}
