﻿// <auto-generated />
using System;
using EBISX_POS.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EBISX_POS.API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("EBISX_POS.API.Models.AddOnType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AddOnTypeName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("AddOnType");
                });

            modelBuilder.Entity("EBISX_POS.API.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CtgryName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("EBISX_POS.API.Models.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CouponCode")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal?>("DiscountAmount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("DiscountPromoPercent")
                        .HasColumnType("int");

                    b.Property<string>("DiscountType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("EligiblePwdScCount")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("ExpirationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PromoCode")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Discount");
                });

            modelBuilder.Entity("EBISX_POS.API.Models.DrinkType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DrinkTypeName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("DrinkType");
                });

            modelBuilder.Entity("EBISX_POS.API.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AddOnId")
                        .HasColumnType("int");

                    b.Property<int?>("DrinkId")
                        .HasColumnType("int");

                    b.Property<long?>("EntryId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsPwdDiscounted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsSeniorDiscounted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsVoid")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal?>("ItemPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("ItemQTY")
                        .HasColumnType("int");

                    b.Property<int?>("MealId")
                        .HasColumnType("int");

                    b.Property<int?>("MenuId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("VoidedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("createdAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("AddOnId");

                    b.HasIndex("DrinkId");

                    b.HasIndex("MealId");

                    b.HasIndex("MenuId");

                    b.HasIndex("OrderId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("EBISX_POS.API.Models.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AddOnTypeId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("DrinkTypeId")
                        .HasColumnType("int");

                    b.Property<bool>("HasAddOn")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("HasDrink")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsAddOn")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("MenuImagePath")
                        .HasColumnType("longtext");

                    b.Property<bool>("MenuIsAvailable")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("MenuName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("MenuPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Size")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AddOnTypeId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("DrinkTypeId");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("EBISX_POS.API.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("CashTendered")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("CashierUserEmail")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("DiscountId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCancelled")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsPending")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ManagerUserEmail")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("OrderType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("CashierUserEmail");

                    b.HasIndex("DiscountId");

                    b.HasIndex("ManagerUserEmail");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("EBISX_POS.API.Models.Timestamp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CashierUserEmail")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ManagerBreakInUserEmail")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ManagerBreakOutUserEmail")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ManagerInUserEmail")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ManagerOutUserEmail")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTimeOffset?>("TsBreakIn")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset?>("TsBreakOut")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset?>("TsIn")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset?>("TsOut")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CashierUserEmail");

                    b.HasIndex("ManagerBreakInUserEmail");

                    b.HasIndex("ManagerBreakOutUserEmail");

                    b.HasIndex("ManagerInUserEmail");

                    b.HasIndex("ManagerOutUserEmail");

                    b.ToTable("Timestamp");
                });

            modelBuilder.Entity("EBISX_POS.API.Models.User", b =>
                {
                    b.Property<string>("UserEmail")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserFName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserLName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("UserEmail");

                    b.ToTable("User");
                });

            modelBuilder.Entity("EBISX_POS.API.Models.Item", b =>
                {
                    b.HasOne("EBISX_POS.API.Models.Menu", "AddOn")
                        .WithMany()
                        .HasForeignKey("AddOnId");

                    b.HasOne("EBISX_POS.API.Models.Menu", "Drink")
                        .WithMany()
                        .HasForeignKey("DrinkId");

                    b.HasOne("EBISX_POS.API.Models.Item", "Meal")
                        .WithMany()
                        .HasForeignKey("MealId");

                    b.HasOne("EBISX_POS.API.Models.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuId");

                    b.HasOne("EBISX_POS.API.Models.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddOn");

                    b.Navigation("Drink");

                    b.Navigation("Meal");

                    b.Navigation("Menu");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("EBISX_POS.API.Models.Menu", b =>
                {
                    b.HasOne("EBISX_POS.API.Models.AddOnType", "AddOnType")
                        .WithMany()
                        .HasForeignKey("AddOnTypeId");

                    b.HasOne("EBISX_POS.API.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EBISX_POS.API.Models.DrinkType", "DrinkType")
                        .WithMany()
                        .HasForeignKey("DrinkTypeId");

                    b.Navigation("AddOnType");

                    b.Navigation("Category");

                    b.Navigation("DrinkType");
                });

            modelBuilder.Entity("EBISX_POS.API.Models.Order", b =>
                {
                    b.HasOne("EBISX_POS.API.Models.User", "Cashier")
                        .WithMany()
                        .HasForeignKey("CashierUserEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EBISX_POS.API.Models.Discount", "Discount")
                        .WithMany()
                        .HasForeignKey("DiscountId");

                    b.HasOne("EBISX_POS.API.Models.User", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerUserEmail");

                    b.Navigation("Cashier");

                    b.Navigation("Discount");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("EBISX_POS.API.Models.Timestamp", b =>
                {
                    b.HasOne("EBISX_POS.API.Models.User", "Cashier")
                        .WithMany()
                        .HasForeignKey("CashierUserEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EBISX_POS.API.Models.User", "ManagerBreakIn")
                        .WithMany()
                        .HasForeignKey("ManagerBreakInUserEmail");

                    b.HasOne("EBISX_POS.API.Models.User", "ManagerBreakOut")
                        .WithMany()
                        .HasForeignKey("ManagerBreakOutUserEmail");

                    b.HasOne("EBISX_POS.API.Models.User", "ManagerIn")
                        .WithMany()
                        .HasForeignKey("ManagerInUserEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EBISX_POS.API.Models.User", "ManagerOut")
                        .WithMany()
                        .HasForeignKey("ManagerOutUserEmail");

                    b.Navigation("Cashier");

                    b.Navigation("ManagerBreakIn");

                    b.Navigation("ManagerBreakOut");

                    b.Navigation("ManagerIn");

                    b.Navigation("ManagerOut");
                });

            modelBuilder.Entity("EBISX_POS.API.Models.Order", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
