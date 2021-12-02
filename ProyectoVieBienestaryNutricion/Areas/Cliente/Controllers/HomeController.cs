using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProyectoVieBienestaryNutricion.Data.Data.Repository;
using ProyectoVieBienestaryNutricion.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoVieBienestaryNutricion.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public HomeController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        public IActionResult Index()
        {
            //enviamos todo el modelo completo
            HomeVW homeVM = new HomeVW()
            {
                Sliders = _contenedorTrabajo.Slider.GetSliders(),
                Categorias = _contenedorTrabajo.Categoria.GetCategorias(),
                Productos = _contenedorTrabajo.Producto.GetProductos()
            };
            return View(homeVM);
        }

      
    }
}
