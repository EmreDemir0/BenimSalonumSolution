using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Validations
{
    public class OdemeTuruTableValidator : AbstractValidator<OdemeTuruTable>
    {
        public OdemeTuruTableValidator()
        {
            // Örnek doğrulama kuralları
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.OdemeTuruAdi).NotEmpty().MaximumLength(50);
        }
    }
}
