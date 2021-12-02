using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoVieBienestaryNutricion.Models.ViewModels
{
   public class HomeVW
   {
        //contiene todos los modelos a mostrar en la vista principal
        public IEnumerable<SliderCLS> Sliders { get; set; }

        public IEnumerable<CategoriaCLS> Categorias { get; set; }
        
        public IEnumerable<ProductoCLS> Productos { get; set; }
   }
}
