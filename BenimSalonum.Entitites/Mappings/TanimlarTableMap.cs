using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Mappings
{
    public class TanimlarTableMap : IEntityTypeConfiguration<TanimlarTable>
    {
        public void Configure(EntityTypeBuilder<TanimlarTable> builder)
        {
            builder.HasKey(x => x.Id);
            
        }
    }
}
