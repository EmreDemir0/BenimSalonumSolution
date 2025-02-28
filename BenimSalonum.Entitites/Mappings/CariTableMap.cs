using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Mappings
{
    public class CariTableMap : IEntityTypeConfiguration<CariTable>
    {
        public void Configure(EntityTypeBuilder<CariTable> builder)
        {
            builder.HasKey(x => x.Id);
            
        }
    }
}
