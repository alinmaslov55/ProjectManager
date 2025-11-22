using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManager.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RenameCreatedToCreatedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Comments",
                newName: "CreatedDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Comments",
                newName: "Created");
        }
    }
}
