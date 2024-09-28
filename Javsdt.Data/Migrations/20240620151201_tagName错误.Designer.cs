﻿// <auto-generated />
using System;
using Javsdt.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Javsdt.Data.Migrations
{
    [DbContext(typeof(CarDbContext))]
    [Migration("20240620151201_tagName错误")]
    partial class tagName错误
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("JavSubtitles", b =>
                {
                    b.Property<int>("JavsID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SubtitlesID")
                        .HasColumnType("INTEGER");

                    b.HasKey("JavsID", "SubtitlesID");

                    b.HasIndex("SubtitlesID");

                    b.ToTable("JavSubtitles");
                });

            modelBuilder.Entity("Javsdt.Shared.Entitys.File.Jav", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CD")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CDCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CarName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Dir")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Edition")
                        .HasColumnType("TEXT");

                    b.Property<int>("Error")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Ext")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FanartPath")
                        .HasColumnType("TEXT");

                    b.Property<bool>("HasSubtitle")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsCracked")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDivulged")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsSeparate")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("NameWithoutExt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("OnlyEdition")
                        .HasColumnType("INTEGER");

                    b.Property<string>("OriginAbsolutePath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Version")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("Javs");
                });

            modelBuilder.Entity("Javsdt.Shared.Entitys.Media.Car", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Arzon")
                        .HasColumnType("TEXT");

                    b.Property<string>("BusCover")
                        .HasColumnType("TEXT");

                    b.Property<string>("CarPrefName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("Crop")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DbCover")
                        .HasColumnType("TEXT");

                    b.Property<string>("DmmCover")
                        .HasColumnType("TEXT");

                    b.Property<string>("Jav321")
                        .HasColumnType("TEXT");

                    b.Property<string>("JavBus")
                        .HasColumnType("TEXT");

                    b.Property<string>("JavDb")
                        .HasColumnType("TEXT");

                    b.Property<string>("JavLibrary")
                        .HasColumnType("TEXT");

                    b.Property<string>("LibraryCover")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("OriginName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Plot")
                        .HasColumnType("TEXT");

                    b.Property<string>("Poster")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Release")
                        .HasColumnType("TEXT");

                    b.Property<string>("Review")
                        .HasColumnType("TEXT");

                    b.Property<int>("Runtime")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Score")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SeriesName")
                        .HasColumnType("TEXT");

                    b.Property<string>("T21Cover")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Version")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ZhPlot")
                        .HasColumnType("TEXT");

                    b.Property<string>("ZhTitle")
                        .HasColumnType("TEXT");

                    b.HasKey("Name");

                    b.HasIndex("CarPrefName");

                    b.HasIndex("SeriesName");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("Javsdt.Shared.Models.Entitys.File.NasJavFolder", b =>
                {
                    b.Property<string>("Path")
                        .HasColumnType("TEXT");

                    b.Property<string>("CarName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("HasVideo")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("Version")
                        .HasColumnType("INTEGER");

                    b.HasKey("Path");

                    b.ToTable("NasJavFolders");
                });

            modelBuilder.Entity("Javsdt.Shared.Models.Entitys.File.Subtitle", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CarName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Dir")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Error")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Ext")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("NameWithoutExt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OriginAbsolutePath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Version")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("Subtitles");
                });

            modelBuilder.Entity("Javsdt.Shared.Models.Entitys.Media.CarPref", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("MaxSuf")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UpdateStatus")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Version")
                        .HasColumnType("INTEGER");

                    b.HasKey("Name");

                    b.ToTable("CarsPrefs");
                });

            modelBuilder.Entity("Javsdt.Shared.Models.Entitys.Media.Crew", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("Version")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ZhName")
                        .HasColumnType("TEXT");

                    b.HasKey("Name");

                    b.ToTable("Crews");
                });

            modelBuilder.Entity("Javsdt.Shared.Models.Entitys.Media.Middle.CarCrew", b =>
                {
                    b.Property<string>("CarName")
                        .HasColumnType("TEXT");

                    b.Property<string>("CrewName")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("Version")
                        .HasColumnType("INTEGER");

                    b.HasKey("CarName", "CrewName", "Type");

                    b.HasIndex("CrewName");

                    b.ToTable("CarCrews");
                });

            modelBuilder.Entity("Javsdt.Shared.Models.Entitys.Media.Middle.CarStudio", b =>
                {
                    b.Property<string>("CarName")
                        .HasColumnType("TEXT");

                    b.Property<string>("StudioName")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("CarName", "StudioName", "Type");

                    b.HasIndex("StudioName");

                    b.ToTable("CarStudios");
                });

            modelBuilder.Entity("Javsdt.Shared.Models.Entitys.Media.Middle.CarTag", b =>
                {
                    b.Property<string>("CarName")
                        .HasColumnType("TEXT");

                    b.Property<string>("TagName")
                        .HasColumnType("TEXT");

                    b.HasKey("CarName", "TagName");

                    b.HasIndex("TagName");

                    b.ToTable("CarTags");
                });

            modelBuilder.Entity("Javsdt.Shared.Models.Entitys.Media.Series", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("Version")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ZhName")
                        .HasColumnType("TEXT");

                    b.HasKey("Name");

                    b.ToTable("Seriess");
                });

            modelBuilder.Entity("Javsdt.Shared.Models.Entitys.Media.Studio", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("Version")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ZhName")
                        .HasColumnType("TEXT");

                    b.HasKey("Name");

                    b.ToTable("Studios");
                });

            modelBuilder.Entity("Javsdt.Shared.Models.Entitys.Media.Tag", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Remark")
                        .HasColumnType("TEXT");

                    b.Property<string>("TName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Version")
                        .HasColumnType("INTEGER");

                    b.HasKey("Name");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Javsdt.Shared.Models.Entitys.Media.WebTag", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("Website")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Remark")
                        .HasColumnType("TEXT");

                    b.Property<string>("TagName")
                        .HasColumnType("TEXT");

                    b.Property<int>("Version")
                        .HasColumnType("INTEGER");

                    b.HasKey("Name", "Website");

                    b.HasIndex("TagName");

                    b.ToTable("WebTags");
                });

            modelBuilder.Entity("JavSubtitles", b =>
                {
                    b.HasOne("Javsdt.Shared.Entitys.File.Jav", null)
                        .WithMany()
                        .HasForeignKey("JavsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Javsdt.Shared.Models.Entitys.File.Subtitle", null)
                        .WithMany()
                        .HasForeignKey("SubtitlesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Javsdt.Shared.Entitys.Media.Car", b =>
                {
                    b.HasOne("Javsdt.Shared.Models.Entitys.Media.CarPref", "CarPref")
                        .WithMany("Cars")
                        .HasForeignKey("CarPrefName");

                    b.HasOne("Javsdt.Shared.Models.Entitys.Media.Series", "Series")
                        .WithMany("Cars")
                        .HasForeignKey("SeriesName");

                    b.Navigation("CarPref");

                    b.Navigation("Series");
                });

            modelBuilder.Entity("Javsdt.Shared.Models.Entitys.Media.Middle.CarCrew", b =>
                {
                    b.HasOne("Javsdt.Shared.Entitys.Media.Car", "Car")
                        .WithMany("CarCrews")
                        .HasForeignKey("CarName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Javsdt.Shared.Models.Entitys.Media.Crew", "Crew")
                        .WithMany("CarCrews")
                        .HasForeignKey("CrewName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Crew");
                });

            modelBuilder.Entity("Javsdt.Shared.Models.Entitys.Media.Middle.CarStudio", b =>
                {
                    b.HasOne("Javsdt.Shared.Entitys.Media.Car", "Car")
                        .WithMany("CarStudios")
                        .HasForeignKey("CarName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Javsdt.Shared.Models.Entitys.Media.Studio", "Studio")
                        .WithMany("CarStudios")
                        .HasForeignKey("StudioName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Studio");
                });

            modelBuilder.Entity("Javsdt.Shared.Models.Entitys.Media.Middle.CarTag", b =>
                {
                    b.HasOne("Javsdt.Shared.Entitys.Media.Car", "Car")
                        .WithMany("CarTags")
                        .HasForeignKey("CarName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Javsdt.Shared.Models.Entitys.Media.Tag", "Tag")
                        .WithMany("CarTags")
                        .HasForeignKey("TagName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Javsdt.Shared.Models.Entitys.Media.WebTag", b =>
                {
                    b.HasOne("Javsdt.Shared.Models.Entitys.Media.Tag", "Tag")
                        .WithMany("WebTags")
                        .HasForeignKey("TagName");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Javsdt.Shared.Entitys.Media.Car", b =>
                {
                    b.Navigation("CarCrews");

                    b.Navigation("CarStudios");

                    b.Navigation("CarTags");
                });

            modelBuilder.Entity("Javsdt.Shared.Models.Entitys.Media.CarPref", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("Javsdt.Shared.Models.Entitys.Media.Crew", b =>
                {
                    b.Navigation("CarCrews");
                });

            modelBuilder.Entity("Javsdt.Shared.Models.Entitys.Media.Series", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("Javsdt.Shared.Models.Entitys.Media.Studio", b =>
                {
                    b.Navigation("CarStudios");
                });

            modelBuilder.Entity("Javsdt.Shared.Models.Entitys.Media.Tag", b =>
                {
                    b.Navigation("CarTags");

                    b.Navigation("WebTags");
                });
#pragma warning restore 612, 618
        }
    }
}
