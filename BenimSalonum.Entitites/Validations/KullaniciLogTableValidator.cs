using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class KullaniciLogTableValidator : AbstractValidator<KullaniciLogTable>
    {
        public KullaniciLogTableValidator()
        {
            // **KullaniciAdi** zorunlu ve 50 karakteri geçemez
            RuleFor(x => x.KullaniciAdi)
                .NotEmpty().WithMessage("Kullanıcı Adı gereklidir.")
                .MaximumLength(50).WithMessage("Kullanıcı Adı en fazla 50 karakter olabilir.");

            // **YapilanIslem** zorunlu ve 200 karakteri geçemez
            RuleFor(x => x.YapilanIslem)
                .NotEmpty().WithMessage("Yapılan işlem gereklidir.")
                .MaximumLength(200).WithMessage("Yapılan işlem en fazla 200 karakter olabilir.");

            // **SonGirisTarihi** nullable ve geçerli bir tarih olmalı
            RuleFor(x => x.SonGirisTarihi)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Son giriş tarihi şu anki tarihten büyük olamaz.")
                .When(x => x.SonGirisTarihi.HasValue); // Eğer SonGirisTarihi varsa, kontrol edilmelidir

            // **YapilanIslemTarihi** zorunlu ve geçerli bir tarih olmalı
            RuleFor(x => x.YapilanIslemTarihi)
                .NotEmpty().WithMessage("Yapılan işlem tarihi gereklidir.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Yapılan işlem tarihi şu anki tarihten büyük olamaz.");
        }
    }
}
