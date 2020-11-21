using PIM.Pessoas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PIM.Pessoas.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Telefone> Telefones { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseSqlServer(
                    @"Server=lslucas9.database.windows.net;Database=clientes;Uid=lslucas;Pwd=mvdbt9@123;",
                    providerOptions => providerOptions.CommandTimeout(60)
                );

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacionamento pessoa <=> endereco
            modelBuilder.Entity<Pessoa>()
                .HasOne(s => s.Endereco)
                .WithOne(ad => ad.Pessoa)
                .HasForeignKey<Endereco>(ad => ad.PessoaREF)
                .HasPrincipalKey<Pessoa>(c => c.Id)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento telefone <=> pessoa
            modelBuilder.Entity<Pessoa>()
                .HasMany(s => s.Telefones)
                .WithOne(ad => ad.Pessoa)
                .HasForeignKey(ad => ad.IdPessoa)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento telefone <=>tipoTelefone
            modelBuilder.Entity<Telefone>()
                .HasOne(s => s.Tipo)
                .WithOne(ad => ad.Telefone)
                .HasForeignKey<TipoTelefone>(ad => ad.Id)
                .HasPrincipalKey<Telefone>(c => c.TipoId);

            modelBuilder.Entity<Pessoa>().ToTable("Pessoa");
            modelBuilder.Entity<Endereco>().ToTable("Endereco");
            modelBuilder.Entity<Telefone>().ToTable("Telefone");
        }
    }
}

