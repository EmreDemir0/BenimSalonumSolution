using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Mappings
{
    public class PersonelTableMap : IEntityTypeConfiguration<PersonelTable>
    {
        public void Configure(EntityTypeBuilder<PersonelTable> builder)
        {
            builder.HasKey(x => x.Id);
            
        }
    }
}
