﻿// <auto-generated />
using System;
using DabeaV2.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DabeaV2.DB.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0-preview6.19304.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DabeaV2.Entities.Benutzer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsExtern");

                    b.Property<DateTime?>("LastLogin");

                    b.Property<string>("Passwort");

                    b.Property<long>("PersonId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Benutzer");
                });

            modelBuilder.Entity("DabeaV2.Entities.Kontakt", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<bool>("IsActive");

                    b.Property<long>("PersonId");

                    b.Property<string>("Telefon");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Kontakte");
                });

            modelBuilder.Entity("DabeaV2.Entities.Modification", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("BenutzerId");

                    b.Property<long?>("ChangedBenutzerId");

                    b.Property<long?>("ChangedKontaktId");

                    b.Property<long?>("ChangedPersonId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("ModificationType");

                    b.HasKey("Id");

                    b.HasIndex("BenutzerId");

                    b.HasIndex("ChangedBenutzerId");

                    b.HasIndex("ChangedKontaktId");

                    b.HasIndex("ChangedPersonId");

                    b.ToTable("Modifications");
                });

            modelBuilder.Entity("DabeaV2.Entities.ModificationItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("ModificationId");

                    b.Property<string>("NewValue");

                    b.Property<string>("OldValue");

                    b.Property<string>("PropertyName");

                    b.HasKey("Id");

                    b.HasIndex("ModificationId");

                    b.ToTable("ModificationItems");
                });

            modelBuilder.Entity("DabeaV2.Entities.Person", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.Property<string>("VorName");

                    b.HasKey("Id");

                    b.ToTable("Personen");
                });

            modelBuilder.Entity("DabeaV2.Entities.Benutzer", b =>
                {
                    b.HasOne("DabeaV2.Entities.Person", "Person")
                        .WithMany("Benutzer")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DabeaV2.Entities.Kontakt", b =>
                {
                    b.HasOne("DabeaV2.Entities.Person", "Person")
                        .WithMany("Kontakte")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DabeaV2.Entities.Modification", b =>
                {
                    b.HasOne("DabeaV2.Entities.Benutzer", "Benutzer")
                        .WithMany("OwnModifications")
                        .HasForeignKey("BenutzerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DabeaV2.Entities.Benutzer", "ChangedBenutzer")
                        .WithMany("Modifications")
                        .HasForeignKey("ChangedBenutzerId");

                    b.HasOne("DabeaV2.Entities.Kontakt", "ChangedKontakt")
                        .WithMany("Modifications")
                        .HasForeignKey("ChangedKontaktId");

                    b.HasOne("DabeaV2.Entities.Person", "ChangedPerson")
                        .WithMany("Modifications")
                        .HasForeignKey("ChangedPersonId");
                });

            modelBuilder.Entity("DabeaV2.Entities.ModificationItem", b =>
                {
                    b.HasOne("DabeaV2.Entities.Modification", "Modification")
                        .WithMany("ModificationItems")
                        .HasForeignKey("ModificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
