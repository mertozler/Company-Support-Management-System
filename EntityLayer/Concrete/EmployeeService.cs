using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class EmployeeService
    {
        [Key]
        public int EmployeeServicesId { get; set; }
        public int ServiceId { get; set; }
        public Service Services { get; set; }
        public string Id { get; set; }
        public CustomUser User { get; set; }
    }
}