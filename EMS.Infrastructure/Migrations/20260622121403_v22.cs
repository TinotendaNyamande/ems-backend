using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropForeignKey(
            //     name: "FK_Emails_Organisations_OrganisationId",
            //     table: "Emails");

            // migrationBuilder.DropIndex(
            //     name: "IX_Emails_OrganisationId",
            //     table: "Emails");

            // migrationBuilder.DropColumn(
            //     name: "OrganisationId",
            //     table: "Emails");

            // migrationBuilder.AddColumn<Guid>(
            //     name: "OrganisationId",
            //     table: "EmailTasks",
            //     type: "uniqueidentifier",
            //     nullable: true);

            // migrationBuilder.CreateIndex(
            //     name: "IX_EmailTasks_OrganisationId",
            //     table: "EmailTasks",
            //     column: "OrganisationId");

            // migrationBuilder.AddForeignKey(
            //     name: "FK_EmailTasks_Organisations_OrganisationId",
            //     table: "EmailTasks",
            //     column: "OrganisationId",
            //     principalTable: "Organisations",
            //     principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropForeignKey(
            //     name: "FK_EmailTasks_Organisations_OrganisationId",
            //     table: "EmailTasks");

            // migrationBuilder.DropIndex(
            //     name: "IX_EmailTasks_OrganisationId",
            //     table: "EmailTasks");

            // migrationBuilder.DropColumn(
            //     name: "OrganisationId",
            //     table: "EmailTasks");

            // migrationBuilder.AddColumn<Guid>(
            //     name: "OrganisationId",
            //     table: "Emails",
            //     type: "uniqueidentifier",
            //     nullable: false,
            //     defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            // migrationBuilder.CreateIndex(
            //     name: "IX_Emails_OrganisationId",
            //     table: "Emails",
            //     column: "OrganisationId");

            // migrationBuilder.AddForeignKey(
            //     name: "FK_Emails_Organisations_OrganisationId",
            //     table: "Emails",
            //     column: "OrganisationId",
            //     principalTable: "Organisations",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Cascade);
        }
    }
}
