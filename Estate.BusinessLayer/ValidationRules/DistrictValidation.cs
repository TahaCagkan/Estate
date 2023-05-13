using Estate.EntityLayer.Entities;
using FluentValidation;

namespace Estate.BusinessLayer.ValidationRules
{
    public class DistrictValidation : AbstractValidator<District>
    {
        public DistrictValidation()
        {
            RuleFor(x => x.DistrictName).NotEmpty().WithMessage("Semt adı bilgisi boş geçilemez!!!");
            RuleFor(x => x.CityId).NotEmpty().WithMessage("Şehir alanı boş geçilemez!!!");
        }
    }
}
