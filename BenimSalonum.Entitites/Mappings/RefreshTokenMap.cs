using BenimSalonum.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BenimSalonum.Entities.Mapping
{
    public class RefreshTokenMap : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");  // 🔹 Tablo adını belirle

            builder.HasKey(x => x.Id);  // 🔹 Primary Key

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasMaxLength(50);  // 🔹 Kullanıcı ID (string olarak)

            builder.Property(x => x.Token)
                .IsRequired()
                .HasMaxLength(256);  // 🔹 Token uzunluğu

            builder.Property(x => x.Expires)
                .IsRequired();

            builder.Property(x => x.IsRevoked)
                .HasDefaultValue(false);

            builder.Property(x => x.CreatedAt)
                .IsRequired();
        }
    }
}
