using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Mappings
{
    public class FisTableMap : IEntityTypeConfiguration<FisTable>
    {
        public void Configure(EntityTypeBuilder<FisTable> builder)
        {
            builder.HasKey(x => x.Id);
            
        }
    }
}
