using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class ServiceDepartment
    {
        [Key] 
        public int ServiceDepartmentId { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}