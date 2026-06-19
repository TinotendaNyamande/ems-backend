using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addmessageIdcolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailCategories_Organisations_OrganisationId",
                table: "EmailCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailInboxes_EmailCategories_EmailCategoryId",
                table: "EmailInboxes");

            migrationBuilder.AddColumn<string>(
                name: "AssignedTo",
                table: "EmailInboxes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalMessageId",
                table: "EmailInboxes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailCategories_Organisations_OrganisationId",
                table: "EmailCategories",
                column: "OrganisationId",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailInboxes_EmailCategories_EmailCategoryId",
                table: "EmailInboxes",
                column: "EmailCategoryId",
                principalTable: "EmailCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailCategories_Organisations_OrganisationId",
                table: "EmailCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailInboxes_EmailCategories_EmailCategoryId",
                table: "EmailInboxes");

            migrationBuilder.DropColumn(
                name: "AssignedTo",
                table: "EmailInboxes");

            migrationBuilder.DropColumn(
                name: "ExternalMessageId",
                table: "EmailInboxes");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailCategories_Organisations_OrganisationId",
                table: "EmailCategories",
                column: "OrganisationId",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailInboxes_EmailCategories_EmailCategoryId",
                table: "EmailInboxes",
                column: "EmailCategoryId",
                principalTable: "EmailCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
