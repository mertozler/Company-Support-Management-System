using EntityLayer.Concrete;
using System;
using System.Collections.Generic;

namespace CompanyPanelUI.Models.DemandListViewModel
{
    public class DemandListViewModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool CevaplanmamisTalep { get; set; }
        public bool Yanitlanmistalep { get; set; }
        public int? ServiceId { get; set; }
        public List<Service> Services { get; set; }
        public List<Demand> Demands { get; set; }

    }
}
