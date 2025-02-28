using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Validations
{
    public class DepoTableValidator : AbstractValidator<DepoTable>
    {
        public DepoTableValidator()
        {
            // Örnek doğrulama kuralları
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.DepoAdi).NotEmpty().MaximumLength(100);
        }
    }
}
