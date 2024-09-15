﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Qinshift.MovieRent.DataAccess;

#nullable disable

namespace Qinshift.MovieRent.DataAccess.Migrations
{
    [DbContext(typeof(MovieRentDbContext))]
    [Migration("20240915113200_AddedUserEntity")]
    partial class AddedUserEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.33")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Qinshift.MovieRent.DomainModels.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<string>("Plot")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedOn = new DateTime(2024, 9, 15, 13, 32, 0, 431, DateTimeKind.Local).AddTicks(1744),
                            Genre = 1,
                            Plot = "A concierge teams up with one of his employees to prove his innocence after he is framed for murder.",
                            ReleaseDate = new DateTime(2014, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "The Grand Budapest Hotel"
                        },
                        new
                        {
                            Id = 2,
                            CreatedOn = new DateTime(2024, 9, 15, 13, 32, 0, 431, DateTimeKind.Local).AddTicks(1787),
                            Genre = 1,
                            Plot = "Two high school friends attempt to make it to a party before they go off to college.",
                            ReleaseDate = new DateTime(2007, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Superbad"
                        },
                        new
                        {
                            Id = 3,
                            CreatedOn = new DateTime(2024, 9, 15, 13, 32, 0, 431, DateTimeKind.Local).AddTicks(1791),
                            Genre = 2,
                            Plot = "In a post-apocalyptic world, Max teams up with Furiosa to escape a tyrant and his army.",
                            ReleaseDate = new DateTime(2015, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Mad Max: Fury Road"
                        },
                        new
                        {
                            Id = 4,
                            CreatedOn = new DateTime(2024, 9, 15, 13, 32, 0, 431, DateTimeKind.Local).AddTicks(1793),
                            Genre = 2,
                            Plot = "A NYPD officer tries to save his wife and others taken hostage by German terrorists during a Christmas party.",
                            ReleaseDate = new DateTime(1988, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Die Hard"
                        },
                        new
                        {
                            Id = 5,
                            CreatedOn = new DateTime(2024, 9, 15, 13, 32, 0, 431, DateTimeKind.Local).AddTicks(1796),
                            Genre = 3,
                            Plot = "A man becomes the focus of an intense media circus when his wife disappears and he is suspected of murder.",
                            ReleaseDate = new DateTime(2014, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Gone Girl"
                        },
                        new
                        {
                            Id = 6,
                            CreatedOn = new DateTime(2024, 9, 15, 13, 32, 0, 431, DateTimeKind.Local).AddTicks(1800),
                            Genre = 3,
                            Plot = "A thief who enters the dreams of others is given the chance to erase his criminal record by planting an idea into someone's subconscious.",
                            ReleaseDate = new DateTime(2010, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Inception"
                        },
                        new
                        {
                            Id = 7,
                            CreatedOn = new DateTime(2024, 9, 15, 13, 32, 0, 431, DateTimeKind.Local).AddTicks(1803),
                            Genre = 4,
                            Plot = "Paranormal investigators help a family terrorized by a dark presence in their farmhouse.",
                            ReleaseDate = new DateTime(2013, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "The Conjuring"
                        },
                        new
                        {
                            Id = 8,
                            CreatedOn = new DateTime(2024, 9, 15, 13, 32, 0, 431, DateTimeKind.Local).AddTicks(1806),
                            Genre = 4,
                            Plot = "A young African-American man visits his white girlfriend's family estate, where he uncovers a disturbing secret.",
                            ReleaseDate = new DateTime(2017, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Get Out"
                        },
                        new
                        {
                            Id = 9,
                            CreatedOn = new DateTime(2024, 9, 15, 13, 32, 0, 431, DateTimeKind.Local).AddTicks(1808),
                            Genre = 5,
                            Plot = "Two imprisoned men bond over a number of ReleaseDates, finding solace and eventual redemption through acts of common decency.",
                            ReleaseDate = new DateTime(1994, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "The Shawshank Redemption"
                        },
                        new
                        {
                            Id = 10,
                            CreatedOn = new DateTime(2024, 9, 15, 13, 32, 0, 431, DateTimeKind.Local).AddTicks(1811),
                            Genre = 5,
                            Plot = "The story of a man with a low IQ, who achieves great things in life despite the odds.",
                            ReleaseDate = new DateTime(1994, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Forrest Gump"
                        });
                });

            modelBuilder.Entity("Qinshift.MovieRent.DomainModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
