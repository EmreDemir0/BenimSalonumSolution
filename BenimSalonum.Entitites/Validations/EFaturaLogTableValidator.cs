using FluentValidation;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Validations
{
    public class EFaturaLogTableValidator : AbstractValidator<EFaturaLogTable>
    {
        public EFaturaLogTableValidator()
        {
            RuleFor(x => x.FaturaId)
                .NotEmpty().WithMessage("Fatura ID gereklidir.");

            RuleFor(x => x.IslemTuru)
                .NotEmpty().WithMessage("İşlem Türü gereklidir.");

            RuleFor(x => x.IslemDurumu)
                .NotEmpty().WithMessage("İşlem Durumu gereklidir.");

            RuleFor(x => x.IslemTarihi)
                .NotNull().WithMessage("İşlem Tarihi gereklidir.");

            RuleFor(x => x.UUID)
                .MaximumLength(36).WithMessage("UUID en fazla 36 karakter olabilir.");

            RuleFor(x => x.BelgeNo)
                .MaximumLength(50).WithMessage("Belge No en fazla 50 karakter olabilir.");

            RuleFor(x => x.RequestData)
                .MaximumLength(500).WithMessage("İstek Verisi en fazla 500 karakter olabilir.");

            RuleFor(x => x.ResponseData)
                .MaximumLength(500).WithMessage("Yanıt Verisi en fazla 500 karakter olabilir.");

            RuleFor(x => x.HataMesaji)
                .MaximumLength(500).WithMessage("Hata Mesajı en fazla 500 karakter olabilir.");

            RuleFor(x => x.PdfUrl)
                .MaximumLength(500).WithMessage("PDF URL en fazla 500 karakter olabilir.");

            RuleFor(x => x.XmlUrl)
                .MaximumLength(500).WithMessage("XML URL en fazla 500 karakter olabilir.");

            RuleFor(x => x.ZrfUrl)
                .MaximumLength(500).WithMessage("ZRF URL en fazla 500 karakter olabilir.");

            RuleFor(x => x.EttnNo)
                .MaximumLength(100).WithMessage("ETTN No en fazla 100 karakter olabilir.");

            RuleFor(x => x.MailAdresi)
                .MaximumLength(100).WithMessage("Mail Adresi en fazla 100 karakter olabilir.")
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.MailAdresi)).WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.TelefonNo)
                .MaximumLength(20).WithMessage("Telefon No en fazla 20 karakter olabilir.");

            RuleFor(x => x.Aciklama)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");
        }
    }
}
