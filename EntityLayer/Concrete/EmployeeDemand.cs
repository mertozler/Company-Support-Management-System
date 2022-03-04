using System;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class EmployeeDemand
    {
        [Key]
        public int EmployeeDemandId { get; set; }
        public int DemandId { get; set; }
        public Demand Demand { get; set; }
        public string EmployeeId { get; set; }
        public CustomUser Employee { get; set; }
        public DateTime CreateTime { get; set; }
    }
}