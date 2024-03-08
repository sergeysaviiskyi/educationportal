using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationPortal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class JoinedUserWithVerificationCodeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "VerificationCodes");

            migrationBuilder.AddColumn<int>(
                name: "VerificationCodeId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_VerificationCodeId",
                table: "Users",
                column: "VerificationCodeId",
                unique: true,
                filter: "[VerificationCodeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_VerificationCodes_VerificationCodeId",
                table: "Users",
                column: "VerificationCodeId",
                principalTable: "VerificationCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_VerificationCodes_VerificationCodeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_VerificationCodeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VerificationCodeId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "VerificationCodes",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");
        }
    }
}
