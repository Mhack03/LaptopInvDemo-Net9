using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Laptop> Laptops { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure LaptopId to start from 1001
            modelBuilder.Entity<Laptop>()
                .Property(l => l.LaptopId)
                .UseIdentityColumn(seed: 1001, increment: 1);

            // Configure EmployeeId to start from 1001
            modelBuilder.Entity<Employee>()
                .Property(e => e.EmployeeId)
                .UseIdentityColumn(seed: 1001, increment: 1);
            
            // Seed data for Laptop
            modelBuilder.Entity<Laptop>().HasData(
                new Laptop
                {
                    LaptopId = 1001,
                    SerialNumber = "TZJHkvhCOe",
                    Brand = Brand.Acer,
                    Model = "Swift 3",
                    Processor = "Intel Core i5",
                    Ram = "8GB",
                    Storage = "256GB SSD",
                    OperatingSystem = Models.OperatingSystem.Windows_11,
                    Description = "Lightweight and portable laptop for everyday use.",
                    Status = Status.Available,
                    HasBag = true,
                    HasCharger = true
                },
                new Laptop
                {
                    LaptopId = 1002,
                    SerialNumber = "AQ5RrvbFR1",
                    Brand = Brand.Asus,
                    Model = "Zenbook 14",
                    Processor = "AMD Ryzen 7",
                    Ram = "16GB",
                    Storage = "512GB SSD",
                    OperatingSystem = Models.OperatingSystem.Windows_11,
                    Description = "Stylish and powerful laptop for professionals.",
                    Status = Status.Available,
                    HasBag = true,
                    HasCharger = true
                },
                new Laptop
                {
                    LaptopId = 1003,
                    SerialNumber = "jdEluAbfxz",
                    Brand = Brand.Dell,
                    Model = "XPS 13",
                    Processor = "Intel Core i7",
                    Ram = "16GB",
                    Storage = "1TB SSD",
                    OperatingSystem = Models.OperatingSystem.Windows_11,
                    Description = "Premium ultrabook with excellent display.",
                    Status = Status.Available,
                    HasBag = true,
                    HasCharger = true
                },
                new Laptop
                {
                    LaptopId = 1004,
                    SerialNumber = "fI5D0yyqxc",
                    Brand = Brand.HP,
                    Model = "Spectre x360",
                    Processor = "Intel Core i7",
                    Ram = "16GB",
                    Storage = "1TB SSD",
                    OperatingSystem = Models.OperatingSystem.Windows_11,
                    Description = "Versatile 2-in-1 laptop.",
                    Status = Status.Available,
                    HasBag = true,
                    HasCharger = true
                },
                new Laptop
                {
                    LaptopId = 1005,
                    SerialNumber = "Drjg7rPPIb",
                    Brand = Brand.Lenovo,
                    Model = "ThinkPad X1 Carbon",
                    Processor = "Intel Core i7",
                    Ram = "32GB",
                    Storage = "1TB SSD",
                    OperatingSystem = Models.OperatingSystem.Windows_11,
                    Description = "Business-oriented laptop.",
                    Status = Status.Available,
                    HasBag = true,
                    HasCharger = true
                }
            );

            // Seed data for Employee
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeId = 1001,
                    Name = "John Doe",
                    Email = "john.doe@example.com",
                    Position = "Manager"
                },
                new Employee
                {
                    EmployeeId = 1002,
                    Name = "Jane Smith",
                    Email = "jane.smith@example.com",
                    Position = "Developer"
                },
                new Employee
                {
                    EmployeeId = 1003,
                    Name = "Alice Johnson",
                    Email = "alice.johnson@example.com",
                    Position = "Designer"
                },
                new Employee
                {
                    EmployeeId = 1004,
                    Name = "Bob Williams",
                    Email = "bob.williams@example.com",
                    Position = "Developer"
                },
                new Employee
                {
                    EmployeeId = 1005,
                    Name = "Emily Brown",
                    Email = "emily.brown@example.com",
                    Position = "Designer"
                }
            );

            // Seed data for Assignment
            modelBuilder.Entity<Assignment>().HasData(
                new Assignment
                {
                    AssignmentId = 1,
                    LaptopSerialNumber = "TZJHkvhCOe",
                    EmployeeId = 1001,
                    EmployeeName = "John Doe",
                    Department = "Sales",
                    AssignedDate = new DateTime(2025, 1, 17),
                    ReturnDate = new DateTime(2025, 2, 17)
                },
                new Assignment
                {
                    AssignmentId = 2,
                    LaptopSerialNumber = "AQ5RrvbFR1",
                    EmployeeId = 1002,
                    EmployeeName = "Jane Smith",
                    Department = "Developer",
                    AssignedDate = new DateTime(2025, 1, 18),
                    ReturnDate = new DateTime(2025, 2, 17)
                },
                new Assignment   {
                    AssignmentId = 3,
                    LaptopSerialNumber = "jdEluAbfxz",
                    EmployeeId = 1003,
                    EmployeeName = "Alice Johnson",
                    Department = "Developer",
                    AssignedDate = new DateTime(2025, 1, 19),
                    ReturnDate = new DateTime(2025, 2, 17)
                },
                new Assignment
                {
                    AssignmentId = 4,
                    LaptopSerialNumber = "fI5D0yyqxc",
                    EmployeeId = 1004,
                    EmployeeName = "Bob Williams",
                    Department = "Developer",
                    AssignedDate = new DateTime(2025, 1, 20),
                    ReturnDate = new DateTime(2025, 2, 17)
                },
                new Assignment
                {
                    AssignmentId = 5,
                    LaptopSerialNumber = "Drjg7rPPIb",
                    EmployeeId = 1005,
                    EmployeeName = "Emily Brown",
                    Department = "Developer",
                    AssignedDate = new DateTime(2025, 1, 21),
                    ReturnDate = new DateTime(2025, 2, 17)
                }
            );

        }
    }
}
