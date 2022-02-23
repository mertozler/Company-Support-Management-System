using EntityLayer.Concrete;
using System.Collections.Generic;

namespace CompanyPanelUI.Models.GetFirmViewModel
{
    public class GetFirmViewModel
    {
        public Firm firm { get; set; }
        public string FirmName { get; set; }
        public string FirmMail { get; set; }
        public int FirmTaxNo { get; set; }
        public string FirmTelNo { get; set; }
        public List<CustomUser> firmUsers { get; set; }
        public List<Service> firmService { get; set; }  

    }
}
