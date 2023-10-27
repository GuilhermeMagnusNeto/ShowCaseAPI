﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ShowCaseAPI.Infra.Context.CrossCutting.Identity.Data;

#nullable disable

namespace ShowCaseAPI.Identity.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231027213250_TestNullField")]
    partial class TestNullField
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ShowCaseAPI.Domain.Entities.Showcase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("ID");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CREATED_AT");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("DELETED");

                    b.Property<string>("ExclusiveCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("EXCLUSIVE_CODE");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("NAME");

                    b.Property<Guid>("StoreId")
                        .HasColumnType("uuid")
                        .HasColumnName("STORE_ID");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UPDATED_AT");

                    b.HasKey("Id");

                    b.HasIndex("ExclusiveCode")
                        .IsUnique();

                    b.HasIndex("StoreId");

                    b.ToTable("showcases");
                });

            modelBuilder.Entity("ShowCaseAPI.Domain.Entities.ShowcaseStyle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("ID");

                    b.Property<string>("BackgroundColorCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("BACKGROUND_COLOR_CODE");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CREATED_AT");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("DELETED");

                    b.Property<bool>("ShowProductValue")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true)
                        .HasColumnName("SHOW_PRODUCT_VALUE");

                    b.Property<bool>("ShowStoreLogo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true)
                        .HasColumnName("SHOW_STORE_LOGO");

                    b.Property<Guid>("ShowcaseId")
                        .HasColumnType("uuid")
                        .HasColumnName("SHOWCASE_ID");

                    b.Property<Guid>("TemplateId")
                        .HasColumnType("uuid")
                        .HasColumnName("TEMPLATE_ID");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UPDATED_AT");

                    b.HasKey("Id");

                    b.HasIndex("ShowcaseId");

                    b.HasIndex("TemplateId");

                    b.ToTable("showcase_styles");
                });

            modelBuilder.Entity("ShowCaseAPI.Domain.Entities.Store", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("ID");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CREATED_AT");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("DELETED");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("NAME");

                    b.Property<string>("StoreLogo")
                        .HasColumnType("text")
                        .HasColumnName("STORE_LOGO");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UPDATED_AT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("USER_ID");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("stores");
                });

            modelBuilder.Entity("ShowCaseAPI.Domain.Entities.StoreProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("ID");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CREATED_AT");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("DELETED");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("NAME");

                    b.Property<string>("ProductPicture")
                        .HasColumnType("text")
                        .HasColumnName("PRODUCT_PICTURE");

                    b.Property<string>("SKU")
                        .HasColumnType("text")
                        .HasColumnName("SKU");

                    b.Property<Guid>("StoreId")
                        .HasColumnType("uuid")
                        .HasColumnName("STORE_ID");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UPDATED_AT");

                    b.Property<double?>("Value")
                        .HasColumnType("double precision")
                        .HasColumnName("VALUE");

                    b.HasKey("Id");

                    b.HasIndex("StoreId");

                    b.ToTable("store_products");
                });

            modelBuilder.Entity("ShowCaseAPI.Domain.Entities.Template", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("ID");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CREATED_AT");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("DELETED");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("NAME");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UPDATED_AT");

                    b.HasKey("Id");

                    b.ToTable("templates");
                });

            modelBuilder.Entity("ShowCaseAPI.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("ID");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CREATED_AT");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("DELETED");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("EMAIL");

                    b.Property<bool>("EmailConfirmed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("EMAIL_CONFIRMED");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("NAME");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("PASSWORD_HASH");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("PASSWORD_SALT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UPDATED_AT");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("users");
                });

            modelBuilder.Entity("ShowCaseAPI.Domain.Entities.Showcase", b =>
                {
                    b.HasOne("ShowCaseAPI.Domain.Entities.Store", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Store");
                });

            modelBuilder.Entity("ShowCaseAPI.Domain.Entities.ShowcaseStyle", b =>
                {
                    b.HasOne("ShowCaseAPI.Domain.Entities.Showcase", "Showcase")
                        .WithMany()
                        .HasForeignKey("ShowcaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShowCaseAPI.Domain.Entities.Template", "Template")
                        .WithMany()
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Showcase");

                    b.Navigation("Template");
                });

            modelBuilder.Entity("ShowCaseAPI.Domain.Entities.Store", b =>
                {
                    b.HasOne("ShowCaseAPI.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShowCaseAPI.Domain.Entities.StoreProduct", b =>
                {
                    b.HasOne("ShowCaseAPI.Domain.Entities.Store", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Store");
                });
#pragma warning restore 612, 618
        }
    }
}
