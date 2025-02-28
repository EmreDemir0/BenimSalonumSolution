using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Validations
{
    public class KullaniciLogTableValidator : AbstractValidator<KullaniciLogTable>
    {
        public KullaniciLogTableValidator()
        {
            // Örnek doğrulama kuralları
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.KullaniciAdi).NotEmpty().MaximumLength(50);
        }
    }
}
