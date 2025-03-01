using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class StokTableValidator : AbstractValidator<StokTable>
    {
        public StokTableValidator()
        {
            // **StokKodu** zorunlu ve 30 karakteri geçemez
            RuleFor(x => x.StokKodu)
                .NotEmpty().WithMessage("Stok Kodu gereklidir.")
                .MaximumLength(30).WithMessage("Stok Kodu en fazla 30 karakter olabilir.");

            // **StokAdi** zorunlu ve 100 karakteri geçemez
            RuleFor(x => x.StokAdi)
                .NotEmpty().WithMessage("Stok Adı gereklidir.")
                .MaximumLength(100).WithMessage("Stok Adı en fazla 100 karakter olabilir.");

            // **Barkod** 50 karakteri geçemez (isteğe bağlı)
            RuleFor(x => x.Barkod)
                .MaximumLength(50).WithMessage("Barkod en fazla 50 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Barkod)); // Eğer Barkod varsa, kontrol edilmelidir

            // **BarkodTuru** 20 karakteri geçemez (isteğe bağlı)
            RuleFor(x => x.BarkodTuru)
                .MaximumLength(20).WithMessage("Barkod Türü en fazla 20 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.BarkodTuru)); // Eğer BarkodTuru varsa, kontrol edilmelidir

            // **Birimi** zorunlu ve 20 karakteri geçemez
            RuleFor(x => x.Birimi)
                .NotEmpty().WithMessage("Birim gereklidir.")
                .MaximumLength(20).WithMessage("Birim en fazla 20 karakter olabilir.");

            // **AlisKdv ve SatisKdv** zorunlu ve %0 ile %100 arasında olmalı
            RuleFor(x => x.AlisKdv)
                .InclusiveBetween(0, 100).WithMessage("Alış KDV oranı %0 ile %100 arasında olmalıdır.");

            RuleFor(x => x.SatisKdv)
                .InclusiveBetween(0, 100).WithMessage("Satış KDV oranı %0 ile %100 arasında olmalıdır.");

            // **AlisFiyati** ve **SatisFiyati** pozitif olmalı (isteğe bağlı)
            RuleFor(x => x.AlisFiyati1)
                .GreaterThanOrEqualTo(0).WithMessage("Alış Fiyatı negatif olamaz.")
                .When(x => x.AlisFiyati1.HasValue);

            RuleFor(x => x.SatisFiyati1)
                .GreaterThanOrEqualTo(0).WithMessage("Satış Fiyatı negatif olamaz.")
                .When(x => x.SatisFiyati1.HasValue);

            // **MinStokMiktari** ve **MaxStokMiktari** pozitif olmalı (isteğe bağlı)
            RuleFor(x => x.MinStokMiktari)
                .GreaterThanOrEqualTo(0).WithMessage("Min Stok Miktarı negatif olamaz.")
                .When(x => x.MinStokMiktari.HasValue);

            RuleFor(x => x.MaxStokMiktari)
                .GreaterThanOrEqualTo(0).WithMessage("Max Stok Miktarı negatif olamaz.")
                .When(x => x.MaxStokMiktari.HasValue);

            // **Aciklama** 500 karakteri geçemez (isteğe bağlı)
            RuleFor(x => x.Aciklama)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Aciklama)); // Eğer Aciklama varsa, kontrol edilmelidir
        }
    }
}
