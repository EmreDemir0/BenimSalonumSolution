using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Validations
{
    public class KodTableValidator : AbstractValidator<KodTable>
    {
        public KodTableValidator()
        {
            // Örnek doğrulama kuralları
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Tablo).NotEmpty().MaximumLength(50);
        }
    }
}
