using System.Collections.Generic;
using EntityLayer.Concrete;

namespace CompanyPanelUI.Models.EmployeeDemandAssignmentViewModel
{
    public class EmployeeDemandAssignmentViewModel
    {
        public List<Demand> Demand { get; set; }
        public IList<CustomUser> Employees { get; set; }
        public string employeeId { get; set; }
        public int demandId { get; set; }
    }
}