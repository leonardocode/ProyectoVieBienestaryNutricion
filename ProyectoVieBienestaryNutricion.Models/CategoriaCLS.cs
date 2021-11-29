using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProyectoVieBienestaryNutricion.Models
{
   public class CategoriaCLS
    {
        
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Nombre Categoria es Obligatorio, No puede estar vacio")]
        [Display(Name = "Nombre Categoria")]
        public string NombreCategoria { get; set; }


        [Required(ErrorMessage = "Descripcion Categoria es Obligatorio, No puede estar vacio")]
        [Display(Name = "Descripcion Categoria")]
        public string DescripcionCategoria { get; set; }


        [Required(ErrorMessage = "Fecha Registro Categoria es Obligatorio, No puede estar vacio")]
        [Display(Name = "Fecha Registro Categoria")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaRegistroCategoria { get; set; }


        [Required(ErrorMessage = "Categoria Activa es Obligatorio, No puede estar vacio")]
        [Display(Name = "Categoria Activa")]
        public int Activo { get; set; }

    }
}
