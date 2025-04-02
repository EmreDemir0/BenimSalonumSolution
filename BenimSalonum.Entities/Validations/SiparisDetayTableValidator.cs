using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class SiparisDetayTableValidator : AbstractValidator<SiparisDetayTable>
    {
        public SiparisDetayTableValidator()
        {
            RuleFor(x => x.SiparisId)
                .NotEmpty().WithMessage("Sipariş ID gereklidir.");

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
                .GreaterThanOrEqualTo(0).WithMessage("KDV Oranı negatif olamaz.");

            RuleFor(x => x.AraToplam)
                .GreaterThanOrEqualTo(0).WithMessage("Ara Toplam negatif olamaz.");

            RuleFor(x => x.ToplamTutar)
                .GreaterThanOrEqualTo(0).WithMessage("Toplam Tutar negatif olamaz.");

            RuleFor(x => x.StokKodu)
                .MaximumLength(30).WithMessage("Stok Kodu en fazla 30 karakter olabilir.");

            RuleFor(x => x.Barkod)
                .MaximumLength(50).WithMessage("Barkod en fazla 50 karakter olabilir.");

            RuleFor(x => x.Aciklama)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");

            RuleFor(x => x.EticaretUrunKodu)
                .MaximumLength(100).WithMessage("E-Ticaret Ürün Kodu en fazla 100 karakter olabilir.");

            RuleFor(x => x.EticaretSepetItemId)
                .MaximumLength(36).WithMessage("E-Ticaret Sepet Item ID en fazla 36 karakter olabilir.");
        }
    }
}
