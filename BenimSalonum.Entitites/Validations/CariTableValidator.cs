using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class CariTableValidator : AbstractValidator<CariTable>
    {
        public CariTableValidator()
        {
            // **CariTuru** zorunlu ve 50 karakteri geçemez
            RuleFor(x => x.CariTuru)
                .NotEmpty().WithMessage("Cari Türü gereklidir.")
                .MaximumLength(50).WithMessage("Cari Türü en fazla 50 karakter olabilir.");

            // **CariKodu** zorunlu ve 20 karakteri geçemez
            RuleFor(x => x.CariKodu)
                .NotEmpty().WithMessage("Cari Kodu gereklidir.")
                .MaximumLength(20).WithMessage("Cari Kodu en fazla 20 karakter olabilir.");

            // **CariAdi** zorunlu ve 100 karakteri geçemez
            RuleFor(x => x.CariAdi)
                .NotEmpty().WithMessage("Cari Adı gereklidir.")
                .MaximumLength(100).WithMessage("Cari Adı en fazla 100 karakter olabilir.");

            // **FaturaUnvani** zorunlu ve 100 karakteri geçemez
            RuleFor(x => x.FaturaUnvani)
                .NotEmpty().WithMessage("Fatura Ünvanı gereklidir.")
                .MaximumLength(100).WithMessage("Fatura Ünvanı en fazla 100 karakter olabilir.");

            // **Il** zorunlu ve 50 karakteri geçemez
            RuleFor(x => x.Il)
                .NotEmpty().WithMessage("İl gereklidir.")
                .MaximumLength(50).WithMessage("İl en fazla 50 karakter olabilir.");

            // **Adres** zorunlu ve 250 karakteri geçemez
            RuleFor(x => x.Adres)
                .NotEmpty().WithMessage("Adres gereklidir.")
                .MaximumLength(250).WithMessage("Adres en fazla 250 karakter olabilir.");

            // **Email** formatı geçerli olmalı ve 100 karakteri geçemez
            RuleFor(x => x.EMail)
                .EmailAddress().WithMessage("Geçersiz e-posta adresi formatı.")
                .MaximumLength(100).WithMessage("E-posta adresi en fazla 100 karakter olabilir.");

            // **Web** URL formatında olmalı ve 150 karakteri geçemez
            RuleFor(x => x.Web)
                .Matches(@"^(https?://)?([a-z0-9-]+\.)+[a-z0-9]{2,4}(/.*)?$").WithMessage("Geçersiz URL formatı.")
                .MaximumLength(150).WithMessage("Web adresi en fazla 150 karakter olabilir.");

            // **Telefon** 15 karakteri geçemez
            RuleFor(x => x.Telefon)
                .MaximumLength(15).WithMessage("Telefon numarası en fazla 15 karakter olabilir.");

            // **CepTelefonu** 15 karakteri geçemez
            RuleFor(x => x.CepTelefonu)
                .MaximumLength(15).WithMessage("Cep Telefonu numarası en fazla 15 karakter olabilir.");

            // **VergiDairesi** 50 karakteri geçemez
            RuleFor(x => x.VergiDairesi)
                .MaximumLength(50).WithMessage("Vergi Dairesi en fazla 50 karakter olabilir.");

            // **VergiNo** 20 karakteri geçemez
            RuleFor(x => x.VergiNo)
                .MaximumLength(20).WithMessage("Vergi Numarası en fazla 20 karakter olabilir.");

            // **IskontoOrani** 2 ondalıklı decimal olmalı
            RuleFor(x => x.IskontoOrani)
                .InclusiveBetween(0, 100).WithMessage("İskonto oranı %0 ile %100 arasında olmalıdır.");

            // **RiskLimiti** 2 ondalıklı decimal olmalı
            RuleFor(x => x.RiskLimiti)
                .GreaterThanOrEqualTo(0).WithMessage("Risk Limiti negatif olamaz.");

            // **Aciklama** en fazla 500 karakter olmalı
            RuleFor(x => x.Aciklama)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");
        }
    }
}
