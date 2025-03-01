using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class FisTableValidator : AbstractValidator<FisTable>
    {
        public FisTableValidator()
        {
            // **FisKodu** zorunlu ve 20 karakteri geçemez
            RuleFor(x => x.FisKodu)
                .NotEmpty().WithMessage("Fiş Kodu gereklidir.")
                .MaximumLength(20).WithMessage("Fiş Kodu en fazla 20 karakter olabilir.");

            // **FisTuru** zorunlu ve 50 karakteri geçemez
            RuleFor(x => x.FisTuru)
                .NotEmpty().WithMessage("Fiş Türü gereklidir.")
                .MaximumLength(50).WithMessage("Fiş Türü en fazla 50 karakter olabilir.");

            // **CariId** nullable ve pozitif olmalı
            RuleFor(x => x.CariId)
                .GreaterThan(0).WithMessage("Cari Id pozitif bir değer olmalıdır.")
                .When(x => x.CariId.HasValue); // Eğer CariId varsa, kontrol edilmelidir

            // **FaturaUnvani** 100 karakteri geçemez
            RuleFor(x => x.FaturaUnvani)
                .MaximumLength(100).WithMessage("Fatura Ünvanı en fazla 100 karakter olabilir.");

            // **Telefon** 15 karakteri geçemez
            RuleFor(x => x.CepTelefonu)
                .MaximumLength(15).WithMessage("Cep Telefonu numarası en fazla 15 karakter olabilir.");

            // **Il** 50 karakteri geçemez
            RuleFor(x => x.Il)
                .MaximumLength(50).WithMessage("İl en fazla 50 karakter olabilir.");

            // **Ilce** 50 karakteri geçemez
            RuleFor(x => x.Ilce)
                .MaximumLength(50).WithMessage("İlçe en fazla 50 karakter olabilir.");

            // **Adres** 250 karakteri geçemez
            RuleFor(x => x.Adres)
                .MaximumLength(250).WithMessage("Adres en fazla 250 karakter olabilir.");

            // **VergiDairesi** 50 karakteri geçemez
            RuleFor(x => x.VergiDairesi)
                .MaximumLength(50).WithMessage("Vergi Dairesi en fazla 50 karakter olabilir.");

            // **VergiNo** 20 karakteri geçemez
            RuleFor(x => x.VergiNo)
                .MaximumLength(20).WithMessage("Vergi Numarası en fazla 20 karakter olabilir.");

            // **BelgeNo** 30 karakteri geçemez
            RuleFor(x => x.BelgeNo)
                .MaximumLength(30).WithMessage("Belge Numarası en fazla 30 karakter olabilir.");

            // **Tarih** zorunlu, geçerli tarih olmalı
            RuleFor(x => x.Tarih)
                .NotEmpty().WithMessage("Tarih gereklidir.")
                .Must(date => date <= DateTime.Now).WithMessage("Tarih, şu anki tarihten büyük olamaz.");

            // **PlasiyerId** nullable ve pozitif olmalı
            RuleFor(x => x.PlasiyerId)
                .GreaterThan(0).WithMessage("Plasiyer Id pozitif bir değer olmalıdır.")
                .When(x => x.PlasiyerId.HasValue); // Eğer PlasiyerId varsa, kontrol edilmelidir

            // **IskontoOrani** 0 ile 100 arasında olmalı
            RuleFor(x => x.IskontoOrani)
                .InclusiveBetween(0, 100).WithMessage("İskonto oranı %0 ile %100 arasında olmalıdır.")
                .When(x => x.IskontoOrani.HasValue);

            // **IskontoTutar** 2 ondalıklı decimal olmalı
            RuleFor(x => x.IskontoTutar)
                .GreaterThanOrEqualTo(0).WithMessage("İskonto Tutarı negatif olamaz.")
                .When(x => x.IskontoTutar.HasValue);

            // **Alacak** 2 ondalıklı decimal olmalı
            RuleFor(x => x.Alacak)
                .GreaterThanOrEqualTo(0).WithMessage("Alacak tutarı negatif olamaz.")
                .When(x => x.Alacak.HasValue);

            // **Borc** 2 ondalıklı decimal olmalı
            RuleFor(x => x.Borc)
                .GreaterThanOrEqualTo(0).WithMessage("Borc tutarı negatif olamaz.")
                .When(x => x.Borc.HasValue);

            // **ToplamTutar** 2 ondalıklı decimal olmalı
            RuleFor(x => x.ToplamTutar)
                .GreaterThanOrEqualTo(0).WithMessage("Toplam Tutar negatif olamaz.")
                .When(x => x.ToplamTutar.HasValue);

            // **Aciklama** 500 karakteri geçemez
            RuleFor(x => x.Aciklama)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");

            // **FisBaglantiKodu** 30 karakteri geçemez
            RuleFor(x => x.FisBaglantiKodu)
                .MaximumLength(30).WithMessage("Fiş Bağlantı Kodu en fazla 30 karakter olabilir.");
        }
    }
}
