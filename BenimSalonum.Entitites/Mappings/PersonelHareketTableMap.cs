using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Mappings
{
    public class PersonelHareketTableMap : IEntityTypeConfiguration<PersonelHareketTable>
    {
        public void Configure(EntityTypeBuilder<PersonelHareketTable> builder)
        {
            builder.HasKey(x => x.Id);
            
        }
    }
}
