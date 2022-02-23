using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class CustomUser:IdentityUser
    {
        [Display(Name = "Ad Soyad")]
        public string NameSurname { get; set; }

        [Display(Name = "Başvurulan Şirket")]
        public string ApplicationFirm { get; set; }

        [Display(Name = "Firma Id")]
        public int? FirmId { get; set; }

    }
}
