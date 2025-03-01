using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class KullaniciRolTableValidator : AbstractValidator<KullaniciRolTable>
    {
        public KullaniciRolTableValidator()
        {
            // **RootId** zorunlu ve pozitif olmalı
            RuleFor(x => x.RootId)
                .GreaterThan(0).WithMessage("Root Id pozitif bir değer olmalıdır.");

            // **ParentId** zorunlu ve pozitif olmalı
            RuleFor(x => x.ParentId)
                .GreaterThan(0).WithMessage("Parent Id pozitif bir değer olmalıdır.");

            // **KullaniciAdi** zorunlu ve 50 karakteri geçemez
            RuleFor(x => x.KullaniciAdi)
                .NotEmpty().WithMessage("Kullanıcı Adı gereklidir.")
                .MaximumLength(50).WithMessage("Kullanıcı Adı en fazla 50 karakter olabilir.");

            // **FormAdi** zorunlu ve 100 karakteri geçemez
            RuleFor(x => x.FormAdi)
                .NotEmpty().WithMessage("Form Adı gereklidir.")
                .MaximumLength(100).WithMessage("Form Adı en fazla 100 karakter olabilir.");

            // **KontrolAdi** zorunlu ve 100 karakteri geçemez
            RuleFor(x => x.KontrolAdi)
                .NotEmpty().WithMessage("Kontrol Adı gereklidir.")
                .MaximumLength(100).WithMessage("Kontrol Adı en fazla 100 karakter olabilir.");

            // **Yetki** varsayılan olarak false, ancak doğrulama yapılabilir
            RuleFor(x => x.Yetki)
                .Must(x => x == true || x == false).WithMessage("Yetki yalnızca doğru veya yanlış olabilir.");
        }
    }
}
