using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

public class UserJwtTokenMap : IEntityTypeConfiguration<UserJwtToken>
{
    public void Configure(EntityTypeBuilder<UserJwtToken> builder)
    {
        builder.ToTable("UserJwtTokens");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Username).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Role).HasMaxLength(50);
        builder.Property(x => x.Token).HasMaxLength(500);
        builder.Property(x => x.Expiration).IsRequired();

        // Foreign Key (Kullanicilar ile bağlantı)
        builder.HasOne<KullaniciTable>()  // Bağlantı yapılacak entity sınıfı
               .WithMany()           // Kullanıcıya ait birden fazla token olabilir
               .HasForeignKey(x => x.UserId) // `UserId` üzerinden bağlanıyor
               .OnDelete(DeleteBehavior.Cascade); // Kullanıcı silinirse tokenları da sil
    }
}
