﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SearchEngine.DataBase;

namespace SearchEngine.Migrations
{
    [DbContext(typeof(SearchEngineContext))]
    [Migration("20201125132702_addMeaningfulText")]
    partial class addMeaningfulText
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("SearchEngine.DataBase.Model.ExternalLinks", b =>
                {
                    b.Property<int>("IDOfExternal")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<string>("externalLink")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDOfExternal");

                    b.ToTable("ExternalLinks");
                });

            modelBuilder.Entity("SearchEngine.DataBase.Model.Keywords", b =>
                {
                    b.Property<string>("Keyword")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Keyword");

                    b.ToTable("Keywords");
                });

            modelBuilder.Entity("SearchEngine.DataBase.Model.LinkDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Snippet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LinkDetails");
                });

            modelBuilder.Entity("SearchEngine.DataBase.Model.LinkPositionTracker", b =>
                {
                    b.Property<string>("Keywords")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Keywords", "Link");

                    b.ToTable("LinkTracker");
                });

            modelBuilder.Entity("SearchEngine.DataBase.Model.PositonAndDate", b =>
                {
                    b.Property<int>("PositionAndDateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<double>("Css")
                        .HasColumnType("float");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("Js")
                        .HasColumnType("float");

                    b.Property<string>("Keywords")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MeaningfulText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<int>("WordCount")
                        .HasColumnType("int");

                    b.HasKey("PositionAndDateId");

                    b.ToTable("PositonAndDates");
                });
#pragma warning restore 612, 618
        }
    }
}
