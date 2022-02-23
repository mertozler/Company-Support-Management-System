using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CompanyPanelUI.DTOS
{
    public class AddDemandModel
    {
        public List<Service> Service { get; set; }
        public int ServiceId { get; set; }
        public string DemandTitle { get; set; }
        public string DemandContent { get; set; }
        public List<IFormFile> DemandFile { get; set; }
        public List<CustomUser> User { get; set; }
        public List<FirmService> FirmService { get; set; }
        public List<Firm> Firm { get; set; }
    }
}
