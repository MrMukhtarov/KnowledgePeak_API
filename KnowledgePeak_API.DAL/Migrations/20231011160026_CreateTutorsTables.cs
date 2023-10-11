using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowledgePeak_API.DAL.Migrations
{
    public partial class CreateTutorsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TutorId",
                table: "Groups",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Tutor_StartDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "(DATEADD(hour,4 , GETUTCDATE()))",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Tutor_IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_TutorId",
                table: "Groups",
                column: "TutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_AspNetUsers_TutorId",
                table: "Groups",
                column: "TutorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_AspNetUsers_TutorId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_TutorId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "TutorId",
                table: "Groups");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Tutor_StartDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValueSql: "(DATEADD(hour,4 , GETUTCDATE()))");

            migrationBuilder.AlterColumn<bool>(
                name: "Tutor_IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: false);
        }
    }
}
