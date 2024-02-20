using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Ecomerce.DBModels;

public partial class DbproductContext : DbContext
{

    public DbproductContext(DbContextOptions<DbproductContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<InvoiceHead> InvoiceHeads { get; set; }

    public virtual DbSet<Product> Products { get; set; }

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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
