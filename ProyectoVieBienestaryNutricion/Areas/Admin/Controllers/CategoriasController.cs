using Microsoft.AspNetCore.Mvc;
using ProyectoVieBienestaryNutricion.Data.Data.Repository;
using ProyectoVieBienestaryNutricion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ProyectoVieBienestaryNutricion.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriasController : Controller
    {
        //contiene todos los repositorios
        private readonly IContenedorTrabajo _contenedorTrabajo;

        //usamos la libreria para subirarchivos
        private readonly IWebHostEnvironment _hostingEnvironment;


        public CategoriasController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostingEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnvironment = hostingEnvironment;
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
                //subida de archivos
                //obtenemos la ruta principal
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                //obtenemos el archivo
                var archivos = HttpContext.Request.Form.Files;

                if (ocategoriaCLS.Id == 0)
                {
                    //nuevo producto
                    string nombreArchivo = Guid.NewGuid().ToString();
                    //pasamos la ruta en donde se van aguardar los productos registrados
                    var subidas = Path.Combine(rutaPrincipal, @"img\categorias");
                    //obtenemos la extension del archivo
                    var extension = Path.GetExtension(archivos[0].FileName);

                    //crea el archivo dentro de la ubicacion dada junto con la extension
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    ocategoriaCLS.UrlImagen = @"\img\categorias\" + nombreArchivo + extension;
                    ocategoriaCLS.FechaRegistroCategoria = DateTime.Now;
                    _contenedorTrabajo.Categoria.CrearCategoria(ocategoriaCLS);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }               
            }
            else
            {
                return View(ocategoriaCLS);
            }

            return View(ocategoriaCLS);
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
