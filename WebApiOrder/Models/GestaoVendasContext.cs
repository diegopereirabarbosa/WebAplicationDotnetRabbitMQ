using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApiOrder.Models;

public partial class GestaoVendasContext : DbContext
{

    public GestaoVendasContext(DbContextOptions<GestaoVendasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Iten> Itens { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseNpgsql("Host=btg-postgres;port=5432;Database=gestaoVendas;User Id=postgres;Password=root");
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                           .AddJsonFile("appsettings.json")
                           .Build();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), action => action.EnableRetryOnFailure(maxRetryCount: 3));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.CodigoCliente).HasName("Clientes_pkey");

            entity.Property(e => e.CodigoCliente)
                .UseIdentityAlwaysColumn()
                .HasColumnName("codigoCliente");
            entity.Property(e => e.ApelidoFantasia)
                .HasMaxLength(60)
                .IsFixedLength()
                .HasColumnName("apelidoFantasia");
            entity.Property(e => e.CpfCnpj)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("cpfCnpj");
            entity.Property(e => e.NomeRazaoSocial)
                .HasMaxLength(60)
                .IsFixedLength()
                .HasColumnName("nomeRazaoSocial");
            entity.Property(e => e.RgInscricaoEstadual)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("rgInscricaoEstadual");
        });

        modelBuilder.Entity<Iten>(entity =>
        {
            entity.HasKey(e => e.CodigoItem).HasName("Itens_pkey");

            entity.Property(e => e.CodigoItem)
                .UseIdentityAlwaysColumn()
                .HasColumnName("codigoItem");
            entity.Property(e => e.CodigoPedido).HasColumnName("codigoPedido");
            entity.Property(e => e.Preco).HasColumnName("preco");
            entity.Property(e => e.Produto)
                .HasMaxLength(60)
                .IsFixedLength()
                .HasColumnName("produto");
            entity.Property(e => e.Quantidade).HasColumnName("quantidade");

            entity.HasOne(d => d.CodigoPedidoNavigation).WithMany(p => p.Itens)
                .HasForeignKey(d => d.CodigoPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_codigoPedido");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.CodigoPedido).HasName("Pedidos_pkey");

            entity.Property(e => e.CodigoPedido)
                .UseIdentityAlwaysColumn()
                .HasColumnName("codigoPedido");
            entity.Property(e => e.CodigoCliente).HasColumnName("codigoCliente");

            entity.HasOne(d => d.CodigoClienteNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.CodigoCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_codigoCliente");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
