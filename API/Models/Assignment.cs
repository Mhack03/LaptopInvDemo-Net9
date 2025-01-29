using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Assignment
    {
        [Key]
        public int AssignmentId { get; set; }
        [Required] 
        public string LaptopSerialNumber { get; set; } = string.Empty;
        [Required]
        public int EmployeeId { get; set; }
        public string? Department { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public DateTime AssignedDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public Laptop? Laptop { get; set; }
        public Employee? Employee { get; set; }



        public void ReturnLaptop()
        {
            if(ReturnDate == null)
            {
                ReturnDate = DateTime.Now;
                if (Laptop != null)
                {
                    Laptop.Status = Status.Available;
                }
            }
            
        }

        public void AssignEmployee(Employee employee, Laptop laptop)
        {
            if (laptop == null)
            {
                throw new ArgumentNullException(nameof(laptop), "Laptop cannot be null");
            }

            Employee = employee;
            Laptop = laptop;
            EmployeeName = employee.Name;
            this.LaptopSerialNumber = laptop.SerialNumber;
            Department = employee.Department;

            // Update the laptop's status to InUse
            if (laptop.Status == Status.Available)
            {
                laptop.Status = Status.InUse;
            }

            employee.Assignments?.Add(this);
        }
    }

}


