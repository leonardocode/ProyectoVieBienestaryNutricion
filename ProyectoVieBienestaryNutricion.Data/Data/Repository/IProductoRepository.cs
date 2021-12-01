using ProyectoVieBienestaryNutricion.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoVieBienestaryNutricion.Data.Data.Repository
{
    public interface IProductoRepository
    {
        ICollection<ProductoCLS> GetProductos();
        ProductoCLS GetProducto(int ProductoId);
        bool ExisteProducto(string nombre);
        bool ExisteProducto(int id);
        bool CrearProducto(ProductoCLS producto);
        void ActualizarProducto(ProductoCLS producto);
        bool BorrarProducto(ProductoCLS producto);
        bool Guardar();
    }
}
