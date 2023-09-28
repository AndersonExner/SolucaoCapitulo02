using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capitulo02.Migrations
{
    /// <inheritdoc />
    public partial class Professores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Departamentos_DepartamentoID",
                table: "Cursos");

            migrationBuilder.DropForeignKey(
                name: "FK_Departamentos_Instituicoes_InstituicaoID",
                table: "Departamentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departamentos",
                table: "Departamentos");

            migrationBuilder.RenameTable(
                name: "Departamentos",
                newName: "Departamento");

            migrationBuilder.RenameIndex(
                name: "IX_Departamentos_InstituicaoID",
                table: "Departamento",
                newName: "IX_Departamento_InstituicaoID");

            migrationBuilder.AlterColumn<string>(
                name: "FotoMimeType",
                table: "Academicos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Foto",
                table: "Academicos",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departamento",
                table: "Departamento",
                column: "DepartamentoID");

            migrationBuilder.CreateTable(
                name: "Professores",
                columns: table => new
                {
                    ProfessorID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professores", x => x.ProfessorID);
                });

            migrationBuilder.CreateTable(
                name: "CursoProfessor",
                columns: table => new
                {
                    CursoID = table.Column<long>(type: "bigint", nullable: false),
                    ProfessorID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoProfessor", x => new { x.CursoID, x.ProfessorID });
                    table.ForeignKey(
                        name: "FK_CursoProfessor_Cursos_CursoID",
                        column: x => x.CursoID,
                        principalTable: "Cursos",
                        principalColumn: "CursoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursoProfessor_Professores_ProfessorID",
                        column: x => x.ProfessorID,
                        principalTable: "Professores",
                        principalColumn: "ProfessorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CursoProfessor_ProfessorID",
                table: "CursoProfessor",
                column: "ProfessorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Departamento_DepartamentoID",
                table: "Cursos",
                column: "DepartamentoID",
                principalTable: "Departamento",
                principalColumn: "DepartamentoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Departamento_Instituicoes_InstituicaoID",
                table: "Departamento",
                column: "InstituicaoID",
                principalTable: "Instituicoes",
                principalColumn: "InstituicaoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Departamento_DepartamentoID",
                table: "Cursos");

            migrationBuilder.DropForeignKey(
                name: "FK_Departamento_Instituicoes_InstituicaoID",
                table: "Departamento");

            migrationBuilder.DropTable(
                name: "CursoProfessor");

            migrationBuilder.DropTable(
                name: "Professores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departamento",
                table: "Departamento");

            migrationBuilder.RenameTable(
                name: "Departamento",
                newName: "Departamentos");

            migrationBuilder.RenameIndex(
                name: "IX_Departamento_InstituicaoID",
                table: "Departamentos",
                newName: "IX_Departamentos_InstituicaoID");

            migrationBuilder.AlterColumn<string>(
                name: "FotoMimeType",
                table: "Academicos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Foto",
                table: "Academicos",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departamentos",
                table: "Departamentos",
                column: "DepartamentoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Departamentos_DepartamentoID",
                table: "Cursos",
                column: "DepartamentoID",
                principalTable: "Departamentos",
                principalColumn: "DepartamentoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Departamentos_Instituicoes_InstituicaoID",
                table: "Departamentos",
                column: "InstituicaoID",
                principalTable: "Instituicoes",
                principalColumn: "InstituicaoID");
        }
    }
}
