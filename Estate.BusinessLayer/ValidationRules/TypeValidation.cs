using FluentValidation;

namespace Estate.BusinessLayer.ValidationRules
{
    public class TypeValidation : AbstractValidator<EntityLayer.Entities.Type>
    {
        public TypeValidation()
        {
            RuleFor(x => x.TypeName).NotEmpty().WithMessage("Tip adı alanı boş geçilemez.");
            RuleFor(x => x.SituationId).NotEmpty().WithMessage("Durum adı alanı boş geçilemez.");
        }
    }
}
