using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Mappings
{
    public class HizliSatisGrupTableMap : IEntityTypeConfiguration<HizliSatisGrupTable>
    {
        public void Configure(EntityTypeBuilder<HizliSatisGrupTable> builder)
        {
            builder.HasKey(x => x.Id);
            
        }
    }
}
