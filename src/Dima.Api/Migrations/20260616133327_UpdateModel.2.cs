using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dima.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "IdentityUser",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("3f224111-61ca-46d7-9dfb-e3a26419b9e5"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "IdentityUser",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("3f224111-61ca-46d7-9dfb-e3a26419b9e5"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
