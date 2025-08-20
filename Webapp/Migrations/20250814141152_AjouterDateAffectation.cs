using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webapp.Migrations
{
    /// <inheritdoc />
    public partial class AjouterDateAffectation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateDerniereAffectation",
                table: "Equipment",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateDerniereAffectation",
                table: "Equipment");
        }
    }
}
