using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProyectoVieBienestaryNutricion.Models
{
    public class SliderCLS
    {
        [Key]
        public int Id { get; set; }


        [Display(Name = "Nombre Slider")]
        [Required(ErrorMessage = "Nombre Slider es obligatorio, no puede ser vacio")]
        public string NombreSlider { get; set; }


        [Required(ErrorMessage = "Fecha Registro Slider es Obligatorio, No puede estar vacio")]
        [Display(Name = "Fecha Registro Slider")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaRegistroSlider { get; set; }


        [DataType(DataType.ImageUrl)]
        [Display(Name = "UrlImagen")]
        public string UrlImagen { get; set; }



        [Display(Name = "Activo")]
        [Required(ErrorMessage ="Estado Activo o no es obligatorio, no puede ser vacio")]
        public bool Activo { get; set; }
        
    }
}
