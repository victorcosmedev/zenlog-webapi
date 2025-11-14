using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZenLogAPI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class init_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_zlg_empresa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    RazaoSocial = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Setor = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_zlg_empresa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_zlg_colaborador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Username = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    NumeroMatricula = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false),
                    Cpf = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false),
                    EmpresaId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_zlg_colaborador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_zlg_colaborador_tb_zlg_empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "tb_zlg_empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_zlg_log_emocional",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NivelEmocional = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DescricaoSentimento = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    FezExercicios = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    HorasDescanso = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    LitrosAgua = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ColaboradorId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_zlg_log_emocional", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_zlg_log_emocional_tb_zlg_colaborador_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "tb_zlg_colaborador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IDX_colaborador_cpf",
                table: "tb_zlg_colaborador",
                column: "Cpf");

            migrationBuilder.CreateIndex(
                name: "IDX_colaborador_email",
                table: "tb_zlg_colaborador",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IDX_colaborador_matricula",
                table: "tb_zlg_colaborador",
                column: "NumeroMatricula");

            migrationBuilder.CreateIndex(
                name: "IX_tb_zlg_colaborador_EmpresaId",
                table: "tb_zlg_colaborador",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_zlg_log_emocional_ColaboradorId",
                table: "tb_zlg_log_emocional",
                column: "ColaboradorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_zlg_log_emocional");

            migrationBuilder.DropTable(
                name: "tb_zlg_colaborador");

            migrationBuilder.DropTable(
                name: "tb_zlg_empresa");
        }
    }
}
