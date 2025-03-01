using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Entities.Mapping
{
    public class OdemeTuruTableMap : IEntityTypeConfiguration<OdemeTuruTable>
    {
        public void Configure(EntityTypeBuilder<OdemeTuruTable> builder)
        {
            builder.HasKey(e => e.Id);

            // Ek mapping ayarlarýný burada tanýmlayabilirsiniz.
        }
    }
}
