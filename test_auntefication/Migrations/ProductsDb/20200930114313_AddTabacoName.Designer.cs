﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using test_auntefication.Data;

namespace test_auntefication.Migrations.ProductsDb
{
    [DbContext(typeof(ProductsDbContext))]
    [Migration("20200930114313_AddTabacoName")]
    partial class AddTabacoName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("test_auntefication.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("test_auntefication.Models.CompanyStock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyId");

                    b.Property<int>("TabacoBundleWeigh");

                    b.Property<int>("TabacoCount");

                    b.Property<int>("TabacoId");

                    b.Property<string>("TabacoName");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("TabacoId");

                    b.ToTable("CompanyStock");
                });

            modelBuilder.Entity("test_auntefication.Models.Tabaco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Tabaco");
                });

            modelBuilder.Entity("test_auntefication.Models.UserCompany", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("UserCompany");
                });

            modelBuilder.Entity("test_auntefication.Models.WorkStock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyId");

                    b.Property<string>("NameTabaco");

                    b.Property<int>("TabacoId");

                    b.Property<int>("TabacoWeigh");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("TabacoId");

                    b.ToTable("WorkStock");
                });

            modelBuilder.Entity("test_auntefication.Models.CompanyStock", b =>
                {
                    b.HasOne("test_auntefication.Models.Company", "Company")
                        .WithMany("CompanyStocks")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("test_auntefication.Models.Tabaco", "Tabaco")
                        .WithMany("CompanyStocks")
                        .HasForeignKey("TabacoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("test_auntefication.Models.UserCompany", b =>
                {
                    b.HasOne("test_auntefication.Models.Company", "Company")
                        .WithMany("UserCompany")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("test_auntefication.Models.WorkStock", b =>
                {
                    b.HasOne("test_auntefication.Models.Company", "Company")
                        .WithMany("WorkStocks")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("test_auntefication.Models.Tabaco", "Tabaco")
                        .WithMany("WorkStocks")
                        .HasForeignKey("TabacoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
