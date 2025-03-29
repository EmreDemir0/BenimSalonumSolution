using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class SiparisTableValidator : AbstractValidator<SiparisTable>
    {
        public SiparisTableValidator()
        {
            RuleFor(x => x.SiparisTuru)
                .NotEmpty().WithMessage("Sipariş Türü gereklidir.");

            RuleFor(x => x.SiparisNo)
                .NotEmpty().WithMessage("Sipariş No gereklidir.")
                .MaximumLength(20).WithMessage("Sipariş No en fazla 20 karakter olabilir.");

            RuleFor(x => x.SiparisTarihi)
                .NotNull().WithMessage("Sipariş Tarihi gereklidir.");

            RuleFor(x => x.CariId)
                .NotEmpty().WithMessage("Cari bilgisi gereklidir.");

            RuleFor(x => x.SubeId)
                .NotEmpty().WithMessage("Şube bilgisi gereklidir.");

            RuleFor(x => x.GenelToplam)
                .GreaterThanOrEqualTo(0).WithMessage("Genel Toplam negatif olamaz.");

            RuleFor(x => x.Aciklama)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");

            RuleFor(x => x.SiparisDurumu)
                .NotEmpty().WithMessage("Sipariş Durumu gereklidir.");

            RuleFor(x => x.EticaretSiparisNo)
                .MaximumLength(100).WithMessage("E-Ticaret Sipariş No en fazla 100 karakter olabilir.");

            RuleFor(x => x.KargoSirketi)
                .MaximumLength(50).WithMessage("Kargo Şirketi en fazla 50 karakter olabilir.");

            RuleFor(x => x.KargoTakipNo)
                .MaximumLength(50).WithMessage("Kargo Takip No en fazla 50 karakter olabilir.");

            RuleFor(x => x.CariUnvan)
                .MaximumLength(100).WithMessage("Cari Ünvan en fazla 100 karakter olabilir.");

            RuleFor(x => x.TeslimatAdresi)
                .MaximumLength(200).WithMessage("Teslimat Adresi en fazla 200 karakter olabilir.");

            RuleFor(x => x.TeslimatIl)
                .MaximumLength(30).WithMessage("Teslimat İl en fazla 30 karakter olabilir.");

            RuleFor(x => x.TeslimatIlce)
                .MaximumLength(30).WithMessage("Teslimat İlçe en fazla 30 karakter olabilir.");

            RuleFor(x => x.TelefonNo)
                .MaximumLength(20).WithMessage("Telefon No en fazla 20 karakter olabilir.");

            RuleFor(x => x.IptalNedeni)
                .MaximumLength(500).WithMessage("İptal Nedeni en fazla 500 karakter olabilir.");
        }
    }
}
