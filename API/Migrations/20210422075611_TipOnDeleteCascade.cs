using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class TipOnDeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTips_Tips_TipId",
                table: "UserTips");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTips_Tips_TipId",
                table: "UserTips",
                column: "TipId",
                principalTable: "Tips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTips_Tips_TipId",
                table: "UserTips");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTips_Tips_TipId",
                table: "UserTips",
                column: "TipId",
                principalTable: "Tips",
                principalColumn: "Id");
        }
    }
}
