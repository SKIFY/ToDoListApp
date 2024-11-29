using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoListApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDueTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompletedDate",
                table: "Tasks",
                newName: "CompletedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompletedAt",
                table: "Tasks",
                newName: "CompletedDate");
        }
    }
}
