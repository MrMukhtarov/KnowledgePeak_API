using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowledgePeak_API.DAL.Migrations
{
    public partial class CreateStudentsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Avarage",
                table: "AspNetUsers",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Course",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Student_EndDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Student_StartDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Student_Status",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GroupId",
                table: "AspNetUsers",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Groups_GroupId",
                table: "AspNetUsers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Groups_GroupId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GroupId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Avarage",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Course",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Student_EndDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Student_StartDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Student_Status",
                table: "AspNetUsers");
        }
    }
}
