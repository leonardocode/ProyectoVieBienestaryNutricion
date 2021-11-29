using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace ProyectoVieBienestaryNutricion.Models.ViewModels
{
   public class ProductoVM
   {
        //cobinamos todas las tablas
        public ProductoCLS Producto { get; set; }

        public IEnumerable<SelectListItem> ListaCategorias { get; set; }
    }
}
