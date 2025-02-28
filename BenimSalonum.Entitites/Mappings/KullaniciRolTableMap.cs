using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BenimSalonum.Entities.Tables;

namespace BenimSalonum.Mappings
{
    public class KullaniciRolTableMap : IEntityTypeConfiguration<KullaniciRolTable>
    {
        public void Configure(EntityTypeBuilder<KullaniciRolTable> builder)
        {
            builder.HasKey(x => x.Id);
            
        }
    }
}
