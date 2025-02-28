using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Validations
{
    public class FisTableValidator : AbstractValidator<FisTable>
    {
        public FisTableValidator()
        {
            // Örnek doğrulama kuralları
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.FisKodu).NotEmpty().MaximumLength(20);
        }
    }
}
