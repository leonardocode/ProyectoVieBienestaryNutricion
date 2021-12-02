using ProyectoVieBienestaryNutricion.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoVieBienestaryNutricion.Data.Data.Repository
{
    public interface ISliderRepository
    {
        ICollection<SliderCLS> GetSliders();
        SliderCLS GetSlider(int sliderId);
        bool ExisteSlider(string nombre);
        bool ExisteSlider(int id);
        bool CrearSlider(SliderCLS slider);
        void ActualizarSlider(SliderCLS slider);
        bool BorrarSlider(SliderCLS slider);
        bool Guardar();
    }
}
