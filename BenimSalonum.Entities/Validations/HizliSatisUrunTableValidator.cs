using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class HizliSatisUrunTableValidator : AbstractValidator<HizliSatisUrunTable>
    {
        public HizliSatisUrunTableValidator()
        {
            // **UrunAdi** zorunlu ve 100 karakteri geçemez
            RuleFor(x => x.UrunAdi)
                .NotEmpty().WithMessage("Ürün Adı gereklidir.")
                .MaximumLength(100).WithMessage("Ürün Adı en fazla 100 karakter olabilir.");

            // **GrupId** zorunlu ve geçerli bir ID olmalı
            RuleFor(x => x.GrupId)
                .GreaterThan(0).WithMessage("Geçerli bir Grup ID'si gereklidir.");

            // **Barkod** 50 karakteri geçemez, isteğe bağlı
            RuleFor(x => x.Barkod)
                .MaximumLength(50).WithMessage("Barkod en fazla 50 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Barkod)); // Barkod null veya boş olmamalıdır.
        }
    }
}
