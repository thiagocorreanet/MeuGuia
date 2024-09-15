using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeuGuia.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoReceita : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "AuditsProcess",
                type: "VARCHAR(20)",
                nullable: false,
                comment: "Tipo da auditoria",
                oldClrType: typeof(string),
                oldType: "VARCHAR(20)",
                oldComment: "Tipo da auditoria")
                .Annotation("Relational:ColumnOrder", 2)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<string>(
                name: "Table",
                table: "AuditsProcess",
                type: "VARCHAR(100)",
                nullable: false,
                comment: "Tabela na qual vai ser exibida na auditoria",
                oldClrType: typeof(string),
                oldType: "VARCHAR(100)",
                oldComment: "Tabela na qual vai ser exibida na auditoria")
                .Annotation("Relational:ColumnOrder", 3)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<string>(
                name: "PrimaryKey",
                table: "AuditsProcess",
                type: "VARCHAR(50)",
                nullable: true,
                comment: "Primary key da tabela",
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)",
                oldNullable: true,
                oldComment: "Primary key da tabela")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<string>(
                name: "OldValues",
                table: "AuditsProcess",
                type: "VARCHAR(MAX)",
                nullable: true,
                comment: "Valor antigo",
                oldClrType: typeof(string),
                oldType: "VARCHAR(MAX)",
                oldNullable: true,
                oldComment: "Valor antigo")
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<string>(
                name: "NewValues",
                table: "AuditsProcess",
                type: "VARCHAR(MAX)",
                nullable: true,
                comment: "Novo valor",
                oldClrType: typeof(string),
                oldType: "VARCHAR(MAX)",
                oldNullable: true,
                oldComment: "Novo valor")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModificationDate",
                table: "AuditsProcess",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                comment: "Data da última atualização da empresa",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()",
                oldComment: "Data da última atualização da empresa")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "AuditsProcess",
                type: "DATETIME2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                comment: "Data de criação da empresa",
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2",
                oldDefaultValueSql: "GETDATE()",
                oldComment: "Data de criação da empresa")
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<string>(
                name: "AffectedColumns",
                table: "AuditsProcess",
                type: "VARCHAR(MAX)",
                nullable: true,
                comment: "Colunas que foram afetadas",
                oldClrType: typeof(string),
                oldType: "VARCHAR(MAX)",
                oldNullable: true,
                oldComment: "Colunas que foram afetadas")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.CreateTable(
                name: "Revenues",
                columns: table => new
                {
                    RevenueId = table.Column<int>(type: "int", nullable: false, comment: "Chave primária da receita.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "VARCHAR(50)", nullable: false, comment: "Descrição da receita."),
                    Value = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false, comment: "Valor da receita."),
                    CreationDate = table.Column<DateTime>(type: "DATETIME2", nullable: false, defaultValueSql: "GETDATE()", comment: "Data de criação do registro"),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "Data da última atualização do registro")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REVENUES", x => x.RevenueId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_REVENUE_ID",
                table: "Revenues",
                column: "RevenueId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Revenues");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "AuditsProcess",
                type: "VARCHAR(20)",
                nullable: false,
                comment: "Tipo da auditoria",
                oldClrType: typeof(string),
                oldType: "VARCHAR(20)",
                oldComment: "Tipo da auditoria")
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<string>(
                name: "Table",
                table: "AuditsProcess",
                type: "VARCHAR(100)",
                nullable: false,
                comment: "Tabela na qual vai ser exibida na auditoria",
                oldClrType: typeof(string),
                oldType: "VARCHAR(100)",
                oldComment: "Tabela na qual vai ser exibida na auditoria")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<string>(
                name: "PrimaryKey",
                table: "AuditsProcess",
                type: "VARCHAR(50)",
                nullable: true,
                comment: "Primary key da tabela",
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)",
                oldNullable: true,
                oldComment: "Primary key da tabela")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<string>(
                name: "OldValues",
                table: "AuditsProcess",
                type: "VARCHAR(MAX)",
                nullable: true,
                comment: "Valor antigo",
                oldClrType: typeof(string),
                oldType: "VARCHAR(MAX)",
                oldNullable: true,
                oldComment: "Valor antigo")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<string>(
                name: "NewValues",
                table: "AuditsProcess",
                type: "VARCHAR(MAX)",
                nullable: true,
                comment: "Novo valor",
                oldClrType: typeof(string),
                oldType: "VARCHAR(MAX)",
                oldNullable: true,
                oldComment: "Novo valor")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModificationDate",
                table: "AuditsProcess",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                comment: "Data da última atualização da empresa",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()",
                oldComment: "Data da última atualização da empresa")
                .Annotation("Relational:ColumnOrder", 11)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "AuditsProcess",
                type: "DATETIME2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                comment: "Data de criação da empresa",
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2",
                oldDefaultValueSql: "GETDATE()",
                oldComment: "Data de criação da empresa")
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<string>(
                name: "AffectedColumns",
                table: "AuditsProcess",
                type: "VARCHAR(MAX)",
                nullable: true,
                comment: "Colunas que foram afetadas",
                oldClrType: typeof(string),
                oldType: "VARCHAR(MAX)",
                oldNullable: true,
                oldComment: "Colunas que foram afetadas")
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 6);
        }
    }
}
