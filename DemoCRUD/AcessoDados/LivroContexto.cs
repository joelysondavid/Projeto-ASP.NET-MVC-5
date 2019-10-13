using DemoCRUD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace DemoCRUD.AcessoDados
{
    public class LivroContexto : DbContext
    {
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Livro> Livros { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // removendo a pluralização automatica do entity
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // configurando para que todos os campos 'string' tenham tamanho maximo de 100 caracteres
            modelBuilder.Properties<string>().Configure(c => c.HasMaxLength(100));
        }
    }
}