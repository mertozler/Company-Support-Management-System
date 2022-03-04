using System.Collections.Generic;
using EntityLayer.Concrete;

namespace CompanyPanelUI.DTOS
{
    public class ServicesDTO
    {
        public string ServiceName { get; set; }
        public string ServiceAbout { get; set; }
        public List<Department> Departments { get; set; }
        public List<Service> Services { get; set; }
        public int departmentId { get; set; }
    }
}
