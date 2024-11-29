using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoListApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDueTimeToTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "DueTime",
                table: "Tasks",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueTime",
                table: "Tasks");
        }
    }
}
