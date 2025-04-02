using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class SistemLogTableValidator : AbstractValidator<SistemLogTable>
    {
        public SistemLogTableValidator()
        {
            RuleFor(x => x.Mesaj)
                .NotEmpty().WithMessage("Log mesajı boş olamaz.")
                .MaximumLength(2000).WithMessage("Log mesajı en fazla 2000 karakter olabilir.");

            RuleFor(x => x.HataSeviyesi)
                .InclusiveBetween(0, 4).WithMessage("Hata seviyesi 0 ile 4 arasında olmalıdır.");

            RuleFor(x => x.Modul)
                .MaximumLength(100).WithMessage("Modül adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.IstekYolu)
                .MaximumLength(500).WithMessage("İstek yolu en fazla 500 karakter olabilir.");

            RuleFor(x => x.KullaniciAdi)
                .MaximumLength(100).WithMessage("Kullanıcı adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.IpAdresi)
                .MaximumLength(50).WithMessage("IP adresi en fazla 50 karakter olabilir.");
                
            RuleFor(x => x.Gorünürlük)
                .InclusiveBetween(0, 2).WithMessage("Görünürlük değeri 0 ile 2 arasında olmalıdır.");
                
            RuleFor(x => x.Tarih)
                .NotEmpty().WithMessage("Tarih alanı boş olamaz.");
        }
    }
}
