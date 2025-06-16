using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ErpMobile.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTokenScopeFieldManual : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Scope",
                schema: "dbo",
                table: "TempCustomerTokens",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
