using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Validations
{
    public class StokTableValidator : AbstractValidator<StokTable>
    {
        public StokTableValidator()
        {
            // Örnek doğrulama kuralları
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.StokAdi).NotEmpty().MaximumLength(100);
        }
    }
}
