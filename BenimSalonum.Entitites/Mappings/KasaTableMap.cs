using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Mappings
{
    public class KasaTableMap : IEntityTypeConfiguration<KasaTable>
    {
        public void Configure(EntityTypeBuilder<KasaTable> builder)
        {
            builder.HasKey(x => x.Id);
            
        }
    }
}
