using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Validations
{
    public class CariTableValidator : AbstractValidator<CariTable>
    {
        public CariTableValidator()
        {
            // Örnek doğrulama kuralları
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.CariAdi).NotEmpty().MaximumLength(100);
        }
    }
}
