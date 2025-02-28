using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Mappings
{
    public class StokTableMap : IEntityTypeConfiguration<StokTable>
    {
        public void Configure(EntityTypeBuilder<StokTable> builder)
        {
            builder.HasKey(x => x.Id);
            
        }
    }
}
