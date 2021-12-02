using ProyectoVieBienestaryNutricion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProyectoVieBienestaryNutricion.Data.Data.Repository
{
    public class SliderRepository :ISliderRepository
    {
        private readonly ApplicationDbContext _bd;

        public SliderRepository(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public void ActualizarSlider(SliderCLS slider)
        {
            var objDesdedb = _bd.Slider.FirstOrDefault(s => s.Id == slider.Id);
            objDesdedb.NombreSlider = slider.NombreSlider;
            objDesdedb.FechaRegistroSlider = slider.FechaRegistroSlider;
            objDesdedb.UrlImagen = slider.UrlImagen;           
            objDesdedb.Activo = slider.Activo;           
        }

        public bool BorrarSlider(SliderCLS slider)
        {
            _bd.Slider.Remove(slider);
            return Guardar();
        }

        public bool CrearSlider(SliderCLS slider)
        {
            _bd.Slider.Add(slider);
            return Guardar();
        }

        public bool ExisteSlider(string nombre)
        {
            bool valor = _bd.Slider.Any(c => c.NombreSlider.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public bool ExisteSlider(int id)
        {
            return _bd.Slider.Any(c => c.Id == id);
        }

        public SliderCLS GetSlider(int sliderId)
        {
            return _bd.Slider.FirstOrDefault(c => c.Id == sliderId);
        }

        public ICollection<SliderCLS> GetSliders()
        {
            return _bd.Slider.OrderBy(c => c.NombreSlider).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }
    }
}
