using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedTo",
                table: "Emails");

            migrationBuilder.AddColumn<bool>(
                name: "IsAssigned",
                table: "Emails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            // migrationBuilder.AddColumn<Guid>(
            //     name: "OrganisationId",
            //     table: "Emails",
            //     type: "uniqueidentifier",
            //     nullable: false,
            //     defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EmailCategoryId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "EmailCategoriesUserMatrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmailCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    LastAssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailCategoriesUserMatrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailCategoriesUserMatrices_EmailCategories_EmailCategoryId",
                        column: x => x.EmailCategoryId,
                        principalTable: "EmailCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedToUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignedToUserDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AdditionalInformation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailTasks_Emails_EmailId",
                        column: x => x.EmailId,
                        principalTable: "Emails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // migrationBuilder.CreateIndex(
            //     name: "IX_Emails_OrganisationId",
            //     table: "Emails",
            //     column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailCategoriesUserMatrices_EmailCategoryId",
                table: "EmailCategoriesUserMatrices",
                column: "EmailCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTasks_EmailId",
                table: "EmailTasks",
                column: "EmailId");

            // migrationBuilder.AddForeignKey(
            //     name: "FK_Emails_Organisations_OrganisationId",
            //     table: "Emails",
            //     column: "OrganisationId",
            //     principalTable: "Organisations",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropForeignKey(
            //     name: "FK_Emails_Organisations_OrganisationId",
            //     table: "Emails");

            migrationBuilder.DropTable(
                name: "EmailCategoriesUserMatrices");

            migrationBuilder.DropTable(
                name: "EmailTasks");

            // migrationBuilder.DropIndex(
            //     name: "IX_Emails_OrganisationId",
            //     table: "Emails");

            migrationBuilder.DropColumn(
                name: "IsAssigned",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "EmailCategoryId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AssignedTo",
                table: "Emails",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
