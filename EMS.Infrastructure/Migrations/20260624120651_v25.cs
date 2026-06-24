using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
