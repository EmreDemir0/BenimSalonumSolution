using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class OdemeTuruTableValidator : AbstractValidator<OdemeTuruTable>
    {
        public OdemeTuruTableValidator()
        {
            // **OdemeTuruKodu** zorunlu ve 20 karakteri geçemez
            RuleFor(x => x.OdemeTuruKodu)
                .NotEmpty().WithMessage("Ödeme Türü Kodu gereklidir.")
                .MaximumLength(20).WithMessage("Ödeme Türü Kodu en fazla 20 karakter olabilir.");

            // **OdemeTuruAdi** zorunlu ve 50 karakteri geçemez
            RuleFor(x => x.OdemeTuruAdi)
                .NotEmpty().WithMessage("Ödeme Türü Adı gereklidir.")
                .MaximumLength(50).WithMessage("Ödeme Türü Adı en fazla 50 karakter olabilir.");

            // **Aciklama** 500 karakteri geçemez, isteğe bağlı
            RuleFor(x => x.Aciklama)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Aciklama)); // Eğer Aciklama varsa, kontrol edilmelidir
        }
    }
}
