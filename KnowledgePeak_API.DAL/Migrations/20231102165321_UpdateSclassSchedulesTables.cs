using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowledgePeak_API.DAL.Migrations
{
    public partial class UpdateSclassSchedulesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "ClassSchedules");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "ClassSchedules",
                type: "int",
                nullable: true);
        }
    }
}
