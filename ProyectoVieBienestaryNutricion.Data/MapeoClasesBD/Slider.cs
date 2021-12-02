using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ProyectoVieBienestaryNutricion.Data.MapeoClasesBD
{
    public partial class Slider
    {
        public int Id { get; set; }
        public string NombreSlider { get; set; }
        public DateTime FechaRegistroSlider { get; set; }
        public string UrlImagen { get; set; }
        public bool Activo { get; set; }
    }
}
