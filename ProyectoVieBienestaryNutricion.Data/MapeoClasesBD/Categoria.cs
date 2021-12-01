using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ProyectoVieBienestaryNutricion.Data.MapeoClasesBD
{
    public partial class Categoria
    {
        public Categoria()
        {
            Producto = new HashSet<Producto>();
        }

        public int Id { get; set; }
        public string NombreCategoria { get; set; }
        public string DescripcionCategoria { get; set; }
        public DateTime FechaRegistroCategoria { get; set; }
        public bool Activo { get; set; }
        public string UrlImagen { get; set; }

        public virtual ICollection<Producto> Producto { get; set; }
    }
}
