using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projects.Migrations
{
    /// <inheritdoc />
    public partial class updateprojectsettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ProjectSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "ProjectSettings");
        }
    }
}
