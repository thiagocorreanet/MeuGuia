using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeuGuia.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Permissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "int", nullable: false, comment: "Chave primária da permissão.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "VARCHAR(36)", nullable: false, comment: "Token da tabela"),
                    AspNetUserClaimId = table.Column<int>(type: "INT", nullable: false, comment: "Chave primária da tabela AspNetUserClaims"),
                    UserId = table.Column<string>(type: "VARCHAR(36)", nullable: false, comment: "Chave do usuário."),
                    Page = table.Column<string>(type: "VARCHAR(50)", nullable: false, comment: "Nome da página que o usuário terá acesso."),
                    Role = table.Column<string>(type: "VARCHAR(50)", nullable: false, comment: "Nome da permissão do usuário."),
                    CreationDate = table.Column<DateTime>(type: "DATETIME2", nullable: false, defaultValueSql: "GETDATE()", comment: "Data de criação do registro"),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "Data da última atualização do registro")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERMISSIONS", x => x.PermissionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PERMISSION_ID",
                table: "Permissions",
                column: "PermissionId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permissions");
        }
    }
}
