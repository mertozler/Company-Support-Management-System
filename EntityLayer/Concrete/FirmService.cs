using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class FirmService
    {
        [Key]
        public int FirmServiceId { get; set; }
        public int FirmId { get; set; }
        public Firm Firm { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        public DateTime FirmServiceCreateDate { get; set; }
    }
}
