using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Stock.Models
{
    public partial class StockContext : IdentityDbContext
    {

        public StockContext(DbContextOptions<StockContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attributes> Attributes { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Catagory> Catagory { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Productbrand> Productbrand { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }
        public virtual DbSet<Stocks> Stocks { get; set; }
        public virtual DbSet<Suplier> Suplier { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(local); Initial Catalog=Stock; integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Attributes>(entity =>
            {
                entity.HasKey(e => e.AttributeId)
                    .HasName("PK__attribut__03B803D0158617C4");

                entity.ToTable("attributes");

                entity.Property(e => e.AttributeId)
                    .HasColumnName("attributeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Color)
                    .HasColumnName("color")
                    .HasMaxLength(30);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Size).HasColumnName("size");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("brand");

                entity.Property(e => e.Brandid)
                    .HasColumnName("brandid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Catagory>(entity =>
            {
                entity.ToTable("catagory");

                entity.Property(e => e.CatagoryId)
                    .HasColumnName("catagoryID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customerID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(100);

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__orders__0809337DDE9D401B");

                entity.ToTable("orders");

                entity.Property(e => e.OrderId)
                    .HasColumnName("orderID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CustomerId).HasColumnName("customerID");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__orders__customer__4CA06362");
            });


            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.ProductId)
                    .HasColumnName("productID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(60);
            });

            modelBuilder.Entity<Productbrand>(entity =>
            {
                entity.ToTable("productbrand");

                entity.Property(e => e.ProductbrandId).HasColumnName("productbrandID");

                entity.Property(e => e.AttributeId).HasColumnName("attributeID");

                entity.Property(e => e.BrandId).HasColumnName("brandID");

                entity.Property(e => e.CatagoryId).HasColumnName("catagoryID");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasMaxLength(100);

                entity.Property(e => e.ProductId).HasColumnName("productID");

                entity.HasOne(d => d.Attribute)
                    .WithMany(p => p.Productbrand)
                    .HasForeignKey(d => d.AttributeId)
                    .HasConstraintName("FK__productbr__attri__4BAC3F29");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Productbrand)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK__productbr__brand__5535A963");

                entity.HasOne(d => d.Catagory)
                    .WithMany(p => p.Productbrand)
                    .HasForeignKey(d => d.CatagoryId)
                    .HasConstraintName("FK__productbr__catag__5629CD9C");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Productbrand)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__productbr__produ__48CFD27E");
            });


            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductBrandId).HasColumnName("ProductBrandID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_OrderDetail_orders");

                entity.HasOne(d => d.ProductBrand)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.ProductBrandId)
                    .HasConstraintName("FK_OrderDetail_productbrand");
            });
            
            modelBuilder.Entity<Suplier>(entity =>
            {
                entity.ToTable("suplier");

                entity.Property(e => e.SuplierId)
                    .HasColumnName("suplierID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(100);

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payment");

                entity.Property(e => e.PaymentId)
                    .HasColumnName("paymentID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Credit).HasColumnName("credit");

                entity.Property(e => e.CustomerId).HasColumnName("customerID");

                entity.Property(e => e.Debit).HasColumnName("debit");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(100);

                entity.Property(e => e.SuplierId).HasColumnName("suplierID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__payment__custome__5BE2A6F2");

                entity.HasOne(d => d.Suplier)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.SuplierId)
                    .HasConstraintName("FK__payment__suplier__5165187F");
            });

            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.HasKey(e => e.PurchaseId);

                entity.Property(e => e.PurchaseId)
                    .HasColumnName("PurchaseID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.SuplierId).HasColumnName("SuplierID");

                entity.HasOne(d => d.Suplier)
                    .WithMany(p => p.PurchaseOrder)
                    .HasForeignKey(d => d.SuplierId)
                    .HasConstraintName("FK_PurchaseOrder_suplier");
            });

            modelBuilder.Entity<PurchaseOrderDetail>(entity =>
            {
                entity.Property(e => e.PurchaseOrderDetailId).HasColumnName("PurchaseOrderDetailID");

                entity.Property(e => e.PoductBrandId).HasColumnName("PoductBrandID");

                entity.Property(e => e.PurchaseId).HasColumnName("PurchaseID");

                entity.HasOne(d => d.PoductBrand)
                    .WithMany(p => p.PurchaseOrderDetail)
                    .HasForeignKey(d => d.PoductBrandId)
                    .HasConstraintName("FK_PurchaseOrderDetail_productbrand");

                entity.HasOne(d => d.Purchase)
                    .WithMany(p => p.PurchaseOrderDetail)
                    .HasForeignKey(d => d.PurchaseId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PurchaseOrderDetail_PurchaseOrder");
            });

            
            modelBuilder.Entity<Stocks>(entity =>
            {
                entity.HasKey(e => e.StockId)
                    .HasName("PK__stock__CBAD8743EA507320");

                entity.ToTable("stocks");

                entity.Property(e => e.StockId).HasColumnName("stockID");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ProductBrandId).HasColumnName("productBrandID");

                entity.Property(e => e.PurchaseId).HasColumnName("PurchaseID");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.ProductBrand)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.ProductBrandId)
                    .HasConstraintName("FK__stock__productBr__4F7CD00D");
            });
                                  
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__users__CB9A1CDF890C61CD");

                entity.ToTable("users");

                entity.Property(e => e.UserId)
                    .HasColumnName("userID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(30);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(30);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(30);
            });
        }
    }
}
