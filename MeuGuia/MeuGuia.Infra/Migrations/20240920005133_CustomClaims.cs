﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeuGuia.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CustomClaims : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Token",
                table: "AspNetUserClaims",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "AspNetUserClaims");
        }
    }
}
