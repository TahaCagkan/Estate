using Estate.EntityLayer.Entities;
using FluentValidation;

namespace Estate.BusinessLayer.ValidationRules
{
    public class ImagesValidation : AbstractValidator<Images>
    {
        public ImagesValidation()
        {
            RuleFor(x => x.ImageName).NotEmpty().WithMessage("Resim adı bilgisi boş geçilemez!!!");
            RuleFor(x => x.AdvertId).NotEmpty().WithMessage("İlan bilgisi boş geçilemez!!!");

        }
    }
}
