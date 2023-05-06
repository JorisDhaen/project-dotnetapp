using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Server.Models;

public partial class DelawareDbContext : DbContext
{
    public DelawareDbContext()
    {
    }

    public DelawareDbContext(DbContextOptions<DelawareDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderitem> Orderitems { get; set; }

    public virtual DbSet<Package> Packages { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;TrustServerCertificate=True;Database=DelawareDB;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Idnotification).HasName("PK_notification_idnotification");

            entity.ToTable("notification", "delaware_db");

            entity.HasIndex(e => e.OrderId, "orderNotific_idx");

            entity.Property(e => e.Idnotification)
                .HasMaxLength(20)
                .HasColumnName("idnotification");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.IsSeen).HasColumnName("isSeen");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.OrderId)
                .HasMaxLength(20)
                .HasColumnName("orderId");

            entity.HasOne(d => d.Order).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("notification$orderNotific");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Idorder).HasName("PK_order_idorder");

            entity.ToTable("order", "delaware_db");

            entity.HasIndex(e => e.PackageId, "package_idx");

            entity.HasIndex(e => e.UserId, "user_idx");

            entity.Property(e => e.Idorder)
                .HasMaxLength(20)
                .HasColumnName("idorder");
            entity.Property(e => e.DeliveryAdress)
                .HasMaxLength(45)
                .HasColumnName("deliveryAdress");
            entity.Property(e => e.InvoiceAdress)
                .HasMaxLength(45)
                .HasColumnName("invoiceAdress");
            entity.Property(e => e.NetPrice).HasColumnName("netPrice");
            entity.Property(e => e.OrderDate)
                .HasPrecision(0)
                .HasColumnName("orderDate");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(45)
                .HasColumnName("orderStatus");
            entity.Property(e => e.PackageId)
                .HasMaxLength(20)
                .HasColumnName("packageId");
            entity.Property(e => e.TaxAmount).HasColumnName("taxAmount");
            entity.Property(e => e.TotalAmount).HasColumnName("totalAmount");
            entity.Property(e => e.UserId)
                .HasMaxLength(20)
                .HasColumnName("userId");

            entity.HasOne(d => d.Package).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order$package");
        });

        modelBuilder.Entity<Orderitem>(entity =>
        {
            entity.HasKey(e => e.IdorderItem).HasName("PK_orderitem_idorderItem");

            entity.ToTable("orderitem", "delaware_db");

            entity.HasIndex(e => e.OrderId, "orderOrderItem_idx");

            entity.HasIndex(e => e.ProductId, "product_idx");

            entity.Property(e => e.IdorderItem)
                .HasMaxLength(20)
                .HasColumnName("idorderItem");
            entity.Property(e => e.OrderId)
                .HasMaxLength(20)
                .HasColumnName("orderId");
            entity.Property(e => e.ProductId)
                .HasMaxLength(20)
                .HasColumnName("productId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.TotalPrice).HasColumnName("totalPrice");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("orderitem$orderOrderItem");

            entity.HasOne(d => d.Product).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderitem$product");
        });

        modelBuilder.Entity<Package>(entity =>
        {
            entity.HasKey(e => e.Idpackage).HasName("PK_package_idpackage");

            entity.ToTable("package", "delaware_db");

            entity.Property(e => e.Idpackage)
                .HasMaxLength(20)
                .HasColumnName("idpackage");
            entity.Property(e => e.Debth).HasColumnName("debth");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Width).HasColumnName("width");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Idproduct).HasName("PK_product_idproduct");

            entity.ToTable("product", "delaware_db");

            entity.HasIndex(e => e.SupplierId, "supplier_idx");

            entity.Property(e => e.Idproduct)
                .HasMaxLength(20)
                .HasColumnName("idproduct");
            entity.Property(e => e.Depth).HasColumnName("depth");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.LeftInStock).HasColumnName("leftInStock");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.ProductCatergoryId).HasColumnName("productCatergoryId");
            entity.Property(e => e.SupplierId)
                .HasMaxLength(20)
                .HasColumnName("supplierId");
            entity.Property(e => e.UnitPrice).HasColumnName("unitPrice");
            entity.Property(e => e.Width).HasColumnName("width");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product$supplier");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Idsupplier).HasName("PK_supplier_idsupplier");

            entity.ToTable("supplier", "delaware_db");

            entity.Property(e => e.Idsupplier)
                .HasMaxLength(20)
                .HasColumnName("idsupplier");
            entity.Property(e => e.Adress)
                .HasMaxLength(45)
                .HasColumnName("adress");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Iduser).HasName("PK_user_iduser");

            entity.ToTable("user", "delaware_db");

            entity.Property(e => e.Iduser)
                .HasMaxLength(45)
                .HasColumnName("iduser");
            entity.Property(e => e.Adress)
                .HasMaxLength(45)
                .HasColumnName("adress");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Username)
                .HasMaxLength(16)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
