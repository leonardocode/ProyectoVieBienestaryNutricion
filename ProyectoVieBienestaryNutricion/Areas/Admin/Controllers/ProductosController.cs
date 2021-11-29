using Microsoft.AspNetCore.Mvc;
using ProyectoVieBienestaryNutricion.Data.Data.Repository;
using ProyectoVieBienestaryNutricion.Models;
using ProyectoVieBienestaryNutricion.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoVieBienestaryNutricion.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductosController : Controller
    {
        //contiene todos los repositorios
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public ProductosController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }


        [HttpGet]
        public IActionResult Index()
        {
            ProductoVM productoVM = new ProductoVM()
            {
                Producto = new Models.ProductoCLS(),
                ListaCategorias = (IEnumerable<System.Web.Mvc.SelectListItem>)_contenedorTrabajo.Categoria.GetCategorias()
            };
            return View(productoVM);
        }
           

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductoCLS productoCLS)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Producto.CrearProducto(productoCLS);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(productoCLS);
            }
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            ProductoCLS producto = new ProductoCLS();
            producto = _contenedorTrabajo.Producto.GetProducto(id);
            if (producto == null)
            {
                return NotFound();
            }
            else
            {
                return View(producto);
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductoCLS productoCLS)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Producto.ActualizarProducto(productoCLS);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(productoCLS);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var usuario = _contenedorTrabajo.Producto.GetProducto(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteRegistro(int id)
        {
            var usuario = _contenedorTrabajo.Producto.GetProducto(id);

            if (usuario == null)
            {
                return View();
            }
            _contenedorTrabajo.Producto.BorrarProducto(usuario);
            _contenedorTrabajo.Save();
            return RedirectToAction(nameof(Index));
        }

    }
}
