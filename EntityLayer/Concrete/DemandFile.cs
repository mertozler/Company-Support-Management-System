using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class DemandFile
    {
        [Key]
        public int DemandFileId { get; set; }
        public int DemandId { get; set; }
        public Demand Demand { get; set; }
        public string DemandFilePath { get; set; }
    }
}
