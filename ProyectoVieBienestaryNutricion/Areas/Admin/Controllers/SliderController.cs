using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ProyectoVieBienestaryNutricion.Data.Data.Repository;
using ProyectoVieBienestaryNutricion.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoVieBienestaryNutricion.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        //contiene todos los repositorios
        private readonly IContenedorTrabajo _contenedorTrabajo;

        //usamos la libreria para subirarchivos
        private readonly IWebHostEnvironment _hostingEnvironment;

        public SliderController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostingEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_contenedorTrabajo.Slider.GetSliders().ToList());
        }       


        [HttpGet]
        public IActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SliderCLS sliderCLS)
        {
            if (ModelState.IsValid)
            {               
                //subida de archivos
                //obtenemos la ruta principal
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                //obtenemos el archivo
                var archivos = HttpContext.Request.Form.Files;

                if (sliderCLS.Id == 0)
                {
                    //nuevo producto
                    string nombreArchivo = Guid.NewGuid().ToString();
                    //pasamos la ruta en donde se van aguardar los productos registrados
                    var subidas = Path.Combine(rutaPrincipal, @"img\slider");
                    //obtenemos la extension del archivo
                    var extension = Path.GetExtension(archivos[0].FileName);

                    //crea el archivo dentro de la ubicacion dada junto con la extension
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    sliderCLS.UrlImagen = @"\img\slider\" + nombreArchivo + extension;
                    sliderCLS.FechaRegistroSlider = DateTime.Now;
                    _contenedorTrabajo.Slider.CrearSlider(sliderCLS);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }


            }
            else
            {               
                return View(sliderCLS);
            }           
            return View(sliderCLS);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {            
            SliderCLS slider = new SliderCLS();
            slider = _contenedorTrabajo.Slider.GetSlider(id);
            if (slider == null)
            {                
                return NotFound();
            }
            else
            {               
                return View(slider);
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SliderCLS sliderCLS)
        {
            if (ModelState.IsValid)
            {                
                //subida de archivos
                //obtenemos la ruta principal
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                //obtenemos el archivo
                var archivos = HttpContext.Request.Form.Files;

                //acceder a bd
                var productodesdeBD = _contenedorTrabajo.Slider.GetSlider(sliderCLS.Id);

                if (archivos.Count > 0)
                {
                    //si si se mando un archivo, editamos imagen
                    string nombreArchivo = Guid.NewGuid().ToString();
                    //pasamos la ruta en donde se van aguardar los productos registrados
                    var subidas = Path.Combine(rutaPrincipal, @"img\slider\");
                    //obtenemos la extension del archivo
                    var extension = Path.GetExtension(archivos[0].FileName);
                    //nueva extension
                    var nuevaExtencion = Path.GetExtension(archivos[0].FileName);

                    //ruta Imagen
                    var rutaImagen = Path.Combine(rutaPrincipal, productodesdeBD.UrlImagen.TrimStart('\\'));

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

                    sliderCLS.UrlImagen = @"\img\slider\" + nombreArchivo + nuevaExtencion;
                    sliderCLS.FechaRegistroSlider = DateTime.Now;
                    _contenedorTrabajo.Slider.ActualizarSlider(sliderCLS);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));

                }
                else
                {                   
                    //cuando la imagen es la misma ya existe y no se reemplaza, debe conservar la de base de datos
                    sliderCLS.UrlImagen = productodesdeBD.UrlImagen;
                }

                //si editamos solo los campos y no la imagen
                _contenedorTrabajo.Slider.ActualizarSlider(sliderCLS);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));

            }
            else
            {                             
                return View(sliderCLS);
            }
            
            return View(sliderCLS);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var usuario = _contenedorTrabajo.Slider.GetSlider(id);

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
            var productoDesdeDb = _contenedorTrabajo.Slider.GetSlider(id);
            string rutaDirectorioPrincipal = _hostingEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, productoDesdeDb.UrlImagen.TrimStart('\\'));

            //si existe el archivo
            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }

            if (productoDesdeDb == null)
            {
                return View();
            }

            _contenedorTrabajo.Slider.BorrarSlider(productoDesdeDb);
            _contenedorTrabajo.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
