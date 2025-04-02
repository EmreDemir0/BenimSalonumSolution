using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class PersonelTableValidator : AbstractValidator<PersonelTable>
    {
        public PersonelTableValidator()
        {
            // **PersonelUnvani** zorunlu ve 50 karakteri geçemez
            RuleFor(x => x.PersonelUnvani)
                .NotEmpty().WithMessage("Personel Unvanı gereklidir.")
                .MaximumLength(50).WithMessage("Personel Unvanı en fazla 50 karakter olabilir.");

            // **PersonelKodu** zorunlu ve 20 karakteri geçemez
            RuleFor(x => x.PersonelKodu)
                .NotEmpty().WithMessage("Personel Kodu gereklidir.")
                .MaximumLength(20).WithMessage("Personel Kodu en fazla 20 karakter olabilir.");

            // **PersonelAdi** zorunlu ve 100 karakteri geçemez
            RuleFor(x => x.PersonelAdi)
                .NotEmpty().WithMessage("Personel Adı gereklidir.")
                .MaximumLength(100).WithMessage("Personel Adı en fazla 100 karakter olabilir.");

            // **PersonelTc** zorunlu ve 11 karakteri geçemez
            RuleFor(x => x.PersonelTc)
                .NotEmpty().WithMessage("Personel TC Kimlik Numarası gereklidir.")
                .MaximumLength(11).WithMessage("Personel TC Kimlik Numarası 11 karakter olmalıdır.");

            // **PersonelGiris** geçerli bir tarih olmalı
            RuleFor(x => x.PersonelGiris)
                .NotEmpty().WithMessage("Personel Giriş Tarihi gereklidir.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Personel Giriş Tarihi şu anki tarihten büyük olamaz.");

            // **PersonelCikis** geçerli bir tarih olmalı (isteğe bağlı)
            RuleFor(x => x.PersonelCikis)
                .GreaterThan(x => x.PersonelGiris).WithMessage("Personel Çıkış Tarihi, Personel Giriş Tarihi'nden önce olamaz.")
                .When(x => x.PersonelCikis.HasValue); // PersonelCikis varsa kontrol edilmelidir

            // **CepTelefonu** zorunlu ve 15 karakteri geçemez
            RuleFor(x => x.CepTelefonu)
                .NotEmpty().WithMessage("Cep Telefonu gereklidir.")
                .MaximumLength(15).WithMessage("Cep Telefonu en fazla 15 karakter olabilir.");

            // **Telefon** 15 karakteri geçemez (isteğe bağlı)
            RuleFor(x => x.Telefon)
                .MaximumLength(15).WithMessage("Telefon en fazla 15 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Telefon));

            // **Fax** 15 karakteri geçemez (isteğe bağlı)
            RuleFor(x => x.Fax)
                .MaximumLength(15).WithMessage("Fax en fazla 15 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Fax));

            // **EMail** geçerli bir e-posta formatı ve 100 karakteri geçemez (isteğe bağlı)
            RuleFor(x => x.EMail)
                .EmailAddress().WithMessage("Geçersiz e-posta adresi.")
                .MaximumLength(100).WithMessage("E-posta adresi en fazla 100 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.EMail));

            // **Web** geçerli bir URL formatı ve 150 karakteri geçemez (isteğe bağlı)
            RuleFor(x => x.Web)
                .Matches(@"^(https?://)?([a-z0-9-]+\.)+[a-z0-9]{2,4}(/.*)?$").WithMessage("Geçersiz URL formatı.")
                .MaximumLength(150).WithMessage("Web adresi en fazla 150 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Web));

            // **Il** 50 karakteri geçemez (isteğe bağlı)
            RuleFor(x => x.Il)
                .MaximumLength(50).WithMessage("İl en fazla 50 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Il));

            // **Ilce** 50 karakteri geçemez (isteğe bağlı)
            RuleFor(x => x.Ilce)
                .MaximumLength(50).WithMessage("İlçe en fazla 50 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Ilce));

            // **Semt** 50 karakteri geçemez (isteğe bağlı)
            RuleFor(x => x.Semt)
                .MaximumLength(50).WithMessage("Semt en fazla 50 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Semt));

            // **Adres** zorunlu ve 250 karakteri geçemez
            RuleFor(x => x.Adres)
                .NotEmpty().WithMessage("Adres gereklidir.")
                .MaximumLength(250).WithMessage("Adres en fazla 250 karakter olabilir.");

            // **AylikMaas** pozitif olmalı (isteğe bağlı)
            RuleFor(x => x.AylikMaas)
                .GreaterThanOrEqualTo(0).WithMessage("Aylık Maaş negatif olamaz.")
                .When(x => x.AylikMaas.HasValue);

            // **PrimOrani** 0 ile 100 arasında olmalı (isteğe bağlı)
            RuleFor(x => x.PrimOrani)
                .InclusiveBetween(0, 100).WithMessage("Prim Oranı %0 ile %100 arasında olmalıdır.")
                .When(x => x.PrimOrani.HasValue);

            // **Aciklama** 500 karakteri geçemez (isteğe bağlı)
            RuleFor(x => x.Aciklama)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Aciklama));
        }
    }
}
