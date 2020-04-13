using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRent.Contexts.SQLiteContext.Migrations
{
    public partial class AutoGenerateID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Colour",
                table: "Cars",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldDefaultValue: 0)
                .OldAnnotation("Sqlite:Autoincrement", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Colour",
                table: "Cars",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldDefaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);
        }
    }
}
