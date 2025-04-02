using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class FaturaDetayTableValidator : AbstractValidator<FaturaDetayTable>
    {
        public FaturaDetayTableValidator()
        {
            RuleFor(x => x.FaturaId)
                .NotEmpty().WithMessage("Fatura bilgisi gereklidir.");

            RuleFor(x => x.UrunAdi)
                .NotEmpty().WithMessage("Ürün Adı gereklidir.")
                .MaximumLength(150).WithMessage("Ürün Adı en fazla 150 karakter olabilir.");

            RuleFor(x => x.Birim)
                .NotEmpty().WithMessage("Birim gereklidir.")
                .MaximumLength(20).WithMessage("Birim en fazla 20 karakter olabilir.");

            RuleFor(x => x.Miktar)
                .GreaterThan(0).WithMessage("Miktar sıfırdan büyük olmalıdır.");

            RuleFor(x => x.BirimFiyat)
                .GreaterThanOrEqualTo(0).WithMessage("Birim Fiyat negatif olamaz.");

            RuleFor(x => x.KdvOrani)
                .InclusiveBetween(0, 100).WithMessage("KDV oranı %0 ile %100 arasında olmalıdır.");

            RuleFor(x => x.SiraNo)
                .GreaterThanOrEqualTo(0).WithMessage("Sıra No negatif olamaz.");

            RuleFor(x => x.AraToplam)
                .GreaterThanOrEqualTo(0).WithMessage("Ara Toplam negatif olamaz.");

            RuleFor(x => x.ToplamTutar)
                .GreaterThanOrEqualTo(0).WithMessage("Toplam Tutar negatif olamaz.");

            RuleFor(x => x.Aciklama)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");

            RuleFor(x => x.StokKodu)
                .MaximumLength(30).WithMessage("Stok Kodu en fazla 30 karakter olabilir.");

            RuleFor(x => x.Barkod)
                .MaximumLength(50).WithMessage("Barkod en fazla 50 karakter olabilir.");

            RuleFor(x => x.GTIPNo)
                .MaximumLength(30).WithMessage("GTIP No en fazla 30 karakter olabilir.");

            RuleFor(x => x.EfaturaKod)
                .MaximumLength(50).WithMessage("E-Fatura Kodu en fazla 50 karakter olabilir.");

            RuleFor(x => x.EfaturaTip)
                .MaximumLength(50).WithMessage("E-Fatura Tipi en fazla 50 karakter olabilir.");

            RuleFor(x => x.TevkifatKodu)
                .MaximumLength(20).WithMessage("Tevkifat Kodu en fazla 20 karakter olabilir.");

            RuleFor(x => x.TevkifatOrani)
                .InclusiveBetween(0, 100).WithMessage("Tevkifat Oranı %0 ile %100 arasında olmalıdır.")
                .When(x => x.TevkifatOrani.HasValue);
        }
    }
}
