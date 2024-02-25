using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MigracionModelDB.DBModels;

public partial class DbproductContext : DbContext
{
    public DbproductContext()
    {
    }

    public DbproductContext(DbContextOptions<DbproductContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<InvoiceHead> InvoiceHeads { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<TUser> TUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=VMT-JMORAN\\SQLEXPRESS;Database=dbproduct;Trusted_Connection=True;TrustServerCertificate=true;Persist Security Info=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("category");

            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.ToTable("invoice_detail");

            entity.Property(e => e.InvoiceDetailId).HasColumnName("invoiceDetailId");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.InvoiceHeadId).HasColumnName("invoiceHeadId");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProductId).HasColumnName("productId");

            entity.HasOne(d => d.InvoiceHead).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.InvoiceHeadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_invoice_detail_invoice_head");
        });

        modelBuilder.Entity<InvoiceHead>(entity =>
        {
            entity.ToTable("invoice_head");

            entity.Property(e => e.InvoiceHeadId).HasColumnName("invoiceHeadId");
            entity.Property(e => e.DateTime)
                .HasColumnType("datetime")
                .HasColumnName("dateTime");
            entity.Property(e => e.Total).HasColumnName("total");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.InvoiceHeads)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_invoice_head_t_user");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("product");

            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_product_category");
        });

        modelBuilder.Entity<TUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("t_user");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.UserName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("userName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
