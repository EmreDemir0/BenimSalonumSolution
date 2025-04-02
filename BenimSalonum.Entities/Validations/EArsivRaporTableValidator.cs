using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class EArsivRaporTableValidator : AbstractValidator<EArsivRaporTable>
    {
        public EArsivRaporTableValidator()
        {
            RuleFor(x => x.RaporNo).MaximumLength(50).WithMessage("Rapor numarası en fazla 50 karakter olabilir.");
            RuleFor(x => x.RaporTarihi).NotEmpty().WithMessage("Rapor tarihi zorunludur.");
            RuleFor(x => x.BaslangicTarihi).NotEmpty().WithMessage("Başlangıç tarihi zorunludur.");
            RuleFor(x => x.BitisTarihi).NotEmpty().WithMessage("Bitiş tarihi zorunludur.");
            
            RuleFor(x => x.BitisTarihi)
                .GreaterThanOrEqualTo(x => x.BaslangicTarihi)
                .WithMessage("Bitiş tarihi başlangıç tarihinden önce olamaz.");
                
            RuleFor(x => x.Durum).NotEmpty().WithMessage("Durum bilgisi zorunludur.");
            RuleFor(x => x.HataMesaji).MaximumLength(500).WithMessage("Hata mesajı en fazla 500 karakter olabilir.");
            RuleFor(x => x.RaporUrl).MaximumLength(500).WithMessage("Rapor URL en fazla 500 karakter olabilir.");
            RuleFor(x => x.UUID).MaximumLength(36).WithMessage("UUID en fazla 36 karakter olabilir.");
            RuleFor(x => x.SubeId).NotEmpty().WithMessage("Şube bilgisi zorunludur.");
            RuleFor(x => x.KullaniciId).NotEmpty().WithMessage("Kullanıcı bilgisi zorunludur.");
            RuleFor(x => x.Aciklama).MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");
            RuleFor(x => x.OlusturanKullaniciId).NotEmpty().WithMessage("Oluşturan kullanıcı bilgisi zorunludur.");
        }
    }
}
