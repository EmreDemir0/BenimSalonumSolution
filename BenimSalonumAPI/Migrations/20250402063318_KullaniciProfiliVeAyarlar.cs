using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenimSalonumAPI.Migrations
{
    /// <inheritdoc />
    public partial class KullaniciProfiliVeAyarlar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReasonRevoked",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RevokedAt",
                table: "RefreshTokens",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adres",
                table: "Kullanicilar",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BasarisizGirisDenemesi",
                table: "Kullanicilar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Cinsiyet",
                table: "Kullanicilar",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DogumTarihi",
                table: "Kullanicilar",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Kullanicilar",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "HesapKilitlenmeTarihi",
                table: "Kullanicilar",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IkiFaktorluDogrulamaAnahtari",
                table: "Kullanicilar",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IkiFaktorluKimlikDogrulama",
                table: "Kullanicilar",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProfilResmiUrl",
                table: "Kullanicilar",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sehir",
                table: "Kullanicilar",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SifreDegistirmeTarihi",
                table: "Kullanicilar",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefon",
                table: "Kullanicilar",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "KullaniciAyarlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    EmailBildirimAktif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    SMSBildirimAktif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    UygulamaIciBildirimAktif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RandevuHatirlatmaZamani = table.Column<int>(type: "int", nullable: false, defaultValue: 60),
                    Dil = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValue: "tr-TR"),
                    Tema = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValue: "Light"),
                    CalismaBaslangicSaati = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValue: "09:00"),
                    CalismaBitisSaati = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValue: "18:00"),
                    OturumSuresi = table.Column<int>(type: "int", nullable: false, defaultValue: 120),
                    OtomatikKilitlemeAktif = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    OtomatikKilitlemeSuresi = table.Column<int>(type: "int", nullable: false, defaultValue: 15),
                    SonGuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciAyarlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KullaniciAyarlar_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KullaniciAyarlar_KullaniciId",
                table: "KullaniciAyarlar",
                column: "KullaniciId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KullaniciAyarlar");

            migrationBuilder.DropColumn(
                name: "ReasonRevoked",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "RevokedAt",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "Adres",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "BasarisizGirisDenemesi",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "Cinsiyet",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "DogumTarihi",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "HesapKilitlenmeTarihi",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "IkiFaktorluDogrulamaAnahtari",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "IkiFaktorluKimlikDogrulama",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "ProfilResmiUrl",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "Sehir",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "SifreDegistirmeTarihi",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "Telefon",
                table: "Kullanicilar");
        }
    }
}
