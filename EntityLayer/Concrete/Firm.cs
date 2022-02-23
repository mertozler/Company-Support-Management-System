using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Firm
    {
        [Key]
        public int FirmId { get; set; }
        public string FirmName { get; set; }
        public int FirmTaxNo { get; set; }
        public string FirmTelNo { get; set; }
        public string FirmMail { get; set; }
        public bool FirmStatus { get; set; }
    }
}
