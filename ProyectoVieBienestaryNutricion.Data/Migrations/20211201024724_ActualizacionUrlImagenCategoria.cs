using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoVieBienestaryNutricion.Data.Migrations
{
    public partial class ActualizacionUrlImagenCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UrlImagen",
                table: "Producto",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UrlImagen",
                table: "Categoria",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlImagen",
                table: "Categoria");

            migrationBuilder.AlterColumn<string>(
                name: "UrlImagen",
                table: "Producto",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
