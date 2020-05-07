using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRent.Contexts.SQLiteContext.Migrations
{
    public partial class AddedRentingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cars_CarId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CarId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Cars");

            migrationBuilder.AddColumn<string>(
                name: "RentingId",
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
                name: "RentingId",
                table: "Cars",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rentings",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Rented = table.Column<DateTime>(nullable: false),
                    PickupDate = table.Column<DateTime>(nullable: false),
                    ReturnDate = table.Column<DateTime>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    CarId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Renting", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RentingId",
                table: "Users",
                column: "RentingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_RentingId",
                table: "Cars",
                column: "RentingId",
                unique: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Renting_RentingId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Renting_RentingId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Rentings");

            migrationBuilder.DropIndex(
                name: "IX_Users_RentingId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Cars_RentingId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "RentingId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RentingId",
                table: "Cars");

            migrationBuilder.AddColumn<string>(
                name: "CarId",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Colour",
                table: "Cars",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldDefaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "Cars",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CarId",
                table: "Users",
                column: "CarId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cars_CarId",
                table: "Users",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
