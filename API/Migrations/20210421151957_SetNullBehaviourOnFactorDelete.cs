using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class SetNullBehaviourOnFactorDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tips_Factors_HealthFactorId",
                table: "Tips");

            migrationBuilder.DropForeignKey(
                name: "FK_Tips_Factors_LaborFactorId",
                table: "Tips");

            migrationBuilder.DropForeignKey(
                name: "FK_Tips_Factors_MentalFactorId",
                table: "Tips");

            migrationBuilder.DropForeignKey(
                name: "FK_Tips_Factors_SleepFactorId",
                table: "Tips");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTips_Tips_TipId",
                table: "UserTips");

            migrationBuilder.AddForeignKey(
                name: "FK_Tips_Factors_HealthFactorId",
                table: "Tips",
                column: "HealthFactorId",
                principalTable: "Factors",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Tips_Factors_LaborFactorId",
                table: "Tips",
                column: "LaborFactorId",
                principalTable: "Factors",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Tips_Factors_MentalFactorId",
                table: "Tips",
                column: "MentalFactorId",
                principalTable: "Factors",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Tips_Factors_SleepFactorId",
                table: "Tips",
                column: "SleepFactorId",
                principalTable: "Factors",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTips_Tips_TipId",
                table: "UserTips",
                column: "TipId",
                principalTable: "Tips",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tips_Factors_HealthFactorId",
                table: "Tips");

            migrationBuilder.DropForeignKey(
                name: "FK_Tips_Factors_LaborFactorId",
                table: "Tips");

            migrationBuilder.DropForeignKey(
                name: "FK_Tips_Factors_MentalFactorId",
                table: "Tips");

            migrationBuilder.DropForeignKey(
                name: "FK_Tips_Factors_SleepFactorId",
                table: "Tips");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTips_Tips_TipId",
                table: "UserTips");

            migrationBuilder.AddForeignKey(
                name: "FK_Tips_Factors_HealthFactorId",
                table: "Tips",
                column: "HealthFactorId",
                principalTable: "Factors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tips_Factors_LaborFactorId",
                table: "Tips",
                column: "LaborFactorId",
                principalTable: "Factors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tips_Factors_MentalFactorId",
                table: "Tips",
                column: "MentalFactorId",
                principalTable: "Factors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tips_Factors_SleepFactorId",
                table: "Tips",
                column: "SleepFactorId",
                principalTable: "Factors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTips_Tips_TipId",
                table: "UserTips",
                column: "TipId",
                principalTable: "Tips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
