using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Mappings
{
    public class DepoTableMap : IEntityTypeConfiguration<DepoTable>
    {
        public void Configure(EntityTypeBuilder<DepoTable> builder)
        {
            builder.HasKey(x => x.Id);
            
        }
    }
}
