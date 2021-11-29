using Microsoft.AspNetCore.Mvc;
using ProyectoVieBienestaryNutricion.Data.Data.Repository;
using ProyectoVieBienestaryNutricion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoVieBienestaryNutricion.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriasController : Controller
    {
        //contiene todos los repositorios
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public CategoriasController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }


      

        [HttpGet]
        public IActionResult Index()
        {
            return View(_contenedorTrabajo.Categoria.GetCategorias().ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoriaCLS ocategoriaCLS)
        {
            if(ModelState.IsValid)
            {
                _contenedorTrabajo.Categoria.CrearCategoria(ocategoriaCLS);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(ocategoriaCLS);
            }    
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            CategoriaCLS categoria = new CategoriaCLS();
            categoria = _contenedorTrabajo.Categoria.GetCategoria(id);
            if(categoria == null)
            {
                return NotFound();
            }else
            {
                return View(categoria);
            }
        }

                

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoriaCLS ocategoriaCLS)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Categoria.ActualizarCategoria(ocategoriaCLS);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(ocategoriaCLS);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var usuario = _contenedorTrabajo.Categoria.GetCategoria(id);

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
            var usuario = _contenedorTrabajo.Categoria.GetCategoria(id);

            if (usuario == null)
            {
                return View();
            }

            _contenedorTrabajo.Categoria.BorrarCategoria(usuario);
            _contenedorTrabajo.Save();
            return RedirectToAction(nameof(Index));
        }




    }
}
