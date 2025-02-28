using Microsoft.EntityFrameworkCore;
using BenimSalonum.Entities.Tables;
using System.Reflection; // ✅ Entities içindeki tabloların olduğu namespace


namespace BenimSalonumAPI.DataAccess.Context
{
    public class BenimSalonumContext : DbContext
    {
        public BenimSalonumContext(DbContextOptions<BenimSalonumContext> options) : base(options) { }
        public DbSet<CariTable> Cariler { get; set; }
        public DbSet<DepoTable> Depolar { get; set; }
        public DbSet<FisTable> Fisler { get; set; }
        public DbSet<HizliSatisGrupTable> HizliSatisGruplari { get; set; }
        public DbSet<HizliSatisUrunTable> HizliSatisUrunleri { get; set; }
        public DbSet<IndirimTable> Indirimler { get; set; }
        public DbSet<KasaHareketTable> KasaHareketleri { get; set; }
        public DbSet<KasaTable> Kasalar { get; set; }
        public DbSet<KodTable> Kodlar { get; set; }
        public DbSet<KullaniciLogTable> KullaniciLoglari { get; set; }
        public DbSet<KullaniciRolTable> KullaniciRolleri { get; set; }
        public DbSet<KullaniciTable> Kullanicilar { get; set; }
        public DbSet<OdemeTuruTable> OdemeTurleri { get; set; }
        public DbSet<PersonelHareketTable> PersonelHareketleri { get; set; }
        public DbSet<PersonelTable> Personeller { get; set; }
        public DbSet<StokHareketTable> StokHareketleri { get; set; }
        public DbSet<StokTable> Stoklar { get; set; }
        public DbSet<TanimlarTable> Tanimlar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // **Tüm Mapping dosyalarını yükle**
        }
    }
}
