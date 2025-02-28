using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Validations
{
    public class TanimlarTableValidator : AbstractValidator<TanimlarTable>
    {
        public TanimlarTableValidator()
        {
            // Örnek doğrulama kuralları
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Tanimi).NotEmpty().MaximumLength(100);
        }
    }
}
