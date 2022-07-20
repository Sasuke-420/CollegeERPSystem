using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeERPSystem.Services.Migrations
{
    public partial class UpdatedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Programmes_ProgrammeId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ProgrammeId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ProgrammeId",
                table: "Students");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ProgramId",
                table: "Students",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Programmes_ProgramId",
                table: "Students",
                column: "ProgramId",
                principalTable: "Programmes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Programmes_ProgramId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ProgramId",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "ProgrammeId",
                table: "Students",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_ProgrammeId",
                table: "Students",
                column: "ProgrammeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Programmes_ProgrammeId",
                table: "Students",
                column: "ProgrammeId",
                principalTable: "Programmes",
                principalColumn: "Id");
        }
    }
}
