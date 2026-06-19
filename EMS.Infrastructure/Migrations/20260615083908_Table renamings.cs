using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Tablerenamings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailAttachments_EmailInboxes_EmailInboxId",
                table: "EmailAttachments");

            migrationBuilder.DropTable(
                name: "EmailInboxes");

            migrationBuilder.DropTable(
                name: "MailBoxConfigs");

            migrationBuilder.RenameColumn(
                name: "EmailInboxId",
                table: "EmailAttachments",
                newName: "EmailId");

            migrationBuilder.RenameIndex(
                name: "IX_EmailAttachments_EmailInboxId",
                table: "EmailAttachments",
                newName: "IX_EmailAttachments_EmailId");

            migrationBuilder.CreateTable(
                name: "EmailAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientSecret = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrganisationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsValidated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailAccounts_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    EmailAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmailCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    AssignedTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExternalMessageId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emails_EmailAccounts_EmailAccountId",
                        column: x => x.EmailAccountId,
                        principalTable: "EmailAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Emails_EmailCategories_EmailCategoryId",
                        column: x => x.EmailCategoryId,
                        principalTable: "EmailCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailAccounts_OrganisationId",
                table: "EmailAccounts",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_EmailAccountId",
                table: "Emails",
                column: "EmailAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_EmailCategoryId",
                table: "Emails",
                column: "EmailCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailAttachments_Emails_EmailId",
                table: "EmailAttachments",
                column: "EmailId",
                principalTable: "Emails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailAttachments_Emails_EmailId",
                table: "EmailAttachments");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "EmailAccounts");

            migrationBuilder.RenameColumn(
                name: "EmailId",
                table: "EmailAttachments",
                newName: "EmailInboxId");

            migrationBuilder.RenameIndex(
                name: "IX_EmailAttachments_EmailId",
                table: "EmailAttachments",
                newName: "IX_EmailAttachments_EmailInboxId");

            migrationBuilder.CreateTable(
                name: "MailBoxConfigs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganisationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientSecret = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsValidated = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailBoxConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MailBoxConfigs_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailInboxes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmailCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MailBoxConfigId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExternalMessageId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailInboxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailInboxes_EmailCategories_EmailCategoryId",
                        column: x => x.EmailCategoryId,
                        principalTable: "EmailCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_EmailInboxes_MailBoxConfigs_MailBoxConfigId",
                        column: x => x.MailBoxConfigId,
                        principalTable: "MailBoxConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailInboxes_EmailCategoryId",
                table: "EmailInboxes",
                column: "EmailCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailInboxes_MailBoxConfigId",
                table: "EmailInboxes",
                column: "MailBoxConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_MailBoxConfigs_OrganisationId",
                table: "MailBoxConfigs",
                column: "OrganisationId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailAttachments_EmailInboxes_EmailInboxId",
                table: "EmailAttachments",
                column: "EmailInboxId",
                principalTable: "EmailInboxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
