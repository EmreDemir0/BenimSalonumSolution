using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Validations
{
    public class KullaniciRolTableValidator : AbstractValidator<KullaniciRolTable>
    {
        public KullaniciRolTableValidator()
        {
            // Örnek doğrulama kuralları
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.KullaniciAdi).NotEmpty().MaximumLength(50);
        }
    }
}
