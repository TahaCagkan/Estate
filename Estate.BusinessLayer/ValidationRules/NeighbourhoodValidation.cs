using Estate.EntityLayer.Entities;
using FluentValidation;

namespace Estate.BusinessLayer.ValidationRules
{
    public class NeighbourhoodValidation : AbstractValidator<Neighbourhood>
    {
        public NeighbourhoodValidation()
        {
            RuleFor(x => x.NeighbourhoodName).NotEmpty().WithMessage("Komşu bilgisi boş geçilemez!!!");
            RuleFor(x => x.DistrictId).NotEmpty().WithMessage("Semt alanı boş geçilemez!!!");

        }
    }
}
