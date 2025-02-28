using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Mappings
{
    public class KodTableMap : IEntityTypeConfiguration<KodTable>
    {
        public void Configure(EntityTypeBuilder<KodTable> builder)
        {
            builder.HasKey(x => x.Id);
            
        }
    }
}
