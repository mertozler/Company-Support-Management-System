using System;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class DepartmentEmployee
    {
        [Key]
        public int DepartmentEmployeeId { get; set; }
        public string Id { get; set; }
        public CustomUser User { get; set; }
        public int DepartmentId { get; set; }
    }
}