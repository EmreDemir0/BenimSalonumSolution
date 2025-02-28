using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Validations
{
    public class KasaHareketTableValidator : AbstractValidator<KasaHareketTable>
    {
        public KasaHareketTableValidator()
        {
            // Örnek doğrulama kuralları
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.FisKodu).NotEmpty().MaximumLength(20);
        }
    }
}
