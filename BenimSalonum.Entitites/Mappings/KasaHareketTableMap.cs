using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Mappings
{
    public class KasaHareketTableMap : IEntityTypeConfiguration<KasaHareketTable>
    {
        public void Configure(EntityTypeBuilder<KasaHareketTable> builder)
        {
            builder.HasKey(x => x.Id);
            
        }
    }
}
