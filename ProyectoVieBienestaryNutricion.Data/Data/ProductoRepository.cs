using ProyectoVieBienestaryNutricion.Data.Data.Repository;
using ProyectoVieBienestaryNutricion.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ProyectoVieBienestaryNutricion.Data.Data
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ApplicationDbContext _bd;

        public ProductoRepository(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool ActualizarProducto(ProductoCLS producto)
        {
            _bd.Producto.Update(producto);
            return Guardar();
        }

        public bool BorrarProducto(ProductoCLS producto)
        {
            _bd.Producto.Remove(producto);
            return Guardar();
        }

        public bool CrearProducto(ProductoCLS producto)
        {
            _bd.Producto.Add(producto);
            return Guardar();
        }

        public bool ExisteProducto(string nombre)
        {
            bool valor = _bd.Producto.Any(c => c.NombreProducto.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public bool ExisteProducto(int id)
        {
            return _bd.Producto.Any(c => c.Id == id);
        }

        public ProductoCLS GetProducto(int ProductoId)
        {
            return _bd.Producto.FirstOrDefault(c => c.Id == ProductoId);
        }

        public ICollection<ProductoCLS> GetProductos()
        {
            return _bd.Producto.OrderBy(c => c.NombreProducto).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }
    }
}
