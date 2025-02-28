using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Validations
{
    public class StokHareketTableValidator : AbstractValidator<StokHareketTable>
    {
        public StokHareketTableValidator()
        {
            // Örnek doğrulama kuralları
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.FisKodu).NotEmpty().MaximumLength(20);
        }
    }
}
