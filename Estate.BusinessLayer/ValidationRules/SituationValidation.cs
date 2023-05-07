using Estate.EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estate.BusinessLayer.ValidationRules
{
    public class SituationValidation : AbstractValidator<Situation>
    {
        public SituationValidation()
        {
            RuleFor(x => x.SituationName).NotEmpty().WithMessage("Durum bilgisi boş geçilemez!!!");
        }
    }
}
