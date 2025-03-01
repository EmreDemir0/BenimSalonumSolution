using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class PersonelHareketTableValidator : AbstractValidator<PersonelHareketTable>
    {
        public PersonelHareketTableValidator()
        {
            // **FisKodu** zorunlu ve 20 karakteri geçemez
            RuleFor(x => x.FisKodu)
                .NotEmpty().WithMessage("Fiş Kodu gereklidir.")
                .MaximumLength(20).WithMessage("Fiş Kodu en fazla 20 karakter olabilir.");

            // **Unvani** zorunlu ve 50 karakteri geçemez
            RuleFor(x => x.Unvani)
                .NotEmpty().WithMessage("Unvan gereklidir.")
                .MaximumLength(50).WithMessage("Unvan en fazla 50 karakter olabilir.");

            // **PersonelKodu** zorunlu ve 20 karakteri geçemez
            RuleFor(x => x.PersonelKodu)
                .NotEmpty().WithMessage("Personel Kodu gereklidir.")
                .MaximumLength(20).WithMessage("Personel Kodu en fazla 20 karakter olabilir.");

            // **PersonelAdi** zorunlu ve 100 karakteri geçemez
            RuleFor(x => x.PersonelAdi)
                .NotEmpty().WithMessage("Personel Adı gereklidir.")
                .MaximumLength(100).WithMessage("Personel Adı en fazla 100 karakter olabilir.");

            // **TcKimlikNo** 11 karakteri geçemez
            RuleFor(x => x.TcKimlikNo)
                .MaximumLength(11).WithMessage("TC Kimlik Numarası 11 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.TcKimlikNo)); // Eğer TC Kimlik Numarası varsa, kontrol edilmelidir

            // **Donemi** zorunlu ve geçerli bir tarih olmalı
            RuleFor(x => x.Donemi)
                .NotEmpty().WithMessage("Dönem bilgisi gereklidir.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Dönem, şu anki tarihten büyük olamaz.");

            // **PrimOrani** zorunlu ve 0 ile 100 arasında olmalı
            RuleFor(x => x.PrimOrani)
                .InclusiveBetween(0, 100).WithMessage("Prim Oranı %0 ile %100 arasında olmalıdır.");

            // **ToplamSatis** zorunlu ve pozitif olmalı
            RuleFor(x => x.ToplamSatis)
                .GreaterThanOrEqualTo(0).WithMessage("Toplam Satış negatif olamaz.");

            // **AylikMaas** zorunlu ve pozitif olmalı
            RuleFor(x => x.AylikMaas)
                .GreaterThanOrEqualTo(0).WithMessage("Aylık Maaş negatif olamaz.");

            // **Aciklama** 500 karakteri geçemez, isteğe bağlı
            RuleFor(x => x.Aciklama)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Aciklama)); // Eğer Aciklama varsa, kontrol edilmelidir
        }
    }
}
