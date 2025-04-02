using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class DepoTableValidator : AbstractValidator<DepoTable>
    {
        public DepoTableValidator()
        {
            // **DepoKodu** zorunlu ve 20 karakteri geçemez
            RuleFor(x => x.DepoKodu)
                .NotEmpty().WithMessage("Depo Kodu gereklidir.")
                .MaximumLength(20).WithMessage("Depo Kodu en fazla 20 karakter olabilir.");

            // **DepoAdi** zorunlu ve 100 karakteri geçemez
            RuleFor(x => x.DepoAdi)
                .NotEmpty().WithMessage("Depo Adı gereklidir.")
                .MaximumLength(100).WithMessage("Depo Adı en fazla 100 karakter olabilir.");

            // **Adres** zorunlu ve 250 karakteri geçemez
            RuleFor(x => x.Adres)
                .NotEmpty().WithMessage("Adres gereklidir.")
                .MaximumLength(250).WithMessage("Adres en fazla 250 karakter olabilir.");

            // **Il** 50 karakteri geçemez
            RuleFor(x => x.Il)
                .MaximumLength(50).WithMessage("İl en fazla 50 karakter olabilir.");

            // **Telefon** 15 karakteri geçemez
            RuleFor(x => x.Telefon)
                .MaximumLength(15).WithMessage("Telefon numarası en fazla 15 karakter olabilir.");

            // **Aciklama** 500 karakteri geçemez
            RuleFor(x => x.Aciklama)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");

            // **YetkiliKodu** 50 karakteri geçemez
            RuleFor(x => x.YetkiliKodu)
                .MaximumLength(50).WithMessage("Yetkili Kodu en fazla 50 karakter olabilir.");

            // **YetkiliAdi** 100 karakteri geçemez
            RuleFor(x => x.YetkiliAdi)
                .MaximumLength(100).WithMessage("Yetkili Adı en fazla 100 karakter olabilir.");

            // **Ilce** 50 karakteri geçemez
            RuleFor(x => x.Ilce)
                .MaximumLength(50).WithMessage("İlçe en fazla 50 karakter olabilir.");

            // **Semt** 50 karakteri geçemez
            RuleFor(x => x.Semt)
                .MaximumLength(50).WithMessage("Semt en fazla 50 karakter olabilir.");
        }
    }
}
