using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Validations
{
    public class HizliSatisUrunTableValidator : AbstractValidator<HizliSatisUrunTable>
    {
        public HizliSatisUrunTableValidator()
        {
            // Örnek doğrulama kuralları
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.UrunAdi).NotEmpty().MaximumLength(100);
        }
    }
}
