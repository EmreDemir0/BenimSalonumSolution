using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Mappings
{
    public class StokHareketTableMap : IEntityTypeConfiguration<StokHareketTable>
    {
        public void Configure(EntityTypeBuilder<StokHareketTable> builder)
        {
            builder.HasKey(x => x.Id);
            
        }
    }
}
