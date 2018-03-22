﻿// <auto-generated />
using core_classe.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace coreclasse.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("core_classe.Models.Nota", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Observacions")
                        .HasMaxLength(50);

                    b.Property<int?>("alumneId");

                    b.Property<int>("nota");

                    b.Property<int>("persona_fk");

                    b.HasKey("Id");

                    b.HasIndex("alumneId");

                    b.ToTable("Nota");
                });

            modelBuilder.Entity("core_classe.Models.Persona", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cognoms")
                        .HasMaxLength(150);

                    b.Property<string>("Nom")
                        .HasMaxLength(50);

                    b.Property<string>("Perfil");

                    b.HasKey("Id");

                    b.ToTable("Gent");
                });

            modelBuilder.Entity("core_classe.Models.Nota", b =>
                {
                    b.HasOne("core_classe.Models.Persona", "alumne")
                        .WithMany("notes")
                        .HasForeignKey("alumneId");
                });
#pragma warning restore 612, 618
        }
    }
}
