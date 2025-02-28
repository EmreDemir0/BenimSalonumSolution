using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenimSalonumAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cariler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Durumu = table.Column<bool>(type: "bit", nullable: false),
                    CariTuru = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CariKodu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CariAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    YetkiliKisi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FaturaUnvani = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CepTelefonu = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    EMail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Web = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Il = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ilce = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Semt = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Adres = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CariGrubu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CariAltGrubu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OzelKod1 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    OzelKod2 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    OzelKod3 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    OzelKod4 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    VergiDairesi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VergiNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IskontoOrani = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RiskLimiti = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cariler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Depolar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepoKodu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DepoAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    YetkiliKodu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    YetkiliAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Il = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Ilce = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Semt = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Adres = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depolar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fisler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FisKodu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FisTuru = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CariId = table.Column<int>(type: "int", nullable: true),
                    FaturaUnvani = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CepTelefonu = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Il = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Ilce = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Semt = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Adres = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    VergiDairesi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VergiNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    BelgeNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlasiyerId = table.Column<int>(type: "int", nullable: true),
                    IskontoOrani = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IskontoTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Alacak = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Borc = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ToplamTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FisBaglantiKodu = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fisler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HizliSatisGruplari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrupAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HizliSatisGruplari", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Indirimler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Durumu = table.Column<bool>(type: "bit", nullable: false),
                    StokKodu = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Barkod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StokAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IndirimTuru = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IndirimOrani = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indirimler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kasalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KasaKodu = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    KasaAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    YetkiliKodu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    YetkiliAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kasalar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kodlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tablo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OnEki = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SonDeger = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kodlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Aktif = table.Column<bool>(type: "bit", nullable: false),
                    Durumu = table.Column<bool>(type: "bit", nullable: false),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Adi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Soyadi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gorevi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Parola = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HatirlatmaSorusu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HatirlatmaCevap = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SonGirisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KullaniciLoglari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SonGirisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    YapilanIslem = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    YapilanIslemTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciLoglari", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KullaniciRolleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RootId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FormAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    KontrolAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Yetki = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciRolleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OdemeTurleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OdemeTuruKodu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OdemeTuruAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OdemeTurleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonelHareketleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FisKodu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Unvani = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PersonelKodu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PersonelAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TcKimlikNo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Donemi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrimOrani = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ToplamSatis = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AylikMaas = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelHareketleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personeller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Durumu = table.Column<bool>(type: "bit", nullable: false),
                    PersonelUnvani = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PersonelKodu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PersonelAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonelTc = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    PersonelGiris = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PersonelCikis = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CepTelefonu = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    EMail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Web = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Il = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Ilce = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Semt = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Adres = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    AylikMaas = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PrimOrani = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personeller", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stoklar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Durumu = table.Column<bool>(type: "bit", nullable: false),
                    StokKodu = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    StokAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Barkod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BarkodTuru = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Birimi = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StokGrubu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StokAltGrubu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Marka = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Modeli = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OzelKod1 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    OzelKod2 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    OzelKod3 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    OzelKod4 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    GarantiSuresi = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    UreticiKodu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AlisKdv = table.Column<int>(type: "int", nullable: false),
                    SatisKdv = table.Column<int>(type: "int", nullable: false),
                    AlisFiyati1 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AlisFiyati2 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AlisFiyati3 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SatisFiyati1 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SatisFiyati2 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SatisFiyati3 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinStokMiktari = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    MaxStokMiktari = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stoklar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tanimlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Turu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tanimi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tanimlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HizliSatisUrunleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Barkod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UrunAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GrupId = table.Column<int>(type: "int", nullable: false),
                    HizliSatisGrupId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HizliSatisUrunleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HizliSatisUrunleri_HizliSatisGruplari_HizliSatisGrupId",
                        column: x => x.HizliSatisGrupId,
                        principalTable: "HizliSatisGruplari",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KasaHareketleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FisKodu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Hareket = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    KasaId = table.Column<int>(type: "int", nullable: false),
                    OdemeTuruId = table.Column<int>(type: "int", nullable: false),
                    CariId = table.Column<int>(type: "int", nullable: true),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KasaHareketleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KasaHareketleri_Kasalar_KasaId",
                        column: x => x.KasaId,
                        principalTable: "Kasalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KasaHareketleri_OdemeTurleri_OdemeTuruId",
                        column: x => x.OdemeTuruId,
                        principalTable: "OdemeTurleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StokHareketleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FisKodu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Hareket = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StokId = table.Column<int>(type: "int", nullable: false),
                    Miktar = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    Kdv = table.Column<int>(type: "int", nullable: false),
                    BirimFiyati = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IndirimOrani = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    DepoId = table.Column<int>(type: "int", nullable: false),
                    SeriNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Siparis = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StokHareketleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StokHareketleri_Depolar_DepoId",
                        column: x => x.DepoId,
                        principalTable: "Depolar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StokHareketleri_Stoklar_StokId",
                        column: x => x.StokId,
                        principalTable: "Stoklar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HizliSatisUrunleri_HizliSatisGrupId",
                table: "HizliSatisUrunleri",
                column: "HizliSatisGrupId");

            migrationBuilder.CreateIndex(
                name: "IX_KasaHareketleri_KasaId",
                table: "KasaHareketleri",
                column: "KasaId");

            migrationBuilder.CreateIndex(
                name: "IX_KasaHareketleri_OdemeTuruId",
                table: "KasaHareketleri",
                column: "OdemeTuruId");

            migrationBuilder.CreateIndex(
                name: "IX_StokHareketleri_DepoId",
                table: "StokHareketleri",
                column: "DepoId");

            migrationBuilder.CreateIndex(
                name: "IX_StokHareketleri_StokId",
                table: "StokHareketleri",
                column: "StokId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cariler");

            migrationBuilder.DropTable(
                name: "Fisler");

            migrationBuilder.DropTable(
                name: "HizliSatisUrunleri");

            migrationBuilder.DropTable(
                name: "Indirimler");

            migrationBuilder.DropTable(
                name: "KasaHareketleri");

            migrationBuilder.DropTable(
                name: "Kodlar");

            migrationBuilder.DropTable(
                name: "Kullanicilar");

            migrationBuilder.DropTable(
                name: "KullaniciLoglari");

            migrationBuilder.DropTable(
                name: "KullaniciRolleri");

            migrationBuilder.DropTable(
                name: "PersonelHareketleri");

            migrationBuilder.DropTable(
                name: "Personeller");

            migrationBuilder.DropTable(
                name: "StokHareketleri");

            migrationBuilder.DropTable(
                name: "Tanimlar");

            migrationBuilder.DropTable(
                name: "HizliSatisGruplari");

            migrationBuilder.DropTable(
                name: "Kasalar");

            migrationBuilder.DropTable(
                name: "OdemeTurleri");

            migrationBuilder.DropTable(
                name: "Depolar");

            migrationBuilder.DropTable(
                name: "Stoklar");
        }
    }
}
