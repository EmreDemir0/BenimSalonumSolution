using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Validations
{
    public class PersonelTableValidator : AbstractValidator<PersonelTable>
    {
        public PersonelTableValidator()
        {
            // Örnek doğrulama kuralları
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.PersonelAdi).NotEmpty().MaximumLength(100);
        }
    }
}
