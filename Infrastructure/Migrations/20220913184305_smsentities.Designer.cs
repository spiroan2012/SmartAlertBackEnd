﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(SmartAlertContext))]
    [Migration("20220913184305_smsentities")]
    partial class smsentities
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Core.Entities.Incident", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<Point>("Coords")
                        .HasColumnType("geometry");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StatusChangeDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Incidents");
                });

            modelBuilder.Entity("Core.Entities.IncidentCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("SmsText")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Core.Entities.IncidentDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<Point>("Coords")
                        .HasColumnType("geometry");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("MasterIncidentId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StatusChangeDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserEmail")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("MasterIncidentId");

                    b.ToTable("IncidentDetails");
                });

            modelBuilder.Entity("Core.Entities.SmsDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("MobilePhone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("SmsMasterId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SmsMasterId");

                    b.ToTable("SmsDetails");
                });

            modelBuilder.Entity("Core.Entities.SmsMaster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("IncidentId")
                        .HasColumnType("integer");

                    b.Property<string>("SmsText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IncidentId");

                    b.ToTable("SmsMasters");
                });

            modelBuilder.Entity("Core.Entities.Incident", b =>
                {
                    b.HasOne("Core.Entities.IncidentCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Core.Entities.IncidentDetail", b =>
                {
                    b.HasOne("Core.Entities.IncidentCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Incident", "MasterIncident")
                        .WithMany("Details")
                        .HasForeignKey("MasterIncidentId");

                    b.Navigation("Category");

                    b.Navigation("MasterIncident");
                });

            modelBuilder.Entity("Core.Entities.SmsDetail", b =>
                {
                    b.HasOne("Core.Entities.SmsMaster", "SmsMaster")
                        .WithMany("SmsDetails")
                        .HasForeignKey("SmsMasterId");

                    b.Navigation("SmsMaster");
                });

            modelBuilder.Entity("Core.Entities.SmsMaster", b =>
                {
                    b.HasOne("Core.Entities.Incident", "Incident")
                        .WithMany()
                        .HasForeignKey("IncidentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Incident");
                });

            modelBuilder.Entity("Core.Entities.Incident", b =>
                {
                    b.Navigation("Details");
                });

            modelBuilder.Entity("Core.Entities.SmsMaster", b =>
                {
                    b.Navigation("SmsDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
