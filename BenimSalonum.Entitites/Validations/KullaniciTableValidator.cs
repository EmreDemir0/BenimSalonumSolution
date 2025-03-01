using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class KullaniciTableValidator : AbstractValidator<KullaniciTable>
    {
        public KullaniciTableValidator()
        {
            // **KullaniciAdi** zorunlu ve 50 karakteri geçemez
            RuleFor(x => x.KullaniciAdi)
                .NotEmpty().WithMessage("Kullanıcı Adı gereklidir.")
                .MaximumLength(50).WithMessage("Kullanıcı Adı en fazla 50 karakter olabilir.");

            // **Adi** zorunlu ve 50 karakteri geçemez
            RuleFor(x => x.Adi)
                .NotEmpty().WithMessage("Adı gereklidir.")
                .MaximumLength(50).WithMessage("Adı en fazla 50 karakter olabilir.");

            // **Soyadi** zorunlu ve 50 karakteri geçemez
            RuleFor(x => x.Soyadi)
                .NotEmpty().WithMessage("Soyadı gereklidir.")
                .MaximumLength(50).WithMessage("Soyadı en fazla 50 karakter olabilir.");

            // **Gorevi** 50 karakteri geçemez, isteğe bağlı
            RuleFor(x => x.Gorevi)
                .MaximumLength(50).WithMessage("Görev en fazla 50 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Gorevi)); // Eğer Gorevi varsa, kontrol edilmelidir

            // **Parola** zorunlu ve 100 karakteri geçemez
            RuleFor(x => x.Parola)
                .NotEmpty().WithMessage("Parola gereklidir.")
                .MaximumLength(100).WithMessage("Parola en fazla 100 karakter olabilir.");

            // **HatirlatmaSorusu** 100 karakteri geçemez, isteğe bağlı
            RuleFor(x => x.HatirlatmaSorusu)
                .MaximumLength(100).WithMessage("Hatırlatma Sorusu en fazla 100 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.HatirlatmaSorusu)); // Eğer HatirlatmaSorusu varsa, kontrol edilmelidir

            // **HatirlatmaCevap** 100 karakteri geçemez, isteğe bağlı
            RuleFor(x => x.HatirlatmaCevap)
                .MaximumLength(100).WithMessage("Hatırlatma Cevap en fazla 100 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.HatirlatmaCevap)); // Eğer HatirlatmaCevap varsa, kontrol edilmelidir

            // **KayitTarihi** zorunlu ve geçerli bir tarih olmalı
            RuleFor(x => x.KayitTarihi)
                .NotEmpty().WithMessage("Kayıt Tarihi gereklidir.");

            // **SonGirisTarihi** nullable ve geçerli bir tarih olmalı
            RuleFor(x => x.SonGirisTarihi)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Son giriş tarihi şu anki tarihten büyük olamaz.")
                .When(x => x.SonGirisTarihi.HasValue); // Eğer SonGirisTarihi varsa, kontrol edilmelidir
        }
    }
}
