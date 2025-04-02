using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class IndirimTableValidator : AbstractValidator<IndirimTable>
    {
        public IndirimTableValidator()
        {
            // **StokKodu** zorunlu ve 30 karakteri geçemez
            RuleFor(x => x.StokKodu)
                .NotEmpty().WithMessage("Stok Kodu gereklidir.")
                .MaximumLength(30).WithMessage("Stok Kodu en fazla 30 karakter olabilir.");

            // **StokAdi** zorunlu ve 100 karakteri geçemez
            RuleFor(x => x.StokAdi)
                .NotEmpty().WithMessage("Stok Adı gereklidir.")
                .MaximumLength(100).WithMessage("Stok Adı en fazla 100 karakter olabilir.");

            // **IndirimTuru** zorunlu ve 50 karakteri geçemez
            RuleFor(x => x.IndirimTuru)
                .NotEmpty().WithMessage("İndirim Türü gereklidir.")
                .MaximumLength(50).WithMessage("İndirim Türü en fazla 50 karakter olabilir.");

            // **BaslangicTarihi** zorunlu ve geçerli bir tarih olmalı
            RuleFor(x => x.BaslangicTarihi)
                .NotEmpty().WithMessage("Başlangıç Tarihi gereklidir.")
                .LessThan(x => x.BitisTarihi).WithMessage("Başlangıç Tarihi, Bitiş Tarihinden önce olmalıdır.");

            // **BitisTarihi** zorunlu ve geçerli bir tarih olmalı
            RuleFor(x => x.BitisTarihi)
                .NotEmpty().WithMessage("Bitiş Tarihi gereklidir.");

            // **IndirimOrani** 0 ile 100 arasında olmalı
            RuleFor(x => x.IndirimOrani)
                .InclusiveBetween(0, 100).WithMessage("İndirim Oranı %0 ile %100 arasında olmalıdır.");

            // **Aciklama** 500 karakteri geçemez
            RuleFor(x => x.Aciklama)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");
        }
    }
}
