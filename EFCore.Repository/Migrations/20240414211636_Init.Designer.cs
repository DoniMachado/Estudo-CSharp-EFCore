﻿// <auto-generated />
using System;
using EFCore.Repository.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFCore.Repository.Migrations
{
    [DbContext(typeof(HeroiContext))]
    [Migration("20240414211636_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EFCore.Domain.Entities.Arma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HeroiId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HeroiId");

                    b.ToTable("Arma", (string)null);
                });

            modelBuilder.Entity("EFCore.Domain.Entities.Batalha", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DtFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DtInicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Batalha", (string)null);
                });

            modelBuilder.Entity("EFCore.Domain.Entities.Heroi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Heroi", (string)null);
                });

            modelBuilder.Entity("EFCore.Domain.Entities.HeroiBatalha", b =>
                {
                    b.Property<int>("BatalhaId")
                        .HasColumnType("int");

                    b.Property<int>("HeroiId")
                        .HasColumnType("int");

                    b.HasKey("BatalhaId", "HeroiId");

                    b.HasIndex("HeroiId");

                    b.ToTable("HeroiBatalha", (string)null);
                });

            modelBuilder.Entity("EFCore.Domain.Entities.IdentidadeSecreta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("HeroiId")
                        .HasColumnType("int");

                    b.Property<string>("NomeReal")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HeroiId")
                        .IsUnique();

                    b.ToTable("IdentidadeSecreta", (string)null);
                });

            modelBuilder.Entity("EFCore.Domain.Entities.Arma", b =>
                {
                    b.HasOne("EFCore.Domain.Entities.Heroi", "Heroi")
                        .WithMany("Armas")
                        .HasForeignKey("HeroiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Heroi");
                });

            modelBuilder.Entity("EFCore.Domain.Entities.HeroiBatalha", b =>
                {
                    b.HasOne("EFCore.Domain.Entities.Batalha", "Batalha")
                        .WithMany("Herois")
                        .HasForeignKey("BatalhaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EFCore.Domain.Entities.Heroi", "Heroi")
                        .WithMany("Batalhas")
                        .HasForeignKey("HeroiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Batalha");

                    b.Navigation("Heroi");
                });

            modelBuilder.Entity("EFCore.Domain.Entities.IdentidadeSecreta", b =>
                {
                    b.HasOne("EFCore.Domain.Entities.Heroi", "Heroi")
                        .WithOne("IdentidadeSecreta")
                        .HasForeignKey("EFCore.Domain.Entities.IdentidadeSecreta", "HeroiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Heroi");
                });

            modelBuilder.Entity("EFCore.Domain.Entities.Batalha", b =>
                {
                    b.Navigation("Herois");
                });

            modelBuilder.Entity("EFCore.Domain.Entities.Heroi", b =>
                {
                    b.Navigation("Armas");

                    b.Navigation("Batalhas");

                    b.Navigation("IdentidadeSecreta");
                });
#pragma warning restore 612, 618
        }
    }
}