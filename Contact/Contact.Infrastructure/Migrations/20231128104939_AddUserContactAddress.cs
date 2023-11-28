using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contact.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserContactAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "UserContacts",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "UserContacts");
        }
    }
}
