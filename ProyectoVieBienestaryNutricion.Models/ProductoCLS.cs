using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProyectoVieBienestaryNutricion.Models
{
    public class ProductoCLS
    {
        [Key]
        public int Id { get; set; }

        
        [Display(Name = "Nombre Producto")]
        [Required(ErrorMessage = "Nombre Producto es Obligatorio, no puede estar vacio")]
        public string NombreProducto { get; set; }


        [Display(Name = "Descripcion Producto")]
        [Required(ErrorMessage = "Descripcion Producto es Obligatorio, no puede estar vacio")]
        public string DescripcionProducto { get; set; }

                
        [Display(Name = "Fecha Registro Producto")]
        [Required(ErrorMessage = "Fecha Registro Producto es Obligatorio, No puede estar vacio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaRegistroProducto { get; set; }


        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        [Required(ErrorMessage = "Imagen del producto es obligatorio, no puede estar vacio")]
        public string UrlImagen { get; set; }

           

        [Required(ErrorMessage = "Categoria Activa es Obligatorio, No puede estar vacio")]
        [Display(Name = "Categoria Activa")]
        public bool Activo { get; set; }


        //se crea la relacion, una categoria puede estar en muchos productos
        [Required]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public CategoriaCLS Categoria { get; set; }

    }
}
