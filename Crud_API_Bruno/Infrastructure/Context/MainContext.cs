using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Crud_API_Bruno.Infrastructure.Mappings;
using Crud_API_Bruno.Domain.Users;
using Crud_API_Bruno.Domain.Products;
using Crud_API_Bruno.Domain.Produtos.ProdutoImagem;

namespace Crud_API_Bruno.Infrastructure.Context
{
    public class MainContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<ProdutosCategorias> ProdutosCategorias { get; set; }
        public virtual DbSet<ProdutoImagem> ProdutoImagem { get; set; }
        public MainContext(DbContextOptions<MainContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserMap());
            builder.ApplyConfiguration(new ProdutoMap());
            builder.ApplyConfiguration(new CategoriaMap());
            builder.ApplyConfiguration(new ProdutosCategoriasMap());
            builder.ApplyConfiguration(new ProdutoImagemMap());
            
        }

    }
}