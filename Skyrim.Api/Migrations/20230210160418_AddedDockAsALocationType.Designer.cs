﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Skyrim.Api.Data;

#nullable disable

namespace Skyrim.Api.Migrations
{
    [DbContext(typeof(SkyrimApiDbContext))]
    [Migration("20230210160418_AddedDockAsALocationType")]
    partial class AddedDockAsALocationType
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence("BuildingSequence");

            modelBuilder.HasSequence("CreatureSequence");

            modelBuilder.HasSequence("LocationSequence");

            modelBuilder.HasSequence("PersonSequence");

            modelBuilder.Entity("Skyrim.Api.Data.AbstractModels.Building", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR [BuildingSequence]");

                    SqlServerPropertyBuilderExtensions.UseSequence(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PatrollerId")
                        .HasColumnType("int");

                    b.Property<int>("TypeOfBuilding")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("PatrollerId");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("Skyrim.Api.Data.AbstractModels.Creature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR [CreatureSequence]");

                    SqlServerPropertyBuilderExtensions.UseSequence(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PatrollerId")
                        .HasColumnType("int");

                    b.Property<int>("TypeOfCreature")
                        .HasColumnType("int");

                    b.Property<int>("locationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PatrollerId");

                    b.HasIndex("locationId");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("Skyrim.Api.Data.AbstractModels.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR [LocationSequence]");

                    SqlServerPropertyBuilderExtensions.UseSequence(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GeographicalDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PatrollerId")
                        .HasColumnType("int");

                    b.Property<int>("TypeOfLocation")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PatrollerId");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("Skyrim.Api.Data.AbstractModels.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR [PersonSequence]");

                    SqlServerPropertyBuilderExtensions.UseSequence(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PatrollerId")
                        .HasColumnType("int");

                    b.Property<int>("TypeOfPerson")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("PatrollerId");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("Skyrim.Api.Data.AbstractModels.Shop", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Building");

                    b.Property<int>("TypeOfShop")
                        .HasColumnType("int");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.Chicken", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Creature");

                    b.ToTable("Chickens");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.Camp", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("Camps");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.Cave", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("Caves");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.City", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.Clearing", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("Clearings");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.DaedricShrine", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("DaedricShrines");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.Dock", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("Docks");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.Homestead", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("Homesteads");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.Landmark", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("Landmarks");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.Settlement", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("Settlements");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.StandingStone", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("StandingStones");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.Town", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("Towns");
                });

            modelBuilder.Entity("Skyrim.Api.Data.AbstractModels.Patroller", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Person");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.PhyscialFightingShop", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Shop");

                    b.Property<bool>("HasBlackSmithStation")
                        .HasColumnType("bit");

                    b.ToTable("PhyscialFightingShops");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.Guard", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Patroller");

                    b.ToTable("Guards");
                });

            modelBuilder.Entity("Skyrim.Api.Data.AbstractModels.Building", b =>
                {
                    b.HasOne("Skyrim.Api.Data.AbstractModels.Location", null)
                        .WithMany("Buildings")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Skyrim.Api.Data.AbstractModels.Patroller", null)
                        .WithMany("PatrolledBuildings")
                        .HasForeignKey("PatrollerId");
                });

            modelBuilder.Entity("Skyrim.Api.Data.AbstractModels.Creature", b =>
                {
                    b.HasOne("Skyrim.Api.Data.AbstractModels.Patroller", null)
                        .WithMany("PatrolledCreatures")
                        .HasForeignKey("PatrollerId");

                    b.HasOne("Skyrim.Api.Data.AbstractModels.Location", null)
                        .WithMany("Creatures")
                        .HasForeignKey("locationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Skyrim.Api.Data.AbstractModels.Location", b =>
                {
                    b.HasOne("Skyrim.Api.Data.AbstractModels.Patroller", null)
                        .WithMany("PatrolledLocations")
                        .HasForeignKey("PatrollerId");
                });

            modelBuilder.Entity("Skyrim.Api.Data.AbstractModels.Person", b =>
                {
                    b.HasOne("Skyrim.Api.Data.AbstractModels.Location", null)
                        .WithMany("People")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Skyrim.Api.Data.AbstractModels.Patroller", null)
                        .WithMany("PatrolledPeople")
                        .HasForeignKey("PatrollerId");
                });

            modelBuilder.Entity("Skyrim.Api.Data.AbstractModels.Location", b =>
                {
                    b.Navigation("Buildings");

                    b.Navigation("Creatures");

                    b.Navigation("People");
                });

            modelBuilder.Entity("Skyrim.Api.Data.AbstractModels.Patroller", b =>
                {
                    b.Navigation("PatrolledBuildings");

                    b.Navigation("PatrolledCreatures");

                    b.Navigation("PatrolledLocations");

                    b.Navigation("PatrolledPeople");
                });
#pragma warning restore 612, 618
        }
    }
}
