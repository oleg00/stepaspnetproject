using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppBlog.UI.Data.Migrations
{
    public partial class userchanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_Id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "_UserName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "_CurrentUserName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "_IsBlocked",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "_OldUserName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "_Rate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_CurrentUserName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "_IsBlocked",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "_OldUserName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "_Rate",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "_Id",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_UserName",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
