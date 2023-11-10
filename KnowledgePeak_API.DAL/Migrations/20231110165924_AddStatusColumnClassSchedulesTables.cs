using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowledgePeak_API.DAL.Migrations
{
    public partial class AddStatusColumnClassSchedulesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ClassSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ClassSchedules");
        }
    }
}
