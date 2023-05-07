using Estate.EntityLayer.Entities;
using FluentValidation;

namespace Estate.BusinessLayer.ValidationRules
{
    public class GeneralSettingsValidation : AbstractValidator<GeneralSettings>
    {
        public GeneralSettingsValidation()
        {
            RuleFor(x => x.Address).NotEmpty().WithMessage("Adres bilgisi boş geçilemez!!!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email bilgisi boş geçilemez!!!");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon Numarası boş geçilemez!!!");
            RuleFor(x => x.ImageName).NotEmpty().WithMessage("Resim adı boş geçilemez!!!");

        }
    }
}
