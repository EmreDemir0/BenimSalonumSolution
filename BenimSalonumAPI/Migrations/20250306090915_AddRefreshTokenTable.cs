﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenimSalonumAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Token = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");
        }
    }
}
