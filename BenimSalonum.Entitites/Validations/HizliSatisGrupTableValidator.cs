using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Validations
{
    public class HizliSatisGrupTableValidator : AbstractValidator<HizliSatisGrupTable>
    {
        public HizliSatisGrupTableValidator()
        {
            // Örnek doğrulama kuralları
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.GrupAdi).NotEmpty().MaximumLength(100);
        }
    }
}
