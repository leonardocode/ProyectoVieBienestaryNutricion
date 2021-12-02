using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoVieBienestaryNutricion.Data.Migrations
{
    public partial class CreacionEntidadSlider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Slider",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreSlider = table.Column<string>(nullable: false),
                    FechaRegistroSlider = table.Column<DateTime>(nullable: false),
                    UrlImagen = table.Column<string>(nullable: true),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slider", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slider");
        }
    }
}
