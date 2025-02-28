using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Mappings
{
    public class KullaniciTableMap : IEntityTypeConfiguration<KullaniciTable>
    {
        public void Configure(EntityTypeBuilder<KullaniciTable> builder)
        {
            builder.HasKey(x => x.Id);
            
        }
    }
}
