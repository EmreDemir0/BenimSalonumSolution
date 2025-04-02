using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenimSalonumAPI.Migrations
{
    /// <inheritdoc />
    public partial class LisansVeFirmaEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AnaKullanici",
                table: "Kullanicilar",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "FirmaId",
                table: "Kullanicilar",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YoneticiId",
                table: "Kullanicilar",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Firmalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirmaAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VergiNo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    VergiDairesi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MersisNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Adres = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Il = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Ilce = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WebSitesi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OlusturanKullaniciId = table.Column<int>(type: "int", nullable: true),
                    GuncelleyenKullaniciId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firmalar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lisanslar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LisansKodu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirmaId = table.Column<int>(type: "int", nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KullaniciSayisiLimiti = table.Column<int>(type: "int", nullable: false),
                    Aktif = table.Column<bool>(type: "bit", nullable: false),
                    AktivasyonAnahtari = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SonKontrolTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KalanKontrolGunu = table.Column<int>(type: "int", nullable: false),
                    LisansTuru = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notlar = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lisanslar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lisanslar_Firmalar_FirmaId",
                        column: x => x.FirmaId,
                        principalTable: "Firmalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kullanicilar_FirmaId",
                table: "Kullanicilar",
                column: "FirmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Kullanicilar_YoneticiId",
                table: "Kullanicilar",
                column: "YoneticiId");

            migrationBuilder.CreateIndex(
                name: "IX_Lisanslar_FirmaId",
                table: "Lisanslar",
                column: "FirmaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kullanicilar_Firmalar_FirmaId",
                table: "Kullanicilar",
                column: "FirmaId",
                principalTable: "Firmalar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Kullanicilar_Kullanicilar_YoneticiId",
                table: "Kullanicilar",
                column: "YoneticiId",
                principalTable: "Kullanicilar",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kullanicilar_Firmalar_FirmaId",
                table: "Kullanicilar");

            migrationBuilder.DropForeignKey(
                name: "FK_Kullanicilar_Kullanicilar_YoneticiId",
                table: "Kullanicilar");

            migrationBuilder.DropTable(
                name: "Lisanslar");

            migrationBuilder.DropTable(
                name: "Firmalar");

            migrationBuilder.DropIndex(
                name: "IX_Kullanicilar_FirmaId",
                table: "Kullanicilar");

            migrationBuilder.DropIndex(
                name: "IX_Kullanicilar_YoneticiId",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "AnaKullanici",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "FirmaId",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "YoneticiId",
                table: "Kullanicilar");
        }
    }
}
