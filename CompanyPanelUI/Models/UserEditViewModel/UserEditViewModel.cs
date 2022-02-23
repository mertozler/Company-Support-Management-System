using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CompanyPanelUI.Models.UserEditViewModel
{
    public class UserEditViewModel
    {
        public string UserId { get; set; }
        public string UserNameSurname { get; set; }
        public string UserMail { get; set; }
        public string UserApplicationFirm { get; set; }
        public string UserPhone { get; set; }
        public int FirmId { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public List<Firm> Firm { get; set; }
        public CustomUser User { get; set; }
        public string Role { get; set; }
    }
}
