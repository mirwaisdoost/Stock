﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Stock.Models;

namespace Stock.Migrations
{
    [DbContext(typeof(StockContext))]
    [Migration("20191225215357_Addidentity1")]
    partial class Addidentity1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Stock.Models.Attributes", b =>
                {
                    b.Property<int>("AttributeId")
                        .HasColumnName("attributeID");

                    b.Property<string>("Color")
                        .HasColumnName("color")
                        .HasMaxLength(30);

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<int?>("Size")
                        .HasColumnName("size");

                    b.HasKey("AttributeId")
                        .HasName("PK__attribut__03B803D0158617C4");

                    b.ToTable("attributes");
                });

            modelBuilder.Entity("Stock.Models.Brand", b =>
                {
                    b.Property<int>("Brandid")
                        .HasColumnName("brandid");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.HasKey("Brandid");

                    b.ToTable("brand");
                });

            modelBuilder.Entity("Stock.Models.Catagory", b =>
                {
                    b.Property<int>("CatagoryId")
                        .HasColumnName("catagoryID");

                    b.Property<string>("Name")
                        .HasMaxLength(30);

                    b.HasKey("CatagoryId");

                    b.ToTable("catagory");
                });

            modelBuilder.Entity("Stock.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnName("customerID");

                    b.Property<string>("Address")
                        .HasColumnName("address")
                        .HasMaxLength(100);

                    b.Property<double?>("Balance")
                        .HasColumnName("balance");

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasMaxLength(70)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<string>("Phone")
                        .HasColumnName("phone")
                        .HasMaxLength(20);

                    b.HasKey("CustomerId");

                    b.ToTable("customer");
                });

            modelBuilder.Entity("Stock.Models.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("OrderId")
                        .HasColumnName("OrderID");

                    b.Property<int?>("ProductBrandId")
                        .HasColumnName("ProductBrandID");

                    b.Property<int?>("Quantity");

                    b.Property<double?>("UnitPrice");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductBrandId");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("Stock.Models.Orders", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnName("orderID");

                    b.Property<int?>("CustomerId")
                        .HasColumnName("customerID");

                    b.Property<DateTime?>("Date")
                        .HasColumnName("date")
                        .HasColumnType("date");

                    b.HasKey("OrderId")
                        .HasName("PK__orders__0809337DDE9D401B");

                    b.HasIndex("CustomerId");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("Stock.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .HasColumnName("paymentID");

                    b.Property<double?>("Credit")
                        .HasColumnName("credit");

                    b.Property<int?>("CustomerId")
                        .HasColumnName("customerID");

                    b.Property<double?>("Debit")
                        .HasColumnName("debit");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(100);

                    b.Property<int?>("SuplierId")
                        .HasColumnName("suplierID");

                    b.HasKey("PaymentId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("SuplierId");

                    b.ToTable("payment");
                });

            modelBuilder.Entity("Stock.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnName("productID");

                    b.Property<string>("Name")
                        .HasMaxLength(60);

                    b.HasKey("ProductId");

                    b.ToTable("product");
                });

            modelBuilder.Entity("Stock.Models.Productbrand", b =>
                {
                    b.Property<int>("ProductbrandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("productbrandID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AttributeId")
                        .HasColumnName("attributeID");

                    b.Property<int?>("BrandId")
                        .HasColumnName("brandID");

                    b.Property<int?>("CatagoryId")
                        .HasColumnName("catagoryID");

                    b.Property<string>("Description")
                        .HasMaxLength(100);

                    b.Property<string>("Image")
                        .HasColumnName("image")
                        .HasMaxLength(100);

                    b.Property<int?>("ProductId")
                        .HasColumnName("productID");

                    b.HasKey("ProductbrandId");

                    b.HasIndex("AttributeId");

                    b.HasIndex("BrandId");

                    b.HasIndex("CatagoryId");

                    b.HasIndex("ProductId");

                    b.ToTable("productbrand");
                });

            modelBuilder.Entity("Stock.Models.PurchaseOrder", b =>
                {
                    b.Property<int>("PurchaseId")
                        .HasColumnName("PurchaseID");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime");

                    b.Property<int?>("Status");

                    b.Property<int?>("SuplierId")
                        .HasColumnName("SuplierID");

                    b.HasKey("PurchaseId");

                    b.HasIndex("SuplierId");

                    b.ToTable("PurchaseOrder");
                });

            modelBuilder.Entity("Stock.Models.PurchaseOrderDetail", b =>
                {
                    b.Property<int>("PurchaseOrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("PurchaseOrderDetailID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PoductBrandId")
                        .HasColumnName("PoductBrandID");

                    b.Property<int?>("PurchaseId")
                        .HasColumnName("PurchaseID");

                    b.Property<int?>("Quantity");

                    b.Property<double?>("UnitPrice");

                    b.HasKey("PurchaseOrderDetailId");

                    b.HasIndex("PoductBrandId");

                    b.HasIndex("PurchaseId");

                    b.ToTable("PurchaseOrderDetail");
                });

            modelBuilder.Entity("Stock.Models.Stocks", b =>
                {
                    b.Property<int>("StockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("stockID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double?>("Price")
                        .HasColumnName("price");

                    b.Property<int?>("ProductBrandId")
                        .HasColumnName("productBrandID");

                    b.Property<int?>("PurchaseId")
                        .HasColumnName("PurchaseID");

                    b.Property<int?>("Quantity")
                        .HasColumnName("quantity");

                    b.HasKey("StockId")
                        .HasName("PK__stock__CBAD8743EA507320");

                    b.HasIndex("ProductBrandId");

                    b.ToTable("stocks");
                });

            modelBuilder.Entity("Stock.Models.Suplier", b =>
                {
                    b.Property<int>("SuplierId")
                        .HasColumnName("suplierID");

                    b.Property<string>("Address")
                        .HasColumnName("address")
                        .HasMaxLength(100);

                    b.Property<double?>("Balance")
                        .HasColumnName("balance");

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasMaxLength(70)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<string>("Phone")
                        .HasColumnName("phone")
                        .HasMaxLength(20);

                    b.HasKey("SuplierId");

                    b.ToTable("suplier");
                });

            modelBuilder.Entity("Stock.Models.Users", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnName("userID");

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasMaxLength(70)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasMaxLength(30);

                    b.Property<string>("Password")
                        .HasColumnName("password")
                        .HasMaxLength(30);

                    b.Property<string>("Username")
                        .HasColumnName("username")
                        .HasMaxLength(30);

                    b.HasKey("UserId")
                        .HasName("PK__users__CB9A1CDF890C61CD");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Stock.Models.OrderDetail", b =>
                {
                    b.HasOne("Stock.Models.Orders", "Order")
                        .WithMany("OrderDetail")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK_OrderDetail_orders")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Stock.Models.Productbrand", "ProductBrand")
                        .WithMany("OrderDetail")
                        .HasForeignKey("ProductBrandId")
                        .HasConstraintName("FK_OrderDetail_productbrand");
                });

            modelBuilder.Entity("Stock.Models.Orders", b =>
                {
                    b.HasOne("Stock.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK__orders__customer__4CA06362");
                });

            modelBuilder.Entity("Stock.Models.Payment", b =>
                {
                    b.HasOne("Stock.Models.Customer", "Customer")
                        .WithMany("Payment")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK__payment__custome__5BE2A6F2");

                    b.HasOne("Stock.Models.Suplier", "Suplier")
                        .WithMany("Payment")
                        .HasForeignKey("SuplierId")
                        .HasConstraintName("FK__payment__suplier__5165187F");
                });

            modelBuilder.Entity("Stock.Models.Productbrand", b =>
                {
                    b.HasOne("Stock.Models.Attributes", "Attribute")
                        .WithMany("Productbrand")
                        .HasForeignKey("AttributeId")
                        .HasConstraintName("FK__productbr__attri__4BAC3F29");

                    b.HasOne("Stock.Models.Brand", "Brand")
                        .WithMany("Productbrand")
                        .HasForeignKey("BrandId")
                        .HasConstraintName("FK__productbr__brand__5535A963");

                    b.HasOne("Stock.Models.Catagory", "Catagory")
                        .WithMany("Productbrand")
                        .HasForeignKey("CatagoryId")
                        .HasConstraintName("FK__productbr__catag__5629CD9C");

                    b.HasOne("Stock.Models.Product", "Product")
                        .WithMany("Productbrand")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK__productbr__produ__48CFD27E")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Stock.Models.PurchaseOrder", b =>
                {
                    b.HasOne("Stock.Models.Suplier", "Suplier")
                        .WithMany("PurchaseOrder")
                        .HasForeignKey("SuplierId")
                        .HasConstraintName("FK_PurchaseOrder_suplier");
                });

            modelBuilder.Entity("Stock.Models.PurchaseOrderDetail", b =>
                {
                    b.HasOne("Stock.Models.Productbrand", "PoductBrand")
                        .WithMany("PurchaseOrderDetail")
                        .HasForeignKey("PoductBrandId")
                        .HasConstraintName("FK_PurchaseOrderDetail_productbrand");

                    b.HasOne("Stock.Models.PurchaseOrder", "Purchase")
                        .WithMany("PurchaseOrderDetail")
                        .HasForeignKey("PurchaseId")
                        .HasConstraintName("FK_PurchaseOrderDetail_PurchaseOrder")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Stock.Models.Stocks", b =>
                {
                    b.HasOne("Stock.Models.Productbrand", "ProductBrand")
                        .WithMany("Stocks")
                        .HasForeignKey("ProductBrandId")
                        .HasConstraintName("FK__stock__productBr__4F7CD00D");
                });
#pragma warning restore 612, 618
        }
    }
}
