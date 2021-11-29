using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoVieBienestaryNutricion.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoVieBienestaryNutricion.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //Se agrega los repositorys creados de la aplicacion
        public DbSet<CategoriaCLS> Categoria { get; set; }
    }
}
