using ProyectoVieBienestaryNutricion.Data.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoVieBienestaryNutricion.Data.Data
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {

        private readonly ApplicationDbContext _bd;

        public ContenedorTrabajo(ApplicationDbContext bd)
        {
            _bd = bd;
            //instancia la categoria creada
            Categoria = new CategoriaRepository(_bd);
        }

        public ICategoriaRepository Categoria { get; set; }

        public void Dispose()
        {
            _bd.Dispose();
        }

        public void Save()
        {
            _bd.SaveChanges();
        }
    }
}
