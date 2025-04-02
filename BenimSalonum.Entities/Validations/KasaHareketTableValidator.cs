using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class KasaHareketTableValidator : AbstractValidator<KasaHareketTable>
    {
        public KasaHareketTableValidator()
        {
            // **FisKodu** zorunlu ve 20 karakteri geçemez
            RuleFor(x => x.FisKodu)
                .NotEmpty().WithMessage("Fiş Kodu gereklidir.")
                .MaximumLength(20).WithMessage("Fiş Kodu en fazla 20 karakter olabilir.");

            // **Hareket** zorunlu ve 50 karakteri geçemez
            RuleFor(x => x.Hareket)
                .NotEmpty().WithMessage("Hareket gereklidir.")
                .MaximumLength(50).WithMessage("Hareket en fazla 50 karakter olabilir.");

            // **KasaId** zorunlu ve geçerli bir KasaId olmalı
            RuleFor(x => x.KasaId)
                .GreaterThan(0).WithMessage("Geçerli bir Kasa ID'si gereklidir.");

            // **OdemeTuruId** zorunlu ve geçerli bir OdemeTuruId olmalı
            RuleFor(x => x.OdemeTuruId)
                .GreaterThan(0).WithMessage("Geçerli bir Ödeme Türü ID'si gereklidir.");

            // **CariId** nullable ve pozitif olmalı
            RuleFor(x => x.CariId)
                .GreaterThan(0).WithMessage("Cari Id pozitif bir değer olmalıdır.")
                .When(x => x.CariId.HasValue); // Eğer CariId varsa, kontrol edilmelidir

            // **Tarih** zorunlu, geçerli bir tarih olmalı
            RuleFor(x => x.Tarih)
                .NotEmpty().WithMessage("Tarih gereklidir.")
                .Must(date => date <= DateTime.Now).WithMessage("Tarih, şu anki tarihten büyük olamaz.");

            // **Tutar** zorunlu ve pozitif olmalı
            RuleFor(x => x.Tutar)
                .GreaterThanOrEqualTo(0).WithMessage("Tutar negatif olamaz.");
        }
    }
}
