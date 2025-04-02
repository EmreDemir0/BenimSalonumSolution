using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class KasaTableValidator : AbstractValidator<KasaTable>
    {
        public KasaTableValidator()
        {
            // **KasaKodu** zorunlu ve 30 karakteri geçemez
            RuleFor(x => x.KasaKodu)
                .NotEmpty().WithMessage("Kasa Kodu gereklidir.")
                .MaximumLength(30).WithMessage("Kasa Kodu en fazla 30 karakter olabilir.");

            // **KasaAdi** zorunlu ve 100 karakteri geçemez
            RuleFor(x => x.KasaAdi)
                .NotEmpty().WithMessage("Kasa Adı gereklidir.")
                .MaximumLength(100).WithMessage("Kasa Adı en fazla 100 karakter olabilir.");

            // **YetkiliKodu** 50 karakteri geçemez, isteğe bağlı
            RuleFor(x => x.YetkiliKodu)
                .MaximumLength(50).WithMessage("Yetkili Kodu en fazla 50 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.YetkiliKodu)); // Eğer YetkiliKodu varsa, kontrol edilmelidir

            // **YetkiliAdi** 100 karakteri geçemez, isteğe bağlı
            RuleFor(x => x.YetkiliAdi)
                .MaximumLength(100).WithMessage("Yetkili Adı en fazla 100 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.YetkiliAdi)); // Eğer YetkiliAdi varsa, kontrol edilmelidir

            // **Aciklama** 500 karakteri geçemez, isteğe bağlı
            RuleFor(x => x.Aciklama)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Aciklama)); // Eğer Aciklama varsa, kontrol edilmelidir
        }
    }
}
