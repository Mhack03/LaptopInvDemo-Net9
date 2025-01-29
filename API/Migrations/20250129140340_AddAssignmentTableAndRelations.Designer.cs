﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250129140340_AddAssignmentTableAndRelations")]
    partial class AddAssignmentTableAndRelations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("API.Models.Assignment", b =>
                {
                    b.Property<int>("AssignmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AssignmentId"));

                    b.Property<DateTime>("AssignedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LaptopSerialNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.HasKey("AssignmentId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("LaptopSerialNumber");

                    b.ToTable("Assignments");

                    b.HasData(
                        new
                        {
                            AssignmentId = 1,
                            AssignedDate = new DateTime(2025, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeId = 1001,
                            EmployeeName = "John Doe",
                            LaptopSerialNumber = "TZJHkvhCOe",
                            ReturnDate = new DateTime(2025, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            AssignmentId = 2,
                            AssignedDate = new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeId = 1002,
                            EmployeeName = "Jane Smith",
                            LaptopSerialNumber = "AQ5RrvbFR1",
                            ReturnDate = new DateTime(2025, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            AssignmentId = 3,
                            AssignedDate = new DateTime(2025, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeId = 1003,
                            EmployeeName = "Alice Johnson",
                            LaptopSerialNumber = "jdEluAbfxz",
                            ReturnDate = new DateTime(2025, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            AssignmentId = 4,
                            AssignedDate = new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeId = 1004,
                            EmployeeName = "Bob Williams",
                            LaptopSerialNumber = "fI5D0yyqxc",
                            ReturnDate = new DateTime(2025, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            AssignmentId = 5,
                            AssignedDate = new DateTime(2025, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeId = 1005,
                            EmployeeName = "Emily Brown",
                            LaptopSerialNumber = "Drjg7rPPIb",
                            ReturnDate = new DateTime(2025, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"), 1001L);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeId = 1001,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Department = "",
                            Email = "john.doe@example.com",
                            Name = "John Doe",
                            Position = "Manager",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            EmployeeId = 1002,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Department = "",
                            Email = "jane.smith@example.com",
                            Name = "Jane Smith",
                            Position = "Developer",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            EmployeeId = 1003,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Department = "",
                            Email = "alice.johnson@example.com",
                            Name = "Alice Johnson",
                            Position = "Designer",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            EmployeeId = 1004,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Department = "",
                            Email = "bob.williams@example.com",
                            Name = "Bob Williams",
                            Position = "Developer",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            EmployeeId = 1005,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Department = "",
                            Email = "emily.brown@example.com",
                            Name = "Emily Brown",
                            Position = "Designer",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("API.Models.Laptop", b =>
                {
                    b.Property<string>("SerialNumber")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<int>("Brand")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasBag")
                        .HasColumnType("bit");

                    b.Property<bool>("HasCharger")
                        .HasColumnType("bit");

                    b.Property<bool>("HasKeyboard")
                        .HasColumnType("bit");

                    b.Property<bool>("HasMouse")
                        .HasColumnType("bit");

                    b.Property<int>("LaptopId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LaptopId"), 1001L);

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("OperatingSystem")
                        .HasColumnType("int");

                    b.Property<string>("Processor")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Ram")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Storage")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("SerialNumber");

                    b.ToTable("Laptops");

                    b.HasData(
                        new
                        {
                            SerialNumber = "TZJHkvhCOe",
                            Brand = 0,
                            Description = "Lightweight and portable laptop for everyday use.",
                            HasBag = true,
                            HasCharger = true,
                            HasKeyboard = false,
                            HasMouse = false,
                            LaptopId = 1001,
                            Model = "Swift 3",
                            OperatingSystem = 1,
                            Processor = "Intel Core i5",
                            Ram = "8GB",
                            Status = 0,
                            Storage = "256GB SSD"
                        },
                        new
                        {
                            SerialNumber = "AQ5RrvbFR1",
                            Brand = 2,
                            Description = "Stylish and powerful laptop for professionals.",
                            HasBag = true,
                            HasCharger = true,
                            HasKeyboard = false,
                            HasMouse = false,
                            LaptopId = 1002,
                            Model = "Zenbook 14",
                            OperatingSystem = 1,
                            Processor = "AMD Ryzen 7",
                            Ram = "16GB",
                            Status = 0,
                            Storage = "512GB SSD"
                        },
                        new
                        {
                            SerialNumber = "jdEluAbfxz",
                            Brand = 3,
                            Description = "Premium ultrabook with excellent display.",
                            HasBag = true,
                            HasCharger = true,
                            HasKeyboard = false,
                            HasMouse = false,
                            LaptopId = 1003,
                            Model = "XPS 13",
                            OperatingSystem = 1,
                            Processor = "Intel Core i7",
                            Ram = "16GB",
                            Status = 0,
                            Storage = "1TB SSD"
                        },
                        new
                        {
                            SerialNumber = "fI5D0yyqxc",
                            Brand = 4,
                            Description = "Versatile 2-in-1 laptop.",
                            HasBag = true,
                            HasCharger = true,
                            HasKeyboard = false,
                            HasMouse = false,
                            LaptopId = 1004,
                            Model = "Spectre x360",
                            OperatingSystem = 1,
                            Processor = "Intel Core i7",
                            Ram = "16GB",
                            Status = 0,
                            Storage = "1TB SSD"
                        },
                        new
                        {
                            SerialNumber = "Drjg7rPPIb",
                            Brand = 5,
                            Description = "Business-oriented laptop.",
                            HasBag = true,
                            HasCharger = true,
                            HasKeyboard = false,
                            HasMouse = false,
                            LaptopId = 1005,
                            Model = "ThinkPad X1 Carbon",
                            OperatingSystem = 1,
                            Processor = "Intel Core i7",
                            Ram = "32GB",
                            Status = 0,
                            Storage = "1TB SSD"
                        });
                });

            modelBuilder.Entity("API.Models.Assignment", b =>
                {
                    b.HasOne("API.Models.Employee", "Employee")
                        .WithMany("Assignments")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Laptop", "Laptop")
                        .WithMany()
                        .HasForeignKey("LaptopSerialNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Laptop");
                });

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.Navigation("Assignments");
                });
#pragma warning restore 612, 618
        }
    }
}
