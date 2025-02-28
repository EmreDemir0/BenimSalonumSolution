using Microsoft.EntityFrameworkCore;
using BenimSalonum.Entities.Tables;
using System.Reflection; // ✅ Entities içindeki tabloların olduğu namespace
using BenimSalonum.Mappings;


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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CariTableMap).Assembly);



            modelBuilder.ApplyConfiguration(new CariTableMap());
            modelBuilder.ApplyConfiguration(new DepoTableMap());
            modelBuilder.ApplyConfiguration(new FisTableMap());
            modelBuilder.ApplyConfiguration(new HizliSatisGrupTableMap());
            modelBuilder.ApplyConfiguration(new HizliSatisUrunTableMap());
            modelBuilder.ApplyConfiguration(new IndirimTableMap());
            modelBuilder.ApplyConfiguration(new KasaHareketTableMap());
            modelBuilder.ApplyConfiguration(new KasaTableMap());
            modelBuilder.ApplyConfiguration(new KodTableMap());
            modelBuilder.ApplyConfiguration(new KullaniciLogTableMap());
            modelBuilder.ApplyConfiguration(new KullaniciRolTableMap());
            modelBuilder.ApplyConfiguration(new KullaniciTableMap());
            modelBuilder.ApplyConfiguration(new OdemeTuruTableMap());
            modelBuilder.ApplyConfiguration(new PersonelHareketTableMap());
            modelBuilder.ApplyConfiguration(new PersonelTableMap());
            modelBuilder.ApplyConfiguration(new StokHareketTableMap());
            modelBuilder.ApplyConfiguration(new StokTableMap());
            modelBuilder.ApplyConfiguration(new TanimlarTableMap());
        }
    }
}
