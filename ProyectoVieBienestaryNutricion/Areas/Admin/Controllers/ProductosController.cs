using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoVieBienestaryNutricion.Data.Data.Repository;
using ProyectoVieBienestaryNutricion.Models;
using ProyectoVieBienestaryNutricion.Models.MapeoClasesBD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoVieBienestaryNutricion.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductosController : Controller
    {
        //contiene todos los repositorios
        private readonly IContenedorTrabajo _contenedorTrabajo;

        //usamos la libreria para subirarchivos
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductosController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostingEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View(_contenedorTrabajo.Producto.GetProductos().ToList());
        }

        public void llenarCategoria()
        {
            List<SelectListItem> listaCategoria = new List<SelectListItem>();
            using (VieBienestaryNutricionDBContext db= new VieBienestaryNutricionDBContext())
            {
                listaCategoria = (from categoria in db.Categoria
                                  where categoria.Activo == true
                                  select new SelectListItem
                                  {
                                      Text = categoria.NombreCategoria,
                                      Value = categoria.Id.ToString()
                                  }).ToList();
            }
            ViewBag.listaCategoria = listaCategoria;
        }
              

        [HttpGet]
        public IActionResult Create()
        {
            llenarCategoria();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductoCLS productoCLS)
        {
            if (ModelState.IsValid)
            {
                llenarCategoria();
                //subida de archivos
                //obtenemos la ruta principal
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                //obtenemos el archivo
                var archivos = HttpContext.Request.Form.Files;

                if(productoCLS.Id == 0)
                {
                    //nuevo producto
                    string nombreArchivo = Guid.NewGuid().ToString();
                    //pasamos la ruta en donde se van aguardar los productos registrados
                    var subidas = Path.Combine(rutaPrincipal, @"img\productos");
                    //obtenemos la extension del archivo
                    var extension = Path.GetExtension(archivos[0].FileName);

                    //crea el archivo dentro de la ubicacion dada junto con la extension
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    productoCLS.UrlImagen = @"\img\productos\" + nombreArchivo + extension;
                    productoCLS.FechaRegistroProducto = DateTime.Now;
                    _contenedorTrabajo.Producto.CrearProducto(productoCLS);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
                               
                
            }
            else
            {
                llenarCategoria();
                return View(productoCLS);
            }
            llenarCategoria();
            return View(productoCLS);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            llenarCategoria();
            ProductoCLS producto = new ProductoCLS();
            producto = _contenedorTrabajo.Producto.GetProducto(id);
            if (producto == null)
            {
                llenarCategoria();
                return NotFound();
            }
            else
            {
                llenarCategoria();
                return View(producto);
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductoCLS productoCLS)
        {
            if (ModelState.IsValid)
            {
                llenarCategoria();
                //subida de archivos
                //obtenemos la ruta principal
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                //obtenemos el archivo
                var archivos = HttpContext.Request.Form.Files;

                //acceder a bd
                var productodesdeBD = _contenedorTrabajo.Producto.GetProducto(productoCLS.Id);

                if (archivos.Count > 0)
                {
                    //si si se mando un archivo, editamos imagen
                    string nombreArchivo = Guid.NewGuid().ToString();
                    //pasamos la ruta en donde se van aguardar los productos registrados
                    var subidas = Path.Combine(rutaPrincipal, @"img\productos\");
                    //obtenemos la extension del archivo
                    var extension = Path.GetExtension(archivos[0].FileName);
                    //nueva extension
                    var nuevaExtencion = Path.GetExtension(archivos[0].FileName);

                    //ruta Imagen
                    var rutaImagen = Path.Combine(rutaPrincipal, productodesdeBD.UrlImagen.TrimStart('\\'));

                    //si la imagen ya existe reemplacela por la nueva
                    if(System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                    //subimos nuevamente el archivo dentro de la ubicacion dada junto con la extension
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + nuevaExtencion), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    productoCLS.UrlImagen = @"\img\productos\" + nombreArchivo + nuevaExtencion;
                    productoCLS.FechaRegistroProducto = DateTime.Now;
                    _contenedorTrabajo.Producto.ActualizarProducto(productoCLS);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    llenarCategoria();
                    //cuando la imagen es la misma ya existe y no se reemplaza, debe conservar la de base de datos
                    productoCLS.UrlImagen = productodesdeBD.UrlImagen;
                }

                //si editamos solo los campos y no la imagen
                _contenedorTrabajo.Producto.ActualizarProducto(productoCLS);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));

            }
            else
            {
                
                llenarCategoria();                
                return View(productoCLS);
            }
            llenarCategoria();
            return View(productoCLS);
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
