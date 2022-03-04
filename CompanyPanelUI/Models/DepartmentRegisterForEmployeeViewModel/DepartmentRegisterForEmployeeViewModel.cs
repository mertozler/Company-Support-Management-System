using System.Collections.Generic;
using EntityLayer.Concrete;

namespace CompanyPanelUI.Models.DepartmentRegisterForEmployeeViewModel
{
    public class DepartmentRegisterForEmployeeViewModel
    {
        public CustomUser User { get; set; }
        public List<Department> allDepartment { get; set; }
        public List<Service> getServicesForSelectedDepartment { get; set; }
        public int defaultDepartmentId { get; set; }
        public List<Firm> allFirms { get; set; }
        public int selectedDepartment { get; set; }
        public int serviceId { get; set; }
        public string UserId { get; set; }
        public int DepartmentId { get; set; }
        public int FirmId { get; set; }
        
    }
}