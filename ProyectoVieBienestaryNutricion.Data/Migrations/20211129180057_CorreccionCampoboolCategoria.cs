using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoVieBienestaryNutricion.Data.Migrations
{
    public partial class CorreccionCampoboolCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Activo",
                table: "Categoria",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Activo",
                table: "Categoria",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool));
        }
    }
}
