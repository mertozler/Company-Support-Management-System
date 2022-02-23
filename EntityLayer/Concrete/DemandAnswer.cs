using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class DemandAnswer
    {
        [Key]
        public int DemandAnswersId { get; set; }
        public int DemandId { get; set; }
        public Demand Demand { get; set; }
        public DateTime  DemandAnswerDate { get; set; }
        public string Answer { get; set; }
    }
}
