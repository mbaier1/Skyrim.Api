﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Skyrim.Api.Data;

#nullable disable

namespace Skyrim.Api.Migrations
{
    [DbContext(typeof(SkyrimApiDbContext))]
    partial class SkyrimApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
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

            modelBuilder.Entity("Skyrim.Api.Data.Models.BodyOfWater", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("BodiesOfWater");
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

            modelBuilder.Entity("Skyrim.Api.Data.Models.DragonLair", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("DragonLairs");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.DwarvenRuin", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("DwarvenRuins");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.Farm", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("Farms");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.Fort", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("Forts");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.GiantCamp", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("GiantCamps");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.Grove", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("Groves");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.Homestead", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("Homesteads");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.ImperialCamp", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("ImperialCamps");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.InnOrTavern", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("InnsOrTaverns");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.Landmark", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("Landmarks");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.LightHouse", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("LightHouses");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.LumberMill", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("LumberMills");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.Mine", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("Mines");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.NordicTower", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("NordicTowers");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.OrcStronghold", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("OrcStrongholds");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.Pass", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("Passes");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.Ruin", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("Ruins");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.Settlement", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("Settlements");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.Shack", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("Shacks");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.Ship", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("Ships");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.Shipwreck", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("Shipwrecks");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.Stable", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("Stables");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.StandingStone", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("StandingStones");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.StormcloakCamp", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("StormcloakCamps");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.Temple", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("Temples");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.Tomb", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("Tombs");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.Town", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("Towns");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.Watchtower", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("Watchtowers");
                });

            modelBuilder.Entity("Skyrim.Api.Data.Models.WheatMill", b =>
                {
                    b.HasBaseType("Skyrim.Api.Data.AbstractModels.Location");

                    b.ToTable("WheatMills");
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
