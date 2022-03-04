using System.Collections.Generic;
using EntityLayer.Concrete;

namespace CompanyPanelUI.Models.ServiceListViewModel
{
    public class ServiceListViewModel
    {
        
        public List<ServiceListClass> ServiceListClasses { get; set; }
    }

    public class ServiceListClass
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string ServiceAbout { get; set; }
        public string DepartmentName { get; set; }
    }
}