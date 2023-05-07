using Estate.EntityLayer.Entities;
using FluentValidation;

namespace Estate.BusinessLayer.ValidationRules
{
    public class CityValidation : AbstractValidator<City>
    {
        public CityValidation()
        {
            RuleFor(x => x.CityName).NotEmpty().WithMessage("Şehir adı bilgisi boş geçilemez!!!");
            RuleFor(x => x.CityName).MinimumLength(3).MaximumLength(50).WithMessage("En az 3, En çok 50 karakter girilmek zorunda!!!");

        }
    }
}
