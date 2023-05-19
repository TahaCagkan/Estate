using Estate.EntityLayer.Entities;
using FluentValidation;

namespace Estate.BusinessLayer.ValidationRules
{
    public class ImagesValidation : AbstractValidator<Images>
    {
        public ImagesValidation()
        {
            RuleFor(x => x.Image).NotEmpty().WithMessage("Resim bilgisi boş geçilemez!!!");
        }
    }
}
