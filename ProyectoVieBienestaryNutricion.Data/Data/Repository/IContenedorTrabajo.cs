using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoVieBienestaryNutricion.Data.Data.Repository
{
    public interface IContenedorTrabajo :IDisposable
    {
        ICategoriaRepository Categoria { get; set; }

        IProductoRepository Producto { get; set; }

       ISliderRepository Slider { get; set; }

        void Save();
    }
}
