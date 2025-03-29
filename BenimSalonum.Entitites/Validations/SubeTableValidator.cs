using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class SubeTableValidator : AbstractValidator<SubeTable>
    {
        public SubeTableValidator()
        {
            RuleFor(x => x.SubeAdi)
                .NotEmpty().WithMessage("Şube Adı gereklidir.")
                .MaximumLength(100).WithMessage("Şube Adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.Adres)
                .NotEmpty().WithMessage("Adres gereklidir.")
                .MaximumLength(200).WithMessage("Adres en fazla 200 karakter olabilir.");

            RuleFor(x => x.Telefon)
                .MaximumLength(20).WithMessage("Telefon en fazla 20 karakter olabilir.");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Geçersiz e-posta adresi formatı.")
                .MaximumLength(100).WithMessage("E-posta adresi en fazla 100 karakter olabilir.");

            RuleFor(x => x.WebSite)
                .MaximumLength(100).WithMessage("Web sitesi adresi en fazla 100 karakter olabilir.");

            RuleFor(x => x.VergiDairesi)
                .NotEmpty().WithMessage("Vergi Dairesi gereklidir.")
                .MaximumLength(20).WithMessage("Vergi Dairesi en fazla 20 karakter olabilir.");

            RuleFor(x => x.VergiNo)
                .MaximumLength(20).WithMessage("Vergi No en fazla 20 karakter olabilir.");

            RuleFor(x => x.LisansKodu)
                .NotEmpty().WithMessage("Lisans Kodu gereklidir.")
                .MaximumLength(100).WithMessage("Lisans Kodu en fazla 100 karakter olabilir.");

            RuleFor(x => x.LisansBitisTarihi)
                .GreaterThanOrEqualTo(DateTime.Now)
                .When(x => x.LisansBitisTarihi.HasValue)
                .WithMessage("Lisans Bitiş Tarihi, şimdiki zamandan önce olamaz.");
                
            RuleFor(x => x.LogoUrl)
                .MaximumLength(500).WithMessage("Logo URL en fazla 500 karakter olabilir.");
                
            RuleFor(x => x.KullaniciLimiti)
                .GreaterThanOrEqualTo(1).WithMessage("Kullanıcı limiti en az 1 olmalıdır.");
        }
    }
}
