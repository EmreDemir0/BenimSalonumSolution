using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenimSalonumAPI.Migrations
{
    /// <inheritdoc />
    public partial class EFaturaWsdlEntegrasyonu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlisFiyati1",
                table: "Stoklar");

            migrationBuilder.DropColumn(
                name: "AlisFiyati2",
                table: "Stoklar");

            migrationBuilder.DropColumn(
                name: "AlisFiyati3",
                table: "Stoklar");

            migrationBuilder.DropColumn(
                name: "SatisFiyati1",
                table: "Stoklar");

            migrationBuilder.DropColumn(
                name: "SatisFiyati2",
                table: "Stoklar");

            migrationBuilder.DropColumn(
                name: "SatisFiyati3",
                table: "Stoklar");

            migrationBuilder.AddColumn<decimal>(
                name: "AlisFiyati",
                table: "Stoklar",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "FiyatHesaplamaTipi",
                table: "Stoklar",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "GuncelleyenKullaniciId",
                table: "Stoklar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "HepsiburadaFiyati",
                table: "Stoklar",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "HepsiburadaKodu",
                table: "Stoklar",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HepsiburadaKomisyon",
                table: "Stoklar",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HepsiburadaStokMiktari",
                table: "Stoklar",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PerakendeFiyati",
                table: "Stoklar",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "SonGuncelleme",
                table: "Stoklar",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<decimal>(
                name: "StokMiktari",
                table: "Stoklar",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "StokTakipTipi",
                table: "Stoklar",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<decimal>(
                name: "ToplamFiyati",
                table: "Stoklar",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TrendyolFiyati",
                table: "Stoklar",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "TrendyolKodu",
                table: "Stoklar",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TrendyolKomisyon",
                table: "Stoklar",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TrendyolStokMiktari",
                table: "Stoklar",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "WebAciklama",
                table: "Stoklar",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WebFiyati",
                table: "Stoklar",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "WebKodu",
                table: "Stoklar",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WebKomisyon",
                table: "Stoklar",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WebStokMiktari",
                table: "Stoklar",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "EArsivKullanicisi",
                table: "Cariler",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EFaturaKullanicisi",
                table: "Cariler",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "EFaturaPostaKutusu",
                table: "Cariler",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EIrsaliyeKullanicisi",
                table: "Cariler",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "EticaretVergiNo",
                table: "Cariler",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FirmaMi",
                table: "Cariler",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "GuncellenmeTarihi",
                table: "Cariler",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GuncelleyenKullaniciId",
                table: "Cariler",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KaydedenKullaniciId",
                table: "Cariler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TCKN",
                table: "Cariler",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ayarlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubeId = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    AyarAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AyarGrubu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AyarDegeri = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SiraNo = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    YedeklemePeriyodu = table.Column<int>(type: "int", nullable: true),
                    YedeklemeSaati = table.Column<int>(type: "int", nullable: true),
                    YedeklemeSaklama = table.Column<int>(type: "int", nullable: true),
                    SmsApiKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SmsApiSecret = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SmsApiUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SmsTitleId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    MailHost = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MailPort = table.Column<int>(type: "int", nullable: true),
                    MailKullanici = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MailSifre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MailSsl = table.Column<bool>(type: "bit", nullable: true),
                    MailGonderenAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EFaturaEntegratorUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EFaturaKullanici = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EFaturaSifre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    WhatsappApiKey = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    WhatsappInstanceId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WhatsappPhoneNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    OlusturanKullaniciId = table.Column<int>(type: "int", nullable: false),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GuncelleyenKullaniciId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ayarlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Duyurular",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baslik = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Icerik = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    ResimUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DuyuruTipi = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    HedefKitlee = table.Column<int>(type: "int", nullable: true),
                    HedefSubeId = table.Column<int>(type: "int", nullable: true),
                    YayinBaslangic = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    YayinBitis = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    OlusturanKullaniciId = table.Column<int>(type: "int", nullable: false),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GuncelleyenKullaniciId = table.Column<int>(type: "int", nullable: true),
                    OkunduBilgisiToplansin = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    GoruntulenmeSayisi = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duyurular", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GibMukellefler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VKN_TCKN = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Unvan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EFaturaKullanicisi = table.Column<bool>(type: "bit", nullable: false),
                    EIrsaliyeKullanicisi = table.Column<bool>(type: "bit", nullable: false),
                    SorgulamaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GecerlilikTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PostaKutusu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Etiket = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    XmlYanit = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OlusturanKullaniciId = table.Column<int>(type: "int", nullable: false),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GuncelleyenKullaniciId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GibMukellefler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Randevular",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MusteriId = table.Column<int>(type: "int", nullable: false),
                    PersonelId = table.Column<int>(type: "int", nullable: false),
                    SubeId = table.Column<int>(type: "int", nullable: false),
                    HizmetId = table.Column<int>(type: "int", nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notlar = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Durum = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    SmsGonderildi = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    EmailGonderildi = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    HatirlatmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Tutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    IndirimTutari = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    OdemeYapildi = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    OdemeTuruId = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    OlusturanKullaniciId = table.Column<int>(type: "int", nullable: false),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GuncelleyenKullaniciId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Randevular_Cariler_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Cariler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Randevular_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subeler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubeAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SubeKodu = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WebSite = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VergiDairesi = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    VergiNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    LisansKodu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LisansBitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KullaniciLimiti = table.Column<int>(type: "int", nullable: false, defaultValue: 5),
                    EFaturaKullanimi = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    WhatsappKullanimi = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SmsKullanimi = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    EMailKullanimi = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    EticaretEntegrasyonu = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    OlusturanKullaniciId = table.Column<int>(type: "int", nullable: false),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GuncelleyenKullaniciId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subeler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EArsivRaporlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RaporNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RaporTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Durum = table.Column<int>(type: "int", nullable: false),
                    HataMesaji = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RaporUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UUID = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    SubeId = table.Column<int>(type: "int", nullable: false),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OlusturanKullaniciId = table.Column<int>(type: "int", nullable: false),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GuncelleyenKullaniciId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EArsivRaporlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EArsivRaporlar_Subeler_SubeId",
                        column: x => x.SubeId,
                        principalTable: "Subeler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EFaturaKontorlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubeId = table.Column<int>(type: "int", nullable: false),
                    ToplamKontor = table.Column<int>(type: "int", nullable: false),
                    KalanKontor = table.Column<int>(type: "int", nullable: false),
                    KullanilanKontor = table.Column<int>(type: "int", nullable: false),
                    KontorTipi = table.Column<int>(type: "int", nullable: false),
                    UstKontorId = table.Column<int>(type: "int", nullable: true),
                    SatinAlmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SonKullanmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Aktif = table.Column<bool>(type: "bit", nullable: false),
                    SiparisNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FaturaNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Tutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OlusturanKullaniciId = table.Column<int>(type: "int", nullable: false),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GuncelleyenKullaniciId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EFaturaKontorlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EFaturaKontorlar_EFaturaKontorlar_UstKontorId",
                        column: x => x.UstKontorId,
                        principalTable: "EFaturaKontorlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EFaturaKontorlar_Subeler_SubeId",
                        column: x => x.SubeId,
                        principalTable: "Subeler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Faturalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubeId = table.Column<int>(type: "int", nullable: false),
                    FaturaTuru = table.Column<int>(type: "int", nullable: false),
                    FaturaTipi = table.Column<int>(type: "int", nullable: false),
                    FaturaNo = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    BelgeNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EfaturaNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EarsivNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EirsaliyeNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CariId = table.Column<int>(type: "int", nullable: false),
                    FaturaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SevkTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VadeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OdemeSekli = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AraToplam = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    KdvToplam = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    IndirimTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    GenelToplam = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    KdvDahil = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    OdenenTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    KalanTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    FaturaDurumu = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    OdemeDurumu = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    EfaturaGuid = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EfaturaDurum = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EfaturaHata = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EfaturaGonderimTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EfaturaBasarili = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    EfaturaPdf = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EfaturaXml = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CariUnvan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CariTckn = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    CariVkn = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    CariVergiDairesi = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CariAdres = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CariIl = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CariIlce = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    SiparisId = table.Column<int>(type: "int", nullable: true),
                    IrsaliyeId = table.Column<int>(type: "int", nullable: true),
                    MaliYil = table.Column<int>(type: "int", nullable: false),
                    MaliDonem = table.Column<int>(type: "int", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    OlusturanKullaniciId = table.Column<int>(type: "int", nullable: false),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GuncelleyenKullaniciId = table.Column<int>(type: "int", nullable: true),
                    OnayTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OnaylayanKullaniciId = table.Column<int>(type: "int", nullable: true),
                    IptalTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IptalEdenKullaniciId = table.Column<int>(type: "int", nullable: true),
                    IptalNedeni = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    GibFaturaDurumu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PostaKutusuId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EtiketId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EarsivRaporId = table.Column<int>(type: "int", nullable: true),
                    KullanılanKontorAdet = table.Column<int>(type: "int", nullable: true),
                    EArsivRaporTableId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faturalar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faturalar_Cariler_CariId",
                        column: x => x.CariId,
                        principalTable: "Cariler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Faturalar_EArsivRaporlar_EArsivRaporTableId",
                        column: x => x.EArsivRaporTableId,
                        principalTable: "EArsivRaporlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Faturalar_Subeler_SubeId",
                        column: x => x.SubeId,
                        principalTable: "Subeler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EFaturaLoglari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FaturaId = table.Column<int>(type: "int", nullable: false),
                    IslemTuru = table.Column<int>(type: "int", nullable: false),
                    IslemDurumu = table.Column<int>(type: "int", nullable: false),
                    IslemTarihi = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UUID = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    BelgeNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RequestData = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ResponseData = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    HataMesaji = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PdfUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    XmlUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ZrfUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EttnNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MailGonderildi = table.Column<bool>(type: "bit", nullable: false),
                    SmsGonderildi = table.Column<bool>(type: "bit", nullable: false),
                    MailGonderimTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SmsGonderimTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MailAdresi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TelefonNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EnvelopeId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EarsivRaporDurumu = table.Column<int>(type: "int", nullable: true),
                    GibMesajId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    KontorMiktari = table.Column<int>(type: "int", nullable: true),
                    FaturaTableId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EFaturaLoglari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EFaturaLoglari_Faturalar_FaturaId",
                        column: x => x.FaturaId,
                        principalTable: "Faturalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EFaturaLoglari_Faturalar_FaturaTableId",
                        column: x => x.FaturaTableId,
                        principalTable: "Faturalar",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FaturaDetaylari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FaturaId = table.Column<int>(type: "int", nullable: false),
                    SiraNo = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    StokId = table.Column<int>(type: "int", nullable: true),
                    StokKodu = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    UrunAdi = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Barkod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Birim = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Miktar = table.Column<decimal>(type: "decimal(18,3)", nullable: false, defaultValue: 0m),
                    BirimFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    KdvOrani = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    KdvTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    AraToplam = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    ToplamTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    IndirimTuru = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IndirimOrani = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    IndirimTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    GTIPNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    IskontoUygula = table.Column<bool>(type: "bit", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    KdvDahil = table.Column<bool>(type: "bit", nullable: false),
                    EfaturaKod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EfaturaTip = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TevkifatKodu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TevkifatOrani = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    OlusturanKullaniciId = table.Column<int>(type: "int", nullable: false),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GuncelleyenKullaniciId = table.Column<int>(type: "int", nullable: true),
                    FaturaTableId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaturaDetaylari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaturaDetaylari_Faturalar_FaturaId",
                        column: x => x.FaturaId,
                        principalTable: "Faturalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FaturaDetaylari_Faturalar_FaturaTableId",
                        column: x => x.FaturaTableId,
                        principalTable: "Faturalar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FaturaDetaylari_Stoklar_StokId",
                        column: x => x.StokId,
                        principalTable: "Stoklar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Siparisler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubeId = table.Column<int>(type: "int", nullable: false),
                    SiparisTuru = table.Column<int>(type: "int", nullable: false),
                    SiparisNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CariId = table.Column<int>(type: "int", nullable: false),
                    SiparisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TeslimTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SiparisDurumu = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    OnayDurumu = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    EticaretPlatformu = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    EticaretSiparisNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AraToplam = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    KdvToplam = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    IndirimTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    KargoTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    GenelToplam = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    FaturaKesildi = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    FaturaId = table.Column<int>(type: "int", nullable: true),
                    KargoSirketi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    KargoTakipNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    KargoTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CariUnvan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TeslimatAdresi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TeslimatIl = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TeslimatIlce = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TelefonNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    MaliYil = table.Column<int>(type: "int", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    OlusturanKullaniciId = table.Column<int>(type: "int", nullable: false),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GuncelleyenKullaniciId = table.Column<int>(type: "int", nullable: true),
                    OnayTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OnaylayanKullaniciId = table.Column<int>(type: "int", nullable: true),
                    IptalTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IptalEdenKullaniciId = table.Column<int>(type: "int", nullable: true),
                    IptalNedeni = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Siparisler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Siparisler_Cariler_CariId",
                        column: x => x.CariId,
                        principalTable: "Cariler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Siparisler_Faturalar_FaturaId",
                        column: x => x.FaturaId,
                        principalTable: "Faturalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Siparisler_Subeler_SubeId",
                        column: x => x.SubeId,
                        principalTable: "Subeler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SiparisDetaylari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiparisId = table.Column<int>(type: "int", nullable: false),
                    SiraNo = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    StokId = table.Column<int>(type: "int", nullable: true),
                    StokKodu = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    UrunAdi = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Barkod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Birim = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Miktar = table.Column<decimal>(type: "decimal(18,3)", nullable: false, defaultValue: 0m),
                    BirimFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    KdvOrani = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    KdvTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    AraToplam = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    ToplamTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    IndirimTuru = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IndirimOrani = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    IndirimTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    KdvDahil = table.Column<bool>(type: "bit", nullable: false),
                    SiparisDurumu = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    KarsilananMiktar = table.Column<decimal>(type: "decimal(18,3)", nullable: false, defaultValue: 0m),
                    EticaretUrunKodu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EticaretSepetItemId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    OlusturanKullaniciId = table.Column<int>(type: "int", nullable: false),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GuncelleyenKullaniciId = table.Column<int>(type: "int", nullable: true),
                    SiparisTableId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiparisDetaylari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiparisDetaylari_Siparisler_SiparisId",
                        column: x => x.SiparisId,
                        principalTable: "Siparisler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SiparisDetaylari_Siparisler_SiparisTableId",
                        column: x => x.SiparisTableId,
                        principalTable: "Siparisler",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SiparisDetaylari_Stoklar_StokId",
                        column: x => x.StokId,
                        principalTable: "Stoklar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ayarlar_AyarGrubu",
                table: "Ayarlar",
                column: "AyarGrubu");

            migrationBuilder.CreateIndex(
                name: "IX_Ayarlar_SubeId_AyarAdi",
                table: "Ayarlar",
                columns: new[] { "SubeId", "AyarAdi" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Duyuru_DuyuruTipi",
                table: "Duyurular",
                column: "DuyuruTipi");

            migrationBuilder.CreateIndex(
                name: "IX_Duyuru_HedefSubeId",
                table: "Duyurular",
                column: "HedefSubeId");

            migrationBuilder.CreateIndex(
                name: "IX_Duyuru_OlusturanKullaniciId",
                table: "Duyurular",
                column: "OlusturanKullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_EArsivRaporlar_Durum",
                table: "EArsivRaporlar",
                column: "Durum");

            migrationBuilder.CreateIndex(
                name: "IX_EArsivRaporlar_RaporNo",
                table: "EArsivRaporlar",
                column: "RaporNo");

            migrationBuilder.CreateIndex(
                name: "IX_EArsivRaporlar_RaporTarihi",
                table: "EArsivRaporlar",
                column: "RaporTarihi");

            migrationBuilder.CreateIndex(
                name: "IX_EArsivRaporlar_SubeId",
                table: "EArsivRaporlar",
                column: "SubeId");

            migrationBuilder.CreateIndex(
                name: "IX_EArsivRaporlar_UUID",
                table: "EArsivRaporlar",
                column: "UUID");

            migrationBuilder.CreateIndex(
                name: "IX_EFaturaKontorlar_KontorTipi",
                table: "EFaturaKontorlar",
                column: "KontorTipi");

            migrationBuilder.CreateIndex(
                name: "IX_EFaturaKontorlar_SatinAlmaTarihi",
                table: "EFaturaKontorlar",
                column: "SatinAlmaTarihi");

            migrationBuilder.CreateIndex(
                name: "IX_EFaturaKontorlar_SubeId",
                table: "EFaturaKontorlar",
                column: "SubeId");

            migrationBuilder.CreateIndex(
                name: "IX_EFaturaKontorlar_UstKontorId",
                table: "EFaturaKontorlar",
                column: "UstKontorId");

            migrationBuilder.CreateIndex(
                name: "IX_EFaturaLog_FaturaId",
                table: "EFaturaLoglari",
                column: "FaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_EFaturaLog_IslemDurumu",
                table: "EFaturaLoglari",
                column: "IslemDurumu");

            migrationBuilder.CreateIndex(
                name: "IX_EFaturaLog_IslemTarihi",
                table: "EFaturaLoglari",
                column: "IslemTarihi");

            migrationBuilder.CreateIndex(
                name: "IX_EFaturaLog_IslemTuru",
                table: "EFaturaLoglari",
                column: "IslemTuru");

            migrationBuilder.CreateIndex(
                name: "IX_EFaturaLog_UUID",
                table: "EFaturaLoglari",
                column: "UUID");

            migrationBuilder.CreateIndex(
                name: "IX_EFaturaLoglari_FaturaTableId",
                table: "EFaturaLoglari",
                column: "FaturaTableId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturaDetay_FaturaId",
                table: "FaturaDetaylari",
                column: "FaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturaDetay_StokId",
                table: "FaturaDetaylari",
                column: "StokId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturaDetaylari_FaturaTableId",
                table: "FaturaDetaylari",
                column: "FaturaTableId");

            migrationBuilder.CreateIndex(
                name: "IX_Fatura_CariId",
                table: "Faturalar",
                column: "CariId");

            migrationBuilder.CreateIndex(
                name: "IX_Fatura_FaturaDurumu",
                table: "Faturalar",
                column: "FaturaDurumu");

            migrationBuilder.CreateIndex(
                name: "IX_Fatura_FaturaNo_SubeId",
                table: "Faturalar",
                columns: new[] { "FaturaNo", "SubeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fatura_FaturaTarihi",
                table: "Faturalar",
                column: "FaturaTarihi");

            migrationBuilder.CreateIndex(
                name: "IX_Fatura_MaliYil_MaliDonem",
                table: "Faturalar",
                columns: new[] { "MaliYil", "MaliDonem" });

            migrationBuilder.CreateIndex(
                name: "IX_Fatura_OdemeDurumu",
                table: "Faturalar",
                column: "OdemeDurumu");

            migrationBuilder.CreateIndex(
                name: "IX_Fatura_VadeTarihi",
                table: "Faturalar",
                column: "VadeTarihi");

            migrationBuilder.CreateIndex(
                name: "IX_Faturalar_EArsivRaporTableId",
                table: "Faturalar",
                column: "EArsivRaporTableId");

            migrationBuilder.CreateIndex(
                name: "IX_Faturalar_SubeId",
                table: "Faturalar",
                column: "SubeId");

            migrationBuilder.CreateIndex(
                name: "IX_GibMukellefler_GecerlilikTarihi",
                table: "GibMukellefler",
                column: "GecerlilikTarihi");

            migrationBuilder.CreateIndex(
                name: "IX_GibMukellefler_SorgulamaTarihi",
                table: "GibMukellefler",
                column: "SorgulamaTarihi");

            migrationBuilder.CreateIndex(
                name: "IX_GibMukellefler_VKN_TCKN",
                table: "GibMukellefler",
                column: "VKN_TCKN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_HizmetId",
                table: "Randevular",
                column: "HizmetId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_MusteriId",
                table: "Randevular",
                column: "MusteriId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_PersonelId",
                table: "Randevular",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_SubeId",
                table: "Randevular",
                column: "SubeId");

            migrationBuilder.CreateIndex(
                name: "IX_SiparisDetay_SiparisId",
                table: "SiparisDetaylari",
                column: "SiparisId");

            migrationBuilder.CreateIndex(
                name: "IX_SiparisDetay_StokId",
                table: "SiparisDetaylari",
                column: "StokId");

            migrationBuilder.CreateIndex(
                name: "IX_SiparisDetaylari_SiparisTableId",
                table: "SiparisDetaylari",
                column: "SiparisTableId");

            migrationBuilder.CreateIndex(
                name: "IX_Siparis_CariId",
                table: "Siparisler",
                column: "CariId");

            migrationBuilder.CreateIndex(
                name: "IX_Siparis_EticaretPlatformu",
                table: "Siparisler",
                column: "EticaretPlatformu");

            migrationBuilder.CreateIndex(
                name: "IX_Siparis_EticaretSiparisNo",
                table: "Siparisler",
                column: "EticaretSiparisNo");

            migrationBuilder.CreateIndex(
                name: "IX_Siparis_FaturaId",
                table: "Siparisler",
                column: "FaturaId",
                unique: true,
                filter: "[FaturaId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Siparis_MaliYil",
                table: "Siparisler",
                column: "MaliYil");

            migrationBuilder.CreateIndex(
                name: "IX_Siparis_OnayDurumu",
                table: "Siparisler",
                column: "OnayDurumu");

            migrationBuilder.CreateIndex(
                name: "IX_Siparis_SiparisDurumu",
                table: "Siparisler",
                column: "SiparisDurumu");

            migrationBuilder.CreateIndex(
                name: "IX_Siparis_SiparisNo_SubeId",
                table: "Siparisler",
                columns: new[] { "SiparisNo", "SubeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Siparis_SiparisTarihi",
                table: "Siparisler",
                column: "SiparisTarihi");

            migrationBuilder.CreateIndex(
                name: "IX_Siparisler_SubeId",
                table: "Siparisler",
                column: "SubeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sube_AktifMi",
                table: "Subeler",
                column: "AktifMi");

            migrationBuilder.CreateIndex(
                name: "IX_Sube_SubeAdi",
                table: "Subeler",
                column: "SubeAdi",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ayarlar");

            migrationBuilder.DropTable(
                name: "Duyurular");

            migrationBuilder.DropTable(
                name: "EFaturaKontorlar");

            migrationBuilder.DropTable(
                name: "EFaturaLoglari");

            migrationBuilder.DropTable(
                name: "FaturaDetaylari");

            migrationBuilder.DropTable(
                name: "GibMukellefler");

            migrationBuilder.DropTable(
                name: "Randevular");

            migrationBuilder.DropTable(
                name: "SiparisDetaylari");

            migrationBuilder.DropTable(
                name: "Siparisler");

            migrationBuilder.DropTable(
                name: "Faturalar");

            migrationBuilder.DropTable(
                name: "EArsivRaporlar");

            migrationBuilder.DropTable(
                name: "Subeler");

            migrationBuilder.DropColumn(
                name: "AlisFiyati",
                table: "Stoklar");

            migrationBuilder.DropColumn(
                name: "FiyatHesaplamaTipi",
                table: "Stoklar");

            migrationBuilder.DropColumn(
                name: "GuncelleyenKullaniciId",
                table: "Stoklar");

            migrationBuilder.DropColumn(
                name: "HepsiburadaFiyati",
                table: "Stoklar");

            migrationBuilder.DropColumn(
                name: "HepsiburadaKodu",
                table: "Stoklar");

            migrationBuilder.DropColumn(
                name: "HepsiburadaKomisyon",
                table: "Stoklar");

            migrationBuilder.DropColumn(
                name: "HepsiburadaStokMiktari",
                table: "Stoklar");

            migrationBuilder.DropColumn(
                name: "PerakendeFiyati",
                table: "Stoklar");

            migrationBuilder.DropColumn(
                name: "SonGuncelleme",
                table: "Stoklar");

            migrationBuilder.DropColumn(
                name: "StokMiktari",
                table: "Stoklar");

            migrationBuilder.DropColumn(
                name: "StokTakipTipi",
                table: "Stoklar");

            migrationBuilder.DropColumn(
                name: "ToplamFiyati",
                table: "Stoklar");

            migrationBuilder.DropColumn(
                name: "TrendyolFiyati",
                table: "Stoklar");

            migrationBuilder.DropColumn(
                name: "TrendyolKodu",
                table: "Stoklar");

            migrationBuilder.DropColumn(
                name: "TrendyolKomisyon",
                table: "Stoklar");

            migrationBuilder.DropColumn(
                name: "TrendyolStokMiktari",
                table: "Stoklar");

            migrationBuilder.DropColumn(
                name: "WebAciklama",
                table: "Stoklar");

            migrationBuilder.DropColumn(
                name: "WebFiyati",
                table: "Stoklar");

            migrationBuilder.DropColumn(
                name: "WebKodu",
                table: "Stoklar");

            migrationBuilder.DropColumn(
                name: "WebKomisyon",
                table: "Stoklar");

            migrationBuilder.DropColumn(
                name: "WebStokMiktari",
                table: "Stoklar");

            migrationBuilder.DropColumn(
                name: "EArsivKullanicisi",
                table: "Cariler");

            migrationBuilder.DropColumn(
                name: "EFaturaKullanicisi",
                table: "Cariler");

            migrationBuilder.DropColumn(
                name: "EFaturaPostaKutusu",
                table: "Cariler");

            migrationBuilder.DropColumn(
                name: "EIrsaliyeKullanicisi",
                table: "Cariler");

            migrationBuilder.DropColumn(
                name: "EticaretVergiNo",
                table: "Cariler");

            migrationBuilder.DropColumn(
                name: "FirmaMi",
                table: "Cariler");

            migrationBuilder.DropColumn(
                name: "GuncellenmeTarihi",
                table: "Cariler");

            migrationBuilder.DropColumn(
                name: "GuncelleyenKullaniciId",
                table: "Cariler");

            migrationBuilder.DropColumn(
                name: "KaydedenKullaniciId",
                table: "Cariler");

            migrationBuilder.DropColumn(
                name: "TCKN",
                table: "Cariler");

            migrationBuilder.AddColumn<decimal>(
                name: "AlisFiyati1",
                table: "Stoklar",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AlisFiyati2",
                table: "Stoklar",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AlisFiyati3",
                table: "Stoklar",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SatisFiyati1",
                table: "Stoklar",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SatisFiyati2",
                table: "Stoklar",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SatisFiyati3",
                table: "Stoklar",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
