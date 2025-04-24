using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICaixa.Migrations
{
    /// <inheritdoc />
    public partial class CriarEstruturaInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Documento = table.Column<string>(type: "text", nullable: false),
                    Saldo = table.Column<decimal>(type: "numeric", nullable: false),
                    DataAbertura = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Ativa = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogsDesativacaoConta",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Documento = table.Column<string>(type: "text", nullable: false),
                    RealizadoPor = table.Column<string>(type: "text", nullable: false),
                    DataHoraDesativacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogsDesativacaoConta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogsTransferencia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentoOrigem = table.Column<string>(type: "text", nullable: false),
                    DocumentoDestino = table.Column<string>(type: "text", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false),
                    DataTransferencia = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogsTransferencia", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contas_Documento",
                table: "Contas",
                column: "Documento",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contas");

            migrationBuilder.DropTable(
                name: "LogsDesativacaoConta");

            migrationBuilder.DropTable(
                name: "LogsTransferencia");
        }
    }
}
