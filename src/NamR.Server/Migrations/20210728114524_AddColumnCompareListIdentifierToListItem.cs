using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NamR.Server.Migrations
{
    public partial class AddColumnCompareListIdentifierToListItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompareListIdentifier",
                table: "ListItems",
                type: "uuid",
                nullable: false);              
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompareListIdentifier",
                table: "ListItems");
        }
    }
}
