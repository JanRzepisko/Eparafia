﻿// <auto-generated />
using System;
using Eparafia.Identity.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Eparafia.Identity.Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230609112339_init2")]
    partial class init2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Eparafia.Identity.Domain.Entities.Priest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("ParishId")
                        .HasColumnType("char(36)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhotoPath")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhotoPathMin")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("_Priests");
                });

            modelBuilder.Entity("Eparafia.Identity.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("ParishId")
                        .HasColumnType("char(36)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhotoPath")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhotoPathMin")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("_Users");
                });

            modelBuilder.Entity("Eparafia.Identity.Domain.Entities.Priest", b =>
                {
                    b.OwnsOne("Eparafia.Identity.Domain.ValueObjects.Contact", "Contact", b1 =>
                        {
                            b1.Property<Guid>("PriestId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("PhoneNumber")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.HasKey("PriestId");

                            b1.ToTable("_Priests");

                            b1.WithOwner()
                                .HasForeignKey("PriestId");
                        });

                    b.Navigation("Contact")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
