using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class EFaturaKontorTableValidator : AbstractValidator<EFaturaKontorTable>
    {
        public EFaturaKontorTableValidator()
        {
            RuleFor(x => x.SubeId).NotEmpty().WithMessage("Şube bilgisi zorunludur.");
            RuleFor(x => x.ToplamKontor).NotEmpty().GreaterThan(0).WithMessage("Toplam kontör miktarı pozitif olmalıdır.");
            RuleFor(x => x.KalanKontor).NotEmpty().GreaterThanOrEqualTo(0).WithMessage("Kalan kontör miktarı negatif olamaz.");
            RuleFor(x => x.KullanilanKontor).NotEmpty().GreaterThanOrEqualTo(0).WithMessage("Kullanılan kontör miktarı negatif olamaz.");
            RuleFor(x => x.KontorTipi).NotEmpty().InclusiveBetween(1, 2).WithMessage("Kontör tipi 1 (Ana Hesap) veya 2 (Alt Hesap) olmalıdır.");
            
            // KontorTipi=2 (Alt Hesap) ise UstKontorId zorunlu
            RuleFor(x => x.UstKontorId).NotEmpty().When(x => x.KontorTipi == 2).WithMessage("Alt hesap için üst kontör kaydı zorunludur.");
            
            RuleFor(x => x.SatinAlmaTarihi).NotEmpty().WithMessage("Satın alma tarihi zorunludur.");
            RuleFor(x => x.Tutar).NotEmpty().GreaterThanOrEqualTo(0).WithMessage("Tutar negatif olamaz.");
            RuleFor(x => x.OlusturanKullaniciId).NotEmpty().WithMessage("Oluşturan kullanıcı bilgisi zorunludur.");
        }
    }
}
