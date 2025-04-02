using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Validations
{
    public class UserJwtTokenValidator : AbstractValidator<UserJwtToken>
    {
        public UserJwtTokenValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("Geçerli bir kullanıcı ID giriniz.");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
                .MaximumLength(100).WithMessage("Kullanıcı adı en fazla 100 karakter olmalıdır.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Rol boş olamaz.")
                .MaximumLength(50).WithMessage("Rol en fazla 50 karakter olmalıdır.");

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token boş olamaz.")
                .MaximumLength(500).WithMessage("Token en fazla 500 karakter olmalıdır.");

            RuleFor(x => x.Expiration)
                .GreaterThan(DateTime.UtcNow).WithMessage("Token süresi geçmiş olamaz.");
        }
    }
}
