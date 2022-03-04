using EntityLayer.Concrete;
using System.Collections.Generic;

namespace CompanyPanelUI.Models.ServiceViewModel
{
    public class ServiceViewModel
    {
        public List<Service> Services { get; set; }
        public List<Firm> Firms { get; set; }
        public int[] ServiceId { get; set; }
        public int FirmId { get; set; }
    }
}
