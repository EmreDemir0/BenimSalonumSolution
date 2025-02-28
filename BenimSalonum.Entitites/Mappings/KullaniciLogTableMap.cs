using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Mappings
{
    public class KullaniciLogTableMap : IEntityTypeConfiguration<KullaniciLogTable>
    {
        public void Configure(EntityTypeBuilder<KullaniciLogTable> builder)
        {
            builder.HasKey(x => x.Id);
            
        }
    }
}
