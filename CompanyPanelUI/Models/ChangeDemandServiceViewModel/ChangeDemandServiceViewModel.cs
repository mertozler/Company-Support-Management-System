using System.Collections.Generic;
using EntityLayer.Concrete;

namespace CompanyPanelUI.Models.ChangeDemandServiceViewModel
{
    public class ChangeDemandServiceViewModel
    {
        public List<Demand> Demand { get; set; }
        public List<Service> Service { get; set; }
        public int serviceId { get; set; }
        public int demandId { get; set; }
    }
}