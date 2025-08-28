using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webapp.Migrations
{
    /// <inheritdoc />
    public partial class updateconstraintsunique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Equipment_NumeroSerie",
                table: "Equipment",
                column: "NumeroSerie",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Telephone",
                table: "Employees",
                column: "Telephone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Equipment_NumeroSerie",
                table: "Equipment");

            migrationBuilder.DropIndex(
                name: "IX_Employee_Email",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employee_Telephone",
                table: "Employees");
        }
    }
}
