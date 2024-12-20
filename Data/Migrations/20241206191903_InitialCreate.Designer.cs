﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningDotNet.Data.Migrations;

[DbContext(typeof(GameStoreContext))]
[Migration("20241206191903_InitialCreate")]
partial class InitialCreate
{
    /// <inheritdoc />
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

        modelBuilder.Entity("LearningDotNet.Entities.Game", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<int>("GenreId")
                    .HasColumnType("INTEGER");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<decimal>("Price")
                    .HasColumnType("TEXT");

                b.Property<DateOnly>("ReleaseDate")
                    .HasColumnType("TEXT");

                b.HasKey("Id");

                b.HasIndex("GenreId");

                b.ToTable("Games");
            });

        modelBuilder.Entity("LearningDotNet.Entities.Genre", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.HasKey("Id");

                b.ToTable("Genre");
            });

        modelBuilder.Entity("LearningDotNet.Entities.Game", b =>
            {
                b.HasOne("LearningDotNet.Entities.Genre", "Genre")
                    .WithMany()
                    .HasForeignKey("GenreId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Genre");
            });
#pragma warning restore 612, 618
    }
}
