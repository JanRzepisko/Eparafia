﻿// <auto-generated />
using System;
using Eparafia.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Eparafia.Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Eparafia.Application.Entities.Announcement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ParishId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ParishId");

                    b.ToTable("_Announcement");
                });

            modelBuilder.Entity("Eparafia.Application.Entities.AnnouncementsRecords", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AnnouncementId")
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AnnouncementId");

                    b.ToTable("_AnnouncementsRecords");
                });

            modelBuilder.Entity("Eparafia.Application.Entities.Parish", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CallName")
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
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("FunctionParish")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("ParishId")
                        .HasColumnType("uuid");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhotoPath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhotoPathMin")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
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
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("ParishId")
                        .HasColumnType("uuid");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhotoPath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhotoPathMin")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ParishId");

                    b.ToTable("_Users");
                });

            modelBuilder.Entity("Eparafia.Application.Entities.Announcement", b =>
                {
                    b.HasOne("Eparafia.Application.Entities.Priest", "Author")
                        .WithMany("CreatedAnnouncements")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Eparafia.Application.Entities.Parish", "Parish")
                        .WithMany("Announcements")
                        .HasForeignKey("ParishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Parish");
                });

            modelBuilder.Entity("Eparafia.Application.Entities.AnnouncementsRecords", b =>
                {
                    b.HasOne("Eparafia.Application.Entities.Announcement", "Announcement")
                        .WithMany("AnnouncementsRecords")
                        .HasForeignKey("AnnouncementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Announcement");
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

                    b.OwnsOne("Contact", "Contact", b1 =>
                        {
                            b1.Property<Guid>("ParishId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("PhoneNumber")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("ParishId");

                            b1.ToTable("_Parishes");

                            b1.WithOwner()
                                .HasForeignKey("ParishId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Contact")
                        .IsRequired();
                });

            modelBuilder.Entity("Eparafia.Application.Entities.Priest", b =>
                {
                    b.HasOne("Eparafia.Application.Entities.Parish", "Parish")
                        .WithMany("Priests")
                        .HasForeignKey("ParishId");

                    b.OwnsOne("Contact", "Contact", b1 =>
                        {
                            b1.Property<Guid>("PriestId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("PhoneNumber")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("PriestId");

                            b1.ToTable("_Priests");

                            b1.WithOwner()
                                .HasForeignKey("PriestId");
                        });

                    b.Navigation("Contact")
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

            modelBuilder.Entity("Eparafia.Application.Entities.Announcement", b =>
                {
                    b.Navigation("AnnouncementsRecords");
                });

            modelBuilder.Entity("Eparafia.Application.Entities.Parish", b =>
                {
                    b.Navigation("Announcements");

                    b.Navigation("Priests");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Eparafia.Application.Entities.Priest", b =>
                {
                    b.Navigation("CreatedAnnouncements");
                });
#pragma warning restore 612, 618
        }
    }
}
