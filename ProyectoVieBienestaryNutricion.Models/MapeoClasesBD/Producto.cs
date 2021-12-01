using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ProyectoVieBienestaryNutricion.Models.MapeoClasesBD
{
    public partial class Producto
    {
        public int Id { get; set; }
        public string NombreProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public DateTime FechaRegistroProducto { get; set; }
        public string UrlImagen { get; set; }
        public bool Activo { get; set; }
        public int CategoriaId { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}
