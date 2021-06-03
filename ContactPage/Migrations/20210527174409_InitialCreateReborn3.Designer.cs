﻿// <auto-generated />
using System;
using ContactPage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ContactPage.Migrations
{
    [DbContext(typeof(EmployeeContext))]
    [Migration("20210527174409_InitialCreateReborn3")]
    partial class InitialCreateReborn3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ContactPage.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("CompletedProjects")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(50);

                    b.Property<string>("FieldOfActivity")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(50);

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("ContactPage.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AssignedEmployee");

                    b.Property<string>("Company")
                        .IsRequired();

                    b.Property<string>("DescriptionInfo");

                    b.Property<string>("DescriptionLicense");

                    b.Property<string>("DescriptionProgramm");

                    b.Property<Guid>("GuidId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("NameDescriptionInfo");

                    b.Property<string>("NameDescriptionLicense");

                    b.Property<string>("NameDescriptionProgramm");

                    b.Property<string>("Serial")
                        .IsRequired();

                    b.Property<int>("TypeItem");

                    b.Property<string>("WhereIs");

                    b.HasKey("ItemId");

                    b.ToTable("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
