using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class FaturaTableValidator : AbstractValidator<FaturaTable>
    {
        public FaturaTableValidator()
        {
            RuleFor(x => x.FaturaTuru)
                .NotEmpty().WithMessage("Fatura Türü gereklidir.");

            RuleFor(x => x.FaturaNo)
                .NotEmpty().WithMessage("Fatura No gereklidir.")
                .MaximumLength(16).WithMessage("Fatura No en fazla 16 karakter olabilir.");

            RuleFor(x => x.BelgeNo)
                .NotEmpty().WithMessage("Belge No gereklidir.")
                .MaximumLength(50).WithMessage("Belge No en fazla 50 karakter olabilir.");

            RuleFor(x => x.FaturaTarihi)
                .NotNull().WithMessage("Fatura Tarihi gereklidir.");

            RuleFor(x => x.CariId)
                .NotEmpty().WithMessage("Cari bilgisi gereklidir.");

            RuleFor(x => x.SubeId)
                .NotEmpty().WithMessage("Şube bilgisi gereklidir.");

            RuleFor(x => x.GenelToplam)
                .GreaterThanOrEqualTo(0).WithMessage("Genel Toplam negatif olamaz.");

            RuleFor(x => x.Aciklama)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");

            RuleFor(x => x.FaturaDurumu)
                .NotEmpty().WithMessage("Fatura Durumu gereklidir.");

            RuleFor(x => x.OdemeDurumu)
                .NotEmpty().WithMessage("Ödeme Durumu gereklidir.");

            RuleFor(x => x.EfaturaNo)
                .MaximumLength(50).WithMessage("E-Fatura No en fazla 50 karakter olabilir.");

            RuleFor(x => x.EarsivNo)
                .MaximumLength(50).WithMessage("E-Arşiv No en fazla 50 karakter olabilir.");

            RuleFor(x => x.EirsaliyeNo)
                .MaximumLength(50).WithMessage("E-İrsaliye No en fazla 50 karakter olabilir.");

            RuleFor(x => x.EfaturaGuid)
                .MaximumLength(200).WithMessage("E-Fatura GUID en fazla 200 karakter olabilir.");

            RuleFor(x => x.EfaturaDurum)
                .MaximumLength(100).WithMessage("E-Fatura Durum en fazla 100 karakter olabilir.");

            RuleFor(x => x.EfaturaHata)
                .MaximumLength(500).WithMessage("E-Fatura Hata en fazla 500 karakter olabilir.");

            RuleFor(x => x.EfaturaPdf)
                .MaximumLength(500).WithMessage("E-Fatura PDF en fazla 500 karakter olabilir.");

            RuleFor(x => x.EfaturaXml)
                .MaximumLength(500).WithMessage("E-Fatura XML en fazla 500 karakter olabilir.");

            RuleFor(x => x.CariUnvan)
                .MaximumLength(100).WithMessage("Cari Ünvan en fazla 100 karakter olabilir.");

            RuleFor(x => x.CariTckn)
                .MaximumLength(11).WithMessage("TCKN en fazla 11 karakter olabilir.");

            RuleFor(x => x.CariVkn)
                .MaximumLength(11).WithMessage("VKN en fazla 11 karakter olabilir.");

            RuleFor(x => x.CariVergiDairesi)
                .MaximumLength(30).WithMessage("Vergi Dairesi en fazla 30 karakter olabilir.");

            RuleFor(x => x.CariAdres)
                .MaximumLength(200).WithMessage("Adres en fazla 200 karakter olabilir.");

            RuleFor(x => x.CariIl)
                .MaximumLength(30).WithMessage("İl en fazla 30 karakter olabilir.");

            RuleFor(x => x.CariIlce)
                .MaximumLength(30).WithMessage("İlçe en fazla 30 karakter olabilir.");

            RuleFor(x => x.IptalNedeni)
                .MaximumLength(500).WithMessage("İptal Nedeni en fazla 500 karakter olabilir.");

            RuleFor(x => x.OdemeSekli)
                .MaximumLength(50).WithMessage("Ödeme Şekli en fazla 50 karakter olabilir.");
        }
    }
}
