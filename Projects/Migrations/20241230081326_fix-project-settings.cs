using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projects.Migrations
{
    /// <inheritdoc />
    public partial class fixprojectsettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectSettings_Properties_PropertyId",
                table: "ProjectSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectSettings",
                table: "ProjectSettings");

            migrationBuilder.RenameTable(
                name: "ProjectSettings",
                newName: "PropertySettings");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectSettings_PropertyId",
                table: "PropertySettings",
                newName: "IX_PropertySettings_PropertyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertySettings",
                table: "PropertySettings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertySettings_Properties_PropertyId",
                table: "PropertySettings",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertySettings_Properties_PropertyId",
                table: "PropertySettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertySettings",
                table: "PropertySettings");

            migrationBuilder.RenameTable(
                name: "PropertySettings",
                newName: "ProjectSettings");

            migrationBuilder.RenameIndex(
                name: "IX_PropertySettings_PropertyId",
                table: "ProjectSettings",
                newName: "IX_ProjectSettings_PropertyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectSettings",
                table: "ProjectSettings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectSettings_Properties_PropertyId",
                table: "ProjectSettings",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
