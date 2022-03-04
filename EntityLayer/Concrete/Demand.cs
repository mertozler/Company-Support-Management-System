using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Demand
    {
        [Key]
        public int DemandId { get; set; }
        public int ServiceId { get; set; }
        public Service Service {get; set;}
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public string UserId { get; set; }
        public string DemandTitle { get; set; }
        public string DemandContent { get; set; }
        public DateTime DemandCreateTime { get; set; }
        public DateTime? DemandReadTime { get; set; }
        public bool DemandStatus { get; set; }
    }
}
