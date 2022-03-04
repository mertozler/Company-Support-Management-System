using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules
{
    public class EmployeeValidator:AbstractValidator<CustomUser>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Mail boş geçilemez");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon numarası boş geçilemez");
            RuleFor(x => x.NameSurname).NotEmpty().WithMessage("Personel adı boş geçilemez");
            
        }
    }
}