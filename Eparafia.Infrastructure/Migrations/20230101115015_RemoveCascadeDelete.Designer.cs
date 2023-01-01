﻿// <auto-generated />
using System;
using Eparafia.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Eparafia.Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230101115015_RemoveCascadeDelete")]
    partial class RemoveCascadeDelete
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Eparafia.Application.Entities.Parish", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("_Parishes");
                });

            modelBuilder.Entity("Eparafia.Application.Entities.Priest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("HasAvatar")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("ParishId")
                        .HasColumnType("uuid");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ParishId");

                    b.ToTable("_Priests");
                });

            modelBuilder.Entity("Eparafia.Application.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("HasAvatar")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid?>("ParishId")
                        .HasColumnType("uuid");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ParishId");

                    b.ToTable("_Users");
                });

            modelBuilder.Entity("Eparafia.Application.Entities.Parish", b =>
                {
                    b.OwnsOne("Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("ParishId")
                                .HasColumnType("uuid");

                            b1.Property<string>("BuildingNumber")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("FlatNumber")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("PostCode")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Region")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("ParishId");

                            b1.ToTable("_Parishes");

                            b1.WithOwner()
                                .HasForeignKey("ParishId");
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });

            modelBuilder.Entity("Eparafia.Application.Entities.Priest", b =>
                {
                    b.HasOne("Eparafia.Application.Entities.Parish", "Parish")
                        .WithMany("Priests")
                        .HasForeignKey("ParishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parish");
                });

            modelBuilder.Entity("Eparafia.Application.Entities.User", b =>
                {
                    b.HasOne("Eparafia.Application.Entities.Parish", "Parish")
                        .WithMany("Users")
                        .HasForeignKey("ParishId");

                    b.Navigation("Parish");
                });

            modelBuilder.Entity("Eparafia.Application.Entities.Parish", b =>
                {
                    b.Navigation("Priests");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}