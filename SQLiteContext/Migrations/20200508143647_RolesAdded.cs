using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRent.Contexts.SQLiteContext.Migrations
{
    public partial class RolesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Renting_RentingId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Renting_RentingId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Renting",
                table: "Renting");

            migrationBuilder.RenameTable(
                name: "Renting",
                newName: "Rentings");

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "Users",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Colour",
                table: "Cars",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldDefaultValue: 0)
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Cars",
                maxLength: 150,
                nullable: true,
                defaultValue: "dummyCar.jpg");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Rented",
                table: "Rentings",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2020, 4, 27, 13, 46, 32, 193, DateTimeKind.Local).AddTicks(3121));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rentings",
                table: "Rentings",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Rentings_RentingId",
                table: "Cars",
                column: "RentingId",
                principalTable: "Rentings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Rentings_RentingId",
                table: "Users",
                column: "RentingId",
                principalTable: "Rentings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Rentings_RentingId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Rentings_RentingId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rentings",
                table: "Rentings");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Cars");

            migrationBuilder.RenameTable(
                name: "Rentings",
                newName: "Renting");

            migrationBuilder.AlterColumn<int>(
                name: "Colour",
                table: "Cars",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldDefaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Rented",
                table: "Renting",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2020, 4, 27, 13, 46, 32, 193, DateTimeKind.Local).AddTicks(3121),
                oldClrType: typeof(DateTime));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Renting",
                table: "Renting",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Renting_RentingId",
                table: "Cars",
                column: "RentingId",
                principalTable: "Renting",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Renting_RentingId",
                table: "Users",
                column: "RentingId",
                principalTable: "Renting",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
