using Estate.EntityLayer.Entities;
using FluentValidation;

namespace Estate.BusinessLayer.ValidationRules
{
    public class AdvertValidation:AbstractValidator<Advert>
    {
        public AdvertValidation()
        {
            RuleFor(x => x.Address).NotEmpty().WithMessage("Adres bilgisi boş geçilemez!!!");
            RuleFor(x => x.AdvertTitle).NotEmpty().WithMessage("İlan başlığı boş geçilemez!!!");
            RuleFor(x => x.AdvertTitle).MinimumLength(10).MaximumLength(500).WithMessage("En az 10, En çok 500 karakter girilmek zorunda!!!");
            RuleFor(x => x.Area).NotEmpty().WithMessage("Bu alan boş geçilemez!!!");
            RuleFor(x => x.BathroomNumbers).NotEmpty().WithMessage("Bu alan boş geçilemez!!!");
            RuleFor(x => x.NumberOfrooms).NotEmpty().WithMessage("Oda sayısı alanı boş geçilemez!!!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama alanı boş geçilemez!!!");
            RuleFor(x => x.Floor).NotEmpty().WithMessage("Kat alanı boş geçilemez!!!");
            RuleFor(x => x.Garage).NotEmpty().WithMessage("Garaj alanı boş geçilemez!!!");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Fiyat alanı boş geçilemez!!!");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon Numarası alanı boş geçilemez!!!");
            RuleFor(x => x.NeighbourhoodId).NotEmpty().WithMessage("Mahalle alanı boş geçilemez!!!");
            RuleFor(x => x.DistrictId).NotEmpty().WithMessage("Semt alanı boş geçilemez!!!");
            RuleFor(x => x.TypeId).NotEmpty().WithMessage("Tip alanı boş geçilemez!!!");
            RuleFor(x => x.CityId).NotEmpty().WithMessage("Şehir alanı boş geçilemez!!!");
            RuleFor(x => x.SituationId).NotEmpty().WithMessage("Durum alanı boş geçilemez!!!");
        }
    }
}
