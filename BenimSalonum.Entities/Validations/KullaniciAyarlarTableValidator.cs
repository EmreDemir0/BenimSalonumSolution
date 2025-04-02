using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class KullaniciAyarlarTableValidator : AbstractValidator<KullaniciAyarlarTable>
    {
        public KullaniciAyarlarTableValidator()
        {
            RuleFor(x => x.KullaniciId).NotEmpty().WithMessage("Kullanıcı ID boş olamaz");
            
            RuleFor(x => x.RandevuHatirlatmaZamani).InclusiveBetween(15, 1440)
                .WithMessage("Randevu hatırlatma zamanı 15 dakika ile 24 saat (1440 dakika) arasında olmalıdır");
            
            RuleFor(x => x.Dil).NotEmpty().MaximumLength(30)
                .WithMessage("Dil tercihi boş olamaz ve en fazla 30 karakter olabilir");
            
            RuleFor(x => x.Tema).NotEmpty().MaximumLength(30)
                .WithMessage("Tema tercihi boş olamaz ve en fazla 30 karakter olabilir");
            
            RuleFor(x => x.CalismaBaslangicSaati).NotEmpty().MaximumLength(10)
                .WithMessage("Çalışma başlangıç saati boş olamaz ve en fazla 10 karakter olabilir");
            
            RuleFor(x => x.CalismaBitisSaati).NotEmpty().MaximumLength(10)
                .WithMessage("Çalışma bitiş saati boş olamaz ve en fazla 10 karakter olabilir");
            
            RuleFor(x => x.OturumSuresi).InclusiveBetween(15, 1440)
                .WithMessage("Oturum süresi 15 dakika ile 24 saat (1440 dakika) arasında olmalıdır");
            
            RuleFor(x => x.OtomatikKilitlemeSuresi).InclusiveBetween(1, 120)
                .WithMessage("Otomatik kilitleme süresi 1 dakika ile 2 saat (120 dakika) arasında olmalıdır");
        }
    }
}
