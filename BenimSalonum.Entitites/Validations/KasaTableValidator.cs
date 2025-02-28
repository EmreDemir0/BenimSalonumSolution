using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Validations
{
    public class KasaTableValidator : AbstractValidator<KasaTable>
    {
        public KasaTableValidator()
        {
            // Örnek doğrulama kuralları
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.KasaAdi).NotEmpty().MaximumLength(100);
        }
    }
}
