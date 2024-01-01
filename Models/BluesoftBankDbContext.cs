using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Bank.Models;

public partial class BluesoftBankDbContext : DbContext
{
    public BluesoftBankDbContext()
    {
    }

    public BluesoftBankDbContext(DbContextOptions<BluesoftBankDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ciudad> Ciudads { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Cuenta> Cuentas { get; set; }

    public virtual DbSet<TipoCuentum> TipoCuenta { get; set; }

    public virtual DbSet<Transaccione> Transacciones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);Database=BancoDB;Trusted_Connection=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ciudad>(entity =>
        {
            entity.HasKey(e => e.CiudadId).HasName("PK__Ciudad__E826E77097A19551");

            entity.ToTable("Ciudad");

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__Clientes__71ABD0A7027B7250");

            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.Ciudad).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.CiudadId)
                .HasConstraintName("FK__Clientes__Ciudad__0B91BA14");
        });

        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.HasKey(e => e.CuentaId).HasName("PK__Cuentas__40072EA1BEC75A40");

            entity.Property(e => e.CuentaId).HasColumnName("CuentaID");
            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.Saldo).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TipoCuentaId).HasColumnName("TipoCuentaID");

            entity.HasOne(d => d.Ciudad).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.CiudadId)
                .HasConstraintName("FK__Cuentas__CiudadI__10566F31");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK__Cuentas__Cliente__0F624AF8");

            entity.HasOne(d => d.TipoCuenta).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.TipoCuentaId)
                .HasConstraintName("FK__Cuentas__TipoCue__0E6E26BF");
        });

        modelBuilder.Entity<TipoCuentum>(entity =>
        {
            entity.HasKey(e => e.TipoCuentaId).HasName("PK__TipoCuen__B3998D140903B907");

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Transaccione>(entity =>
        {
            entity.HasKey(e => e.TransaccionId).HasName("PK__Transacc__86A849DEBB4F388A");

            entity.Property(e => e.TransaccionId).HasColumnName("TransaccionID");
            entity.Property(e => e.CuentaId).HasColumnName("CuentaID");
            entity.Property(e => e.FechaTransaccion).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Tipo).HasMaxLength(20);

            entity.HasOne(d => d.Ciudad).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.CiudadId)
                .HasConstraintName("FK__Transacci__Ciuda__14270015");

            entity.HasOne(d => d.Cuenta).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.CuentaId)
                .HasConstraintName("FK__Transacci__Cuent__1332DBDC");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
