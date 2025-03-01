using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class HizliSatisGrupTableValidator : AbstractValidator<HizliSatisGrupTable>
    {
        public HizliSatisGrupTableValidator()
        {
            // **GrupAdi** zorunlu ve 100 karakteri geçemez
            RuleFor(x => x.GrupAdi)
                .NotEmpty().WithMessage("Grup Adı gereklidir.")
                .MaximumLength(100).WithMessage("Grup Adı en fazla 100 karakter olabilir.");
        }
    }
}
