using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Clockwork.API.Migrations
{
    public partial class AddRequestedTimeZoneId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequestedTimeZoneId",
                table: "CurrentTimeQueries",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestedTimeZoneId",
                table: "CurrentTimeQueries");
        }
    }
}
