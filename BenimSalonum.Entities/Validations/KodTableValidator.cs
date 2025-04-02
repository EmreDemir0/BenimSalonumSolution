using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class KodTableValidator : AbstractValidator<KodTable>
    {
        public KodTableValidator()
        {
            // **Tablo** zorunlu ve 50 karakteri geçemez
            RuleFor(x => x.Tablo)
                .NotEmpty().WithMessage("Tablo ismi gereklidir.")
                .MaximumLength(50).WithMessage("Tablo ismi en fazla 50 karakter olabilir.");

            // **OnEki** zorunlu ve 10 karakteri geçemez
            RuleFor(x => x.OnEki)
                .NotEmpty().WithMessage("Ön Eki gereklidir.")
                .MaximumLength(10).WithMessage("Ön Eki en fazla 10 karakter olabilir.");

            // **SonDeger** zorunlu ve pozitif olmalı
            RuleFor(x => x.SonDeger)
                .GreaterThanOrEqualTo(0).WithMessage("Son Değer negatif olamaz.");
        }
    }
}
