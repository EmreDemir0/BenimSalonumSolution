using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class TanimlarTableValidator : AbstractValidator<TanimlarTable>
    {
        public TanimlarTableValidator()
        {
            // **Turu** zorunlu ve 50 karakteri geçemez
            RuleFor(x => x.Turu)
                .NotEmpty().WithMessage("Tanım türü gereklidir.")
                .MaximumLength(50).WithMessage("Tanım türü en fazla 50 karakter olabilir.");

            // **Tanimi** zorunlu ve 100 karakteri geçemez
            RuleFor(x => x.Tanimi)
                .NotEmpty().WithMessage("Tanım içeriği gereklidir.")
                .MaximumLength(100).WithMessage("Tanım içeriği en fazla 100 karakter olabilir.");

            // **Aciklama** 500 karakteri geçemez (isteğe bağlı)
            RuleFor(x => x.Aciklama)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Aciklama)); // Eğer Aciklama varsa, kontrol edilmelidir
        }
    }
}
