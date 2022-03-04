using System.Collections.Generic;
using EntityLayer.Concrete;

namespace CompanyPanelUI.Models.EmployeeDemandDepartmentAssignmentViewModel
{
    public class EmployeeDemandDepartmentAssignmentViewModel
    {
        public List<Demand> Demand { get; set; }
        public List<Department> Department { get; set; }
        public int demandId { get; set; }
        public int departmentId { get; set; }
    }
}