using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogPessoal.Migrations
{
    /// <inheritdoc />
    public partial class PostagemIA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoriaIA",
                table: "Postagens",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ResumoIA",
                table: "Postagens",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TagsIA",
                table: "Postagens",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoriaIA",
                table: "Postagens");

            migrationBuilder.DropColumn(
                name: "ResumoIA",
                table: "Postagens");

            migrationBuilder.DropColumn(
                name: "TagsIA",
                table: "Postagens");
        }
    }
}
