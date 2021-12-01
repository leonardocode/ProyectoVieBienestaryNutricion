﻿using System;
using System.Collections.Generic;
using ProyectoVieBienestaryNutricion.Models;
using System.Text;
using System.Web.Mvc;

namespace ProyectoVieBienestaryNutricion.Data.Data.Repository
{
    public interface ICategoriaRepository
    {
        IEnumerable<SelectListItem> GetListaCategorias();
        ICollection<CategoriaCLS> GetCategorias();
        CategoriaCLS GetCategoria(int CategoriaId);
        bool ExisteCategoria(string nombre);
        bool ExisteCategoria(int id);
        bool CrearCategoria(CategoriaCLS categoria);
        bool ActualizarCategoria(CategoriaCLS categoria);
        bool BorrarCategoria(CategoriaCLS categoria);
        bool Guardar();
    }
}
