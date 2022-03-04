using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class FirmValidator:AbstractValidator<Firm>
    {
        public FirmValidator()
        {
            RuleFor(x => x.FirmMail).NotEmpty().WithMessage("Mail boş geçilemez");
            RuleFor(x => x.FirmTelNo).NotEmpty().WithMessage("Telefon numarası boş geçilemez");
            RuleFor(x => x.FirmName).NotEmpty().WithMessage("Firma adı boş geçilemez");
            RuleFor(x => x.FirmTaxNo).NotEmpty().WithMessage("Firma adı boş geçilemez");
            RuleFor(x => x.FirmTaxNo).MaximumLength(11).WithMessage("Vergi numarası 11 karakterden fazla olamaz");
            RuleFor(x => x.FirmTaxNo).MinimumLength(11).WithMessage("Vergi numarası 11 karakterden az olamaz");
            RuleFor(x => x.FirmName).MinimumLength(2).WithMessage("Lütfen en az 2 karakter girişi yapın");
            RuleFor(x => x.FirmName).MaximumLength(200).WithMessage("Lütfen en fazla 200 karakter girişi yapın");
            RuleFor(x => x.FirmMail).MinimumLength(2).WithMessage("Lütfen en az 5 karakter girişi yapın");
            RuleFor(x => x.FirmMail).MaximumLength(100).WithMessage("Lütfen fazla 100 karakter girişi yapın");
            RuleFor(x => x.FirmTelNo).MinimumLength(10).WithMessage("Lütfen en az 6 karakter girişi yapın");
            RuleFor(x => x.FirmTelNo).MaximumLength(13).WithMessage("Lütfen en fazla 10 karakter girişi yapın");

            
        }
    }
}
