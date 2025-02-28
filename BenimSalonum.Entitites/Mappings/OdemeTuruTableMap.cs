using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Mappings
{
    public class OdemeTuruTableMap : IEntityTypeConfiguration<OdemeTuruTable>
    {
        public void Configure(EntityTypeBuilder<OdemeTuruTable> builder)
        {
            builder.HasKey(x => x.Id);
            
        }
    }
}
