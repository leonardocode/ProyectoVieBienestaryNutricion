using ProyectoVieBienestaryNutricion.Data.Data.Repository;
using System;
using System.Collections.Generic;
using ProyectoVieBienestaryNutricion.Models;
using System.Text;
using System.Linq;

namespace ProyectoVieBienestaryNutricion.Data.Data
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ApplicationDbContext _bd;

        public CategoriaRepository(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool ActualizarCategoria(CategoriaCLS categoria)
        {
            _bd.Categoria.Update(categoria);
            return Guardar();
        }

        public bool BorrarCategoria(CategoriaCLS categoria)
        {
            _bd.Categoria.Remove(categoria);
            return Guardar();
        }

        public bool CrearCategoria(CategoriaCLS categoria)
        {
            _bd.Categoria.Add(categoria);
            return Guardar();
        }

        public bool ExisteCategoria(string nombre)
        {
            bool valor = _bd.Categoria.Any(c => c.NombreCategoria.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public bool ExisteCategoria(int id)
        {
            return _bd.Categoria.Any(c => c.Id == id);
        }

        public CategoriaCLS GetCategoria(int CategoriaId)
        {
            return _bd.Categoria.FirstOrDefault(c => c.Id == CategoriaId);
        }

        public ICollection<CategoriaCLS> GetCategorias()
        {
            return _bd.Categoria.OrderBy(c => c.NombreCategoria).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }
    }
}
