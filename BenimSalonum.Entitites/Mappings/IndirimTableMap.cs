using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Mappings
{
    public class IndirimTableMap : IEntityTypeConfiguration<IndirimTable>
    {
        public void Configure(EntityTypeBuilder<IndirimTable> builder)
        {
            builder.HasKey(x => x.Id);
            
        }
    }
}
