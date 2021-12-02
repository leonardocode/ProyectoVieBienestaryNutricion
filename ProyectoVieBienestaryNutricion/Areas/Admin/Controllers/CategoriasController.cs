

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ProyectoVieBienestaryNutricion.Data.Data.Repository;
using ProyectoVieBienestaryNutricion.Models;
using System;
using System.IO;
using System.Linq;

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
            if (ModelState.IsValid)
            {               
                //subida de archivos
                //obtenemos la ruta principal
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                //obtenemos el archivo
                var archivos = HttpContext.Request.Form.Files;                

                if (ocategoriaCLS.Id == 0)
                {
                    //nueva categoria
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
                //subida de archivos
                //obtenemos la ruta principal
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                //obtenemos el archivo
                var archivos = HttpContext.Request.Form.Files;

                //acceder a bd
                var categoriadesdeBD = _contenedorTrabajo.Categoria.GetCategoria(ocategoriaCLS.Id);

                if (archivos.Count > 0)
                {
                    //si si se mando un archivo, editamos imagen
                    string nombreArchivo = Guid.NewGuid().ToString();
                    //pasamos la ruta en donde se van aguardar los productos registrados
                    var subidas = Path.Combine(rutaPrincipal, @"img\categorias\");
                    //obtenemos la extension del archivo
                    var extension = Path.GetExtension(archivos[0].FileName);
                    //nueva extension
                    var nuevaExtencion = Path.GetExtension(archivos[0].FileName);

                    //ruta Imagen
                    var rutaImagen = Path.Combine(rutaPrincipal, categoriadesdeBD.UrlImagen.TrimStart('\\'));

                    //si la imagen ya existe reemplacela por la nueva
                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                    //subimos nuevamente el archivo dentro de la ubicacion dada junto con la extension
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + nuevaExtencion), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    ocategoriaCLS.UrlImagen = @"\img\categorias\" + nombreArchivo + nuevaExtencion;
                    ocategoriaCLS.FechaRegistroCategoria = DateTime.Now;
                    _contenedorTrabajo.Categoria.ActualizarCategoria(ocategoriaCLS);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));

                }
                else
                {                    
                    //cuando la imagen es la misma ya existe y no se reemplaza, debe conservar la de base de datos
                    ocategoriaCLS.UrlImagen = categoriadesdeBD.UrlImagen;
                }

                //si editamos solo los campos y no la imagen
                _contenedorTrabajo.Categoria.ActualizarCategoria(ocategoriaCLS);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));

            }
            else
            {               
                return View(ocategoriaCLS);
            }            
            return View(ocategoriaCLS);
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
            var categoriaDesdeDb = _contenedorTrabajo.Categoria.GetCategoria(id);
            string rutaDirectorioPrincipal = _hostingEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, categoriaDesdeDb.UrlImagen.TrimStart('\\'));

            //si existe el archivo
            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }

            if(categoriaDesdeDb == null)
            {
                return View();
            }

            _contenedorTrabajo.Categoria.BorrarCategoria(categoriaDesdeDb);
            _contenedorTrabajo.Save();
            return RedirectToAction(nameof(Index));
        }




    }
}
