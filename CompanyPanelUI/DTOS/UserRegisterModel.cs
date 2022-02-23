using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CompanyPanelUI.DTOS
{
    public class UserRegisterModel
    {
        public int UserId { get; set; }
        public int FirmId { get; set; }
        public string UserPhone { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public List<Firm> Firm { get; set; }
        public List<User> User { get; set; }
        public string Role { get; set; }
    }
}
