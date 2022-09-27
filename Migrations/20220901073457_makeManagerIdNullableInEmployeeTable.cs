using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC.Migrations
{
    public partial class makeManagerIdNullableInEmployeeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_Manager_Id",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "Manager_Id",
                table: "Employees",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_Manager_Id",
                table: "Employees",
                column: "Manager_Id",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_Manager_Id",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "Manager_Id",
                table: "Employees",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_Manager_Id",
                table: "Employees",
                column: "Manager_Id",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
