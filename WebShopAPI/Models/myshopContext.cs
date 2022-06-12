using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebshopAPI.Models
{
    public partial class myshopContext : DbContext
    {
        public myshopContext()
        {
        }

        public myshopContext(DbContextOptions<myshopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductStock> ProductStocks { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("server=localhost; user=root; pwd=gronic; database=myshop");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasColumnName("address")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Balance)
                    .HasColumnType("decimal(6,2)")
                    .HasColumnName("balance");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("email");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("firstname");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lastname");

                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .HasColumnName("phone")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Postcode)
                    .HasMaxLength(4)
                    .HasColumnName("postcode")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.State)
                    .HasMaxLength(3)
                    .HasColumnName("state")
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<InvoiceItem>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PRIMARY");

                entity.ToTable("invoice_items");

                entity.HasIndex(e => e.Invoice, "invoice");

                entity.HasIndex(e => e.Product, "product");

                entity.Property(e => e.RecordId).HasColumnName("record_id");

                entity.Property(e => e.Invoice).HasColumnName("invoice");

                entity.Property(e => e.Product)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasColumnName("product");

                entity.Property(e => e.SoldPrice)
                    .HasColumnType("decimal(5,2)")
                    .HasColumnName("sold_price");

                entity.HasOne(d => d.InvoiceNavigation)
                    .WithMany(p => p.InvoiceItems)
                    .HasForeignKey(d => d.Invoice)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("invoice_items_ibfk_1");

                entity.HasOne(d => d.ProductNavigation)
                    .WithMany(p => p.InvoiceItems)
                    .HasForeignKey(d => d.Product)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("invoice_items_ibfk_2");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductCode)
                    .HasName("PRIMARY");

                entity.ToTable("products");

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(3)
                    .HasColumnName("product_code");

                entity.Property(e => e.Category)
                    .HasMaxLength(10)
                    .HasColumnName("category")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");

                entity.Property(e => e.Image)
                    .HasMaxLength(30)
                    .HasColumnName("image")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(5,2)")
                    .HasColumnName("price");

                entity.Property(e => e.Quantity)
                    .HasMaxLength(10)
                    .HasColumnName("quantity");
            });

            modelBuilder.Entity<ProductStock>(entity =>
            {
                entity.HasKey(e => e.ProductCode)
                    .HasName("PRIMARY");

                entity.ToTable("product_stocks");

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(3)
                    .HasColumnName("product_code");

                entity.Property(e => e.Stock)
                    .HasColumnName("stock")
                    .HasDefaultValueSql("'10'");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.InvoiceNo)
                    .HasName("PRIMARY");

                entity.ToTable("transactions");

                entity.HasIndex(e => e.Customer, "customer");

                entity.Property(e => e.InvoiceNo).HasColumnName("invoice_no");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(5,2)")
                    .HasColumnName("amount");

                entity.Property(e => e.Customer).HasColumnName("customer");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.Customer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("transactions_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
