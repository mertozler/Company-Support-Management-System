using System.Collections.Generic;
using EntityLayer.Concrete;

namespace CompanyPanelUI.Models.EmployeeListViewModel
{
    public class EmployeeListViewModel
    {
       
            public List<EmployeeWM> Users { get; set; }
        
        
    }
    public class EmployeeWM
    {
        public string EmployeeId { get; set; }
        public string EmployeeNameSurname { get; set; }
        public string EmployeeMail { get; set; }
        public string EmployeePhone { get; set; }
        public Firm Firm { get; set; }
    }
}