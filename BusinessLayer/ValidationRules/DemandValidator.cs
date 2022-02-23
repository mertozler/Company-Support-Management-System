using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class DemandValidator : AbstractValidator<Demand>
    {
        public DemandValidator()
        {
            RuleFor(x => x.DemandTitle).NotEmpty().WithMessage("Talep başlığı boş geçilemez");
            RuleFor(x => x.DemandTitle).MinimumLength(2).WithMessage("Talep başlığı 2 karakterden daha az olamaz");
            RuleFor(x => x.DemandTitle).MaximumLength(500).WithMessage("Talep başlığı 500 karakterden daha fazla olamaz");
            RuleFor(x => x.DemandContent).NotEmpty().WithMessage("Talep başlığı boş geçilemez");
            RuleFor(x => x.DemandContent).MinimumLength(20).WithMessage("Talep içeriği 20 karakterden daha az olamaz");
            RuleFor(x => x.DemandContent).MaximumLength(5000).WithMessage("Talep içeriği 5000 karakterden daha fazla olamaz");
            RuleFor(x => x.ServiceId).NotEmpty().WithMessage("Servis boş geçilemez");

        }
    }
}
