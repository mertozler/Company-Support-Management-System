using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyPanelUI.Models.UsersinRoleViewModel
{
    public class UsersinRoleViewModel
    {
        public string UserId { get; set; }
        public string NameSurname { get; set; }
        public string PhoneNumber { get; set; }
        public int FirmId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
