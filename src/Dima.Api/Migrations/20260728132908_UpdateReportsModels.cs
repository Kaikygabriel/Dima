using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dima.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReportsModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voucher_IdentityUser_UserId",
                table: "Voucher");

            migrationBuilder.DropIndex(
                name: "IX_Voucher_UserId",
                table: "Voucher");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Voucher");

            migrationBuilder.CreateTable(
                name: "VoucherUser",
                columns: table => new
                {
                    VoucherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherUser", x => new { x.VoucherId, x.UserId });
                    table.ForeignKey(
                        name: "FK_VoucherUser_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoucherUser_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Voucher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VoucherUser_UserId",
                table: "VoucherUser",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoucherUser");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Voucher",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_UserId",
                table: "Voucher",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Voucher_IdentityUser_UserId",
                table: "Voucher",
                column: "UserId",
                principalTable: "IdentityUser",
                principalColumn: "Id");
        }
    }
}
