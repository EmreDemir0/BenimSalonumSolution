using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class GibMukellefTableValidator : AbstractValidator<GibMukellefTable>
    {
        public GibMukellefTableValidator()
        {
            RuleFor(x => x.VKN_TCKN)
                .NotEmpty().WithMessage("VKN/TCKN zorunludur.")
                .Length(10, 11).WithMessage("VKN/TCKN 10 veya 11 karakter olmalıdır.");
                
            RuleFor(x => x.Unvan).MaximumLength(100).WithMessage("Unvan en fazla 100 karakter olabilir.");
            RuleFor(x => x.SorgulamaTarihi).NotEmpty().WithMessage("Sorgulama tarihi zorunludur.");
            RuleFor(x => x.PostaKutusu).MaximumLength(100).WithMessage("Posta kutusu adresi en fazla 100 karakter olabilir.");
            RuleFor(x => x.Etiket).MaximumLength(100).WithMessage("Etiket bilgisi en fazla 100 karakter olabilir.");
            RuleFor(x => x.XmlYanit).MaximumLength(500).WithMessage("XML yanıt en fazla 500 karakter olabilir.");
            RuleFor(x => x.OlusturanKullaniciId).NotEmpty().WithMessage("Oluşturan kullanıcı bilgisi zorunludur.");
        }
    }
}
