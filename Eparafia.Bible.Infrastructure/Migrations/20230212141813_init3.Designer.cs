﻿// <auto-generated />
using System;
using Eparafia.Bible.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Eparafia.Bible.Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230212141813_init3")]
    partial class init3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Eparafia.Bible.Domain.Entities.Day", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("_Days");
                });

            modelBuilder.Entity("Eparafia.Bible.Domain.Entities.Reading", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("DayId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.ToTable("_Readings");
                });

            modelBuilder.Entity("Eparafia.Bible.Domain.Entities.Reading", b =>
                {
                    b.HasOne("Eparafia.Bible.Domain.Entities.Day", "Day")
                        .WithMany("Readings")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Day");
                });

            modelBuilder.Entity("Eparafia.Bible.Domain.Entities.Day", b =>
                {
                    b.Navigation("Readings");
                });
#pragma warning restore 612, 618
        }
    }
}
