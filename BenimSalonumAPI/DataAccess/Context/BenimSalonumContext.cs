using BenimSalonum.Entities.Mapping;
using BenimSalonum.Entities.Tables;
using BenimSalonum.Entities.Mappings;
using Microsoft.EntityFrameworkCore;

namespace BenimSalonumAPI.DataAccess.Context
{
    public class BenimSalonumContext : DbContext
    {
        public BenimSalonumContext(DbContextOptions<BenimSalonumContext> options) : base(options) { }

        // 🔹 **Yeni RefreshToken DbSet Eklendi**
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        // DbSet'ler
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
        public DbSet<UserJwtToken> UserJwtTokens { get; set; }

        // Yeni eklenen DbSet'ler
        public DbSet<SubeTable> Subeler { get; set; }
        public DbSet<FaturaTable> Faturalar { get; set; }
        public DbSet<FaturaDetayTable> FaturaDetaylari { get; set; }
        public DbSet<SiparisTable> Siparisler { get; set; }
        public DbSet<SiparisDetayTable> SiparisDetaylari { get; set; }
        public DbSet<EFaturaLogTable> EFaturaLoglari { get; set; }
        public DbSet<RandevuTable> Randevular { get; set; }
        public DbSet<DuyuruTable> Duyurular { get; set; }
        public DbSet<AyarlarTable> Ayarlar { get; set; }

        // E-Fatura WSDL entegrasyonu için yeni eklenen DbSet'ler
        public DbSet<EFaturaKontorTable> EFaturaKontorlar { get; set; }
        public DbSet<GibMukellefTable> GibMukellefler { get; set; }
        public DbSet<EArsivRaporTable> EArsivRaporlar { get; set; }
        
        // Gelişmiş loglama için yeni eklenen DbSet
        public DbSet<SistemLogTable> SistemLoglar { get; set; }

        // 🔹 **Model mapping**
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
            modelBuilder.ApplyConfiguration(new UserJwtTokenMap());

            // 🔹 **Yeni RefreshToken Mapping'i ekleyelim**
            modelBuilder.ApplyConfiguration(new RefreshTokenMap());
            
            // Yeni eklenen mappingler
            modelBuilder.ApplyConfiguration(new SubeTableMap());
            modelBuilder.ApplyConfiguration(new FaturaTableMap());
            modelBuilder.ApplyConfiguration(new FaturaDetayTableMap());
            modelBuilder.ApplyConfiguration(new SiparisTableMap());
            modelBuilder.ApplyConfiguration(new SiparisDetayTableMap());
            modelBuilder.ApplyConfiguration(new EFaturaLogTableMap());
            modelBuilder.ApplyConfiguration(new RandevuTableMap());
            modelBuilder.ApplyConfiguration(new DuyuruTableMap());
            modelBuilder.ApplyConfiguration(new AyarlarTableMap());
            modelBuilder.ApplyConfiguration(new SistemLogTableMap());
        }
    }
}
