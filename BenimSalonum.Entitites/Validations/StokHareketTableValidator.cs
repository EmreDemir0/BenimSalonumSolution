using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class StokHareketTableValidator : AbstractValidator<StokHareketTable>
    {
        public StokHareketTableValidator()
        {
            // **FisKodu** zorunlu ve 20 karakteri geçemez
            RuleFor(x => x.FisKodu)
                .NotEmpty().WithMessage("Fiş Kodu gereklidir.")
                .MaximumLength(20).WithMessage("Fiş Kodu en fazla 20 karakter olabilir.");

            // **Hareket** zorunlu ve 50 karakteri geçemez
            RuleFor(x => x.Hareket)
                .NotEmpty().WithMessage("Hareket gereklidir.")
                .MaximumLength(50).WithMessage("Hareket en fazla 50 karakter olabilir.");

            // **StokId** zorunlu ve geçerli bir ID olmalı
            RuleFor(x => x.StokId)
                .GreaterThan(0).WithMessage("Geçerli bir Stok ID'si gereklidir.");

            // **Miktar** pozitif olmalı (isteğe bağlı)
            RuleFor(x => x.Miktar)
                .GreaterThanOrEqualTo(0).WithMessage("Miktar negatif olamaz.")
                .When(x => x.Miktar.HasValue); // Miktar varsa, kontrol edilmelidir

            // **Kdv** zorunlu ve geçerli bir değer olmalı
            RuleFor(x => x.Kdv)
                .InclusiveBetween(0, 100).WithMessage("KDV oranı %0 ile %100 arasında olmalıdır.");

            // **BirimFiyati** 2 ondalıklı decimal olmalı (isteğe bağlı)
            RuleFor(x => x.BirimFiyati)
                .GreaterThanOrEqualTo(0).WithMessage("Birim Fiyatı negatif olamaz.")
                .When(x => x.BirimFiyati.HasValue);

            // **IndirimOrani** 2 ondalıklı decimal olmalı (isteğe bağlı)
            RuleFor(x => x.IndirimOrani)
                .InclusiveBetween(0, 100).WithMessage("İndirim Oranı %0 ile %100 arasında olmalıdır.")
                .When(x => x.IndirimOrani.HasValue);

            // **DepoId** zorunlu ve geçerli bir ID olmalı
            RuleFor(x => x.DepoId)
                .GreaterThan(0).WithMessage("Geçerli bir Depo ID'si gereklidir.");

            // **SeriNo** 50 karakteri geçemez (isteğe bağlı)
            RuleFor(x => x.SeriNo)
                .MaximumLength(50).WithMessage("Seri No en fazla 50 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.SeriNo));

            // **Tarih** geçerli bir tarih olmalı (isteğe bağlı)
            RuleFor(x => x.Tarih)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Tarih, şu anki tarihten büyük olamaz.")
                .When(x => x.Tarih.HasValue);

            // **Aciklama** 500 karakteri geçemez (isteğe bağlı)
            RuleFor(x => x.Aciklama)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Aciklama));

            // **Siparis** sadece true ya da false olmalı (isteğe bağlı)
            RuleFor(x => x.Siparis)
                .Must(x => x == true || x == false).WithMessage("Sipariş yalnızca true veya false olabilir.");
        }
    }
}
