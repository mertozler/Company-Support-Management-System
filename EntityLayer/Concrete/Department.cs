using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentAbout { get; set; }
        public bool DepartmentisDefault { get; set; }
    }
}