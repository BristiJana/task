using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechPrimeLab.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitials3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "project_details",
                columns: table => new
                {
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    project_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    project_name = table.Column<string>(type: "text", nullable: false),
                    project_theme = table.Column<string>(type: "text", nullable: false),
                    project_reason = table.Column<string>(type: "text", nullable: false),
                    project_type = table.Column<string>(type: "text", nullable: false),
                    project_division = table.Column<string>(type: "text", nullable: false),
                    project_category = table.Column<string>(type: "text", nullable: false),
                    project_priority = table.Column<string>(type: "text", nullable: false),
                    project_department = table.Column<string>(type: "text", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    project_location = table.Column<string>(type: "text", nullable: false),
                    project_status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_details", x => x.project_id);
                });

            migrationBuilder.CreateTable(
                name: "user_details",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_name = table.Column<string>(type: "text", nullable: false),
                    user_email = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<byte[]>(type: "bytea", nullable: false),
                    password_salt = table.Column<byte[]>(type: "bytea", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_ts = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modified_ts = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_details", x => x.user_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "project_details");

            migrationBuilder.DropTable(
                name: "user_details");
        }
    }
}
