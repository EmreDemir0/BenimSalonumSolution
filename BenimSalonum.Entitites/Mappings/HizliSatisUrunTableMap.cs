using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Mappings
{
    public class HizliSatisUrunTableMap : IEntityTypeConfiguration<HizliSatisUrunTable>
    {
        public void Configure(EntityTypeBuilder<HizliSatisUrunTable> builder)
        {
            builder.HasKey(x => x.Id);
            
        }
    }
}
