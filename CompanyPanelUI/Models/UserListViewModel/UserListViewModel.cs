using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyPanelUI.Models.UserListViewModel
{
    public class UserListViewModel
    {
        public List<UserWm> Users { get; set; }
    }
    public class UserWm
    {
        public string UserId { get; set; }
        public string UserNameSurname { get; set; }
        public string UserMail { get; set; }
        public string UserPhone { get; set; }
        public Firm Firm { get; set; }
    }
}
