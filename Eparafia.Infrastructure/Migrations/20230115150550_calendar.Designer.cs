﻿// <auto-generated />
using System;
using Eparafia.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Eparafia.Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230115150550_calendar")]
    partial class calendar
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Eparafia.Application.Entities.Announcement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ParishId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ParishId");

                    b.ToTable("_Announcement");
                });

            modelBuilder.Entity("Eparafia.Application.Entities.AnnouncementRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AnnouncementId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AnnouncementId");

                    b.ToTable("_AnnouncementsRecords");
                });

            modelBuilder.Entity("Eparafia.Application.Entities.CommonEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<int>("EventInWeekId")
                        .HasColumnType("int");

                    b.Property<Guid>("ParishId")
                        .HasColumnType("char(36)");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time(6)");

                    b.HasKey("Id");

                    b.HasIndex("ParishId");

                    b.ToTable("_CommonWeek");
                });

            modelBuilder.Entity("Eparafia.Application.Entities.Parish", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CallName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("_Parishes");
                });

            modelBuilder.Entity("Eparafia.Application.Entities.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("ParishId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ParishId");

                    b.ToTable("_Post");
                });

            modelBuilder.Entity("Eparafia.Application.Entities.PostFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FilePathMin")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("FileType")
                        .HasColumnType("int");

                    b.Property<Guid>("PostId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("_PostFile");
                });

            modelBuilder.Entity("Eparafia.Application.Entities.Priest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("FunctionParish")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("ParishId")
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

                    b.Property<string>("Role")
                        .HasColumnType("longtext");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ParishId");

                    b.ToTable("_Priests");
                });

            modelBuilder.Entity("Eparafia.Application.Entities.SpecialEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("ParishId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ParishId");

                    b.ToTable("_SpecialEvent");
                });

            modelBuilder.Entity("Eparafia.Application.Entities.User", b =>
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

                    b.Property<Guid?>("ParishId")
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

                    b.Property<string>("Role")
                        .HasColumnType("longtext");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("longtext");

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

            modelBuilder.Entity("Eparafia.Application.Entities.AnnouncementRecord", b =>
                {
                    b.HasOne("Eparafia.Application.Entities.Announcement", "Announcement")
                        .WithMany("AnnouncementsRecords")
                        .HasForeignKey("AnnouncementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Announcement");
                });

            modelBuilder.Entity("Eparafia.Application.Entities.CommonEvent", b =>
                {
                    b.HasOne("Eparafia.Application.Entities.Parish", "Parish")
                        .WithMany("CommonWeek")
                        .HasForeignKey("ParishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Eparafia.Application.ValueObjects.Event", "Event", b1 =>
                        {
                            b1.Property<Guid>("CommonEventId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("EventDescription")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<int>("Type")
                                .HasColumnType("int");

                            b1.HasKey("CommonEventId");

                            b1.ToTable("_CommonWeek");

                            b1.WithOwner()
                                .HasForeignKey("CommonEventId");
                        });

                    b.Navigation("Event")
                        .IsRequired();

                    b.Navigation("Parish");
                });

            modelBuilder.Entity("Eparafia.Application.Entities.Parish", b =>
                {
                    b.OwnsOne("Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("ParishId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("BuildingNumber")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("PostCode")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("Region")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.HasKey("ParishId");

                            b1.ToTable("_Parishes");

                            b1.WithOwner()
                                .HasForeignKey("ParishId");
                        });

                    b.OwnsOne("Contact", "Contact", b1 =>
                        {
                            b1.Property<Guid>("ParishId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("PhoneNumber")
                                .IsRequired()
                                .HasColumnType("longtext");

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

            modelBuilder.Entity("Eparafia.Application.Entities.Post", b =>
                {
                    b.HasOne("Eparafia.Application.Entities.Priest", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Eparafia.Application.Entities.Parish", "Parish")
                        .WithMany("Posts")
                        .HasForeignKey("ParishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Parish");
                });

            modelBuilder.Entity("Eparafia.Application.Entities.PostFile", b =>
                {
                    b.HasOne("Eparafia.Application.Entities.Post", "Post")
                        .WithMany("Files")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Eparafia.Application.Entities.Priest", b =>
                {
                    b.HasOne("Eparafia.Application.Entities.Parish", "Parish")
                        .WithMany("Priests")
                        .HasForeignKey("ParishId");

                    b.OwnsOne("Contact", "Contact", b1 =>
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

                    b.Navigation("Parish");
                });

            modelBuilder.Entity("Eparafia.Application.Entities.SpecialEvent", b =>
                {
                    b.HasOne("Eparafia.Application.Entities.Parish", "Parish")
                        .WithMany("SpecialEvents")
                        .HasForeignKey("ParishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Eparafia.Application.ValueObjects.Event", "Event", b1 =>
                        {
                            b1.Property<Guid>("SpecialEventId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("EventDescription")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<int>("Type")
                                .HasColumnType("int");

                            b1.HasKey("SpecialEventId");

                            b1.ToTable("_SpecialEvent");

                            b1.WithOwner()
                                .HasForeignKey("SpecialEventId");
                        });

                    b.Navigation("Event")
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

                    b.Navigation("CommonWeek");

                    b.Navigation("Posts");

                    b.Navigation("Priests");

                    b.Navigation("SpecialEvents");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Eparafia.Application.Entities.Post", b =>
                {
                    b.Navigation("Files");
                });

            modelBuilder.Entity("Eparafia.Application.Entities.Priest", b =>
                {
                    b.Navigation("CreatedAnnouncements");
                });
#pragma warning restore 612, 618
        }
    }
}