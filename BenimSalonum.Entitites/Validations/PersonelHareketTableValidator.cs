using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Validations
{
    public class PersonelHareketTableValidator : AbstractValidator<PersonelHareketTable>
    {
        public PersonelHareketTableValidator()
        {
            // Örnek doğrulama kuralları
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.PersonelAdi).NotEmpty().MaximumLength(100);
        }
    }
}
