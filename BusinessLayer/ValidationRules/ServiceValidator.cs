using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class ServiceValidator : AbstractValidator<Service>
    {
        public ServiceValidator()
        {
            RuleFor(x => x.ServiceName).NotEmpty().WithMessage("Servis adı boş geçilemez");
            RuleFor(x => x.ServiceAbout).NotEmpty().WithMessage("Servis açıklaması boş geçilemez");
            RuleFor(x => x.ServiceName).MinimumLength(2).WithMessage("Servis adı 2 karakterden daha fazla olmalı");
            RuleFor(x => x.ServiceAbout).MinimumLength(2).WithMessage("Servis açıklaması 2 karakterden daha fazla olmalı");
            RuleFor(x => x.ServiceName).MaximumLength(100).WithMessage("Servis adı 100 karakterden daha az olmalı");
            RuleFor(x => x.ServiceAbout).MaximumLength(100).WithMessage("Servis açıklaması 100 karakterden daha az olmalı");

        }
    }
}
