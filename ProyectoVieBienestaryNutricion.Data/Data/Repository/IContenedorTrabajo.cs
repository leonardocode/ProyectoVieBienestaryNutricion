using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoVieBienestaryNutricion.Data.Data.Repository
{
    public interface IContenedorTrabajo :IDisposable
    {
        ICategoriaRepository Categoria { get; set; }

        void Save();
    }
}
